

using Microsoft.Extensions.Caching.Memory;

namespace TodoList.Application.Services
{
    public class GenerationCodeRecoveryService
    {

        public string Code { get; private set; }
        public DateTime CreateDate { get; private set; }
        public int ValidationTimeInMinutes { get; private set; }

        private readonly IMemoryCache _memoryCache;



        public GenerationCodeRecoveryService(int validationTimeInMinutes = 2, IMemoryCache memoryCache = null)
        {
            ValidationTimeInMinutes = validationTimeInMinutes;

            _memoryCache = memoryCache;
        }

        public string GenerationCodeTemporary()
        {
            CreateDate = DateTime.Now;
            Code = RandomCodeGeneration();

            _memoryCache.Set("CodigoTemporario", Code, TimeSpan.FromMinutes(ValidationTimeInMinutes));
            return Code;
        }

        

        public bool VerifyCodeTemporary(string code)
        {

            if (_memoryCache.TryGetValue("CodigoTemporario", out string codeCache))
            {

                var PassMinutes = (DateTime.Now - CreateDate).TotalMinutes;

                if (PassMinutes < 0 || code != codeCache || code is null)
                    return true;
            }
            

            return false;
            
         }

        private string RandomCodeGeneration()
            // Gere um código aleatório aqui
          => Guid.NewGuid().ToString().Substring(0, 6);
    
        
    }
}
