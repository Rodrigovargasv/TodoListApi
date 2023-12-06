

using eSistemCurso.Domain.Common.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Caching.Memory;
using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;

namespace TodoList.Application.Services
{
    public class RecoveryPassawordUserService : IRecoveryPasswordUserService
    {
        private readonly IRecoveryPasswordUserRepository _recoveryPasswordUserRepository;
        private readonly ISendEmail _sendEmail;
        private readonly GenerationCodeRecoveryService _generationCodeRecoveryService;
        private readonly IMemoryCache _memoryCache;

        public RecoveryPassawordUserService(IRecoveryPasswordUserRepository recoveryPasswordUserRepository,
            ISendEmail sendEmail, GenerationCodeRecoveryService code, IMemoryCache memoryCache)

        {
            _recoveryPasswordUserRepository = recoveryPasswordUserRepository;
            _sendEmail = sendEmail;
            _generationCodeRecoveryService = code;
            _memoryCache = memoryCache;
        }

        public async Task<string> VerificationUserAsync(string email)
        {

            var user = await _recoveryPasswordUserRepository.VerificationUser(email);


            if (user is null)
                throw new NotFoundException("O email é inválido, ou não correponde ao email cadastrado para este usuário.");


            await _sendEmail.SendEmailAsync(email, "Codigo de recuperação",

                $"Segue abaixo o código de recuperação de senha:\n\n" +
                $"Código: {_generationCodeRecoveryService.GenerationCodeTemporary()}\n\n" +
                $"Observação: O código tem duração de 2 minutos.");

 
            // Armazena email em cache por 3 minutos.
            _memoryCache.Set("id", user.Id, TimeSpan.FromMinutes(3));

            return $"Um código será enviado para o email: {user.Email}.";

        }

    }
}
