

using eSistemCurso.Domain.Common.Exceptions;
using Microsoft.Extensions.Caching.Memory;
using TodoList.Application.Interfaces;

namespace TodoList.Application.Services
{
    public class GenerationCodeRecoveryService
    {

        public string Code { get; private set; }
        public DateTime CreateDate { get; private set; }
        public int ValidationTimeInMinutes { get; private set; }

        private readonly IMemoryCache _memoryCache;

        private readonly IUserService _userService;



        public GenerationCodeRecoveryService(IMemoryCache memoryCache, IUserService userService)
        {
            ValidationTimeInMinutes = 2;

            _memoryCache = memoryCache;
            _userService = userService;
        }

        public string GenerationCodeTemporary()
        {
            CreateDate = DateTime.Now;
            Code = RandomCodeGeneration();

            // armazena código temporário por 2 minutos
            _memoryCache.Set("CodigoTemporario", Code, TimeSpan.FromMinutes(ValidationTimeInMinutes));

            return Code;
        }



        public async Task<string> VerifyCodeTemporary(string code, string password, string confirmPassword)
        {


            // Busca o id armazenado em cache e seta na variavel emailCache
            _memoryCache.TryGetValue("id", out int id);

            var userId = await _userService.GetUserByIdAsync(id);


            if (password != confirmPassword)
                throw new BadRequestException("A Senha devem ser iguais.");

            if (password is null || confirmPassword is null)
                throw new BadRequestException("Senha inválida.");

            userId.Password = password;

            await _userService.UpdateUserAsync(userId, id);


            if (_memoryCache.TryGetValue("CodigoTemporario", out string codeCache))
            {

                var PassMinutes = (DateTime.Now - CreateDate).TotalMinutes;

                if (PassMinutes < 0 || code != codeCache || code is null)
                    throw new CustomException("Código inválido ou expirado.");
            }


            return "Senha atualiza com sucesso.";
        }

        private string RandomCodeGeneration()
          // Gere um código aleatório aqui
          => Guid.NewGuid().ToString().Substring(0, 6).ToUpper();


    }
}
