

using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;

namespace TodoList.Application.Services
{
    public class RecoveryPassawordUserService : IRecoveryPasswordUserService
    {
        private readonly IRecoveryPasswordUserRepository _recoveryPasswordUserRepository;

        public RecoveryPassawordUserService(IRecoveryPasswordUserRepository recoveryPasswordUserRepository)
        {
            _recoveryPasswordUserRepository = recoveryPasswordUserRepository;
        }

        public async Task<User> VerificationUserAsync(string email)
           => await _recoveryPasswordUserRepository.VerificationUser(email);
        
    }
}
