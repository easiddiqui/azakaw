using Azakaw.Domain;
using Azakaw.Domain.Models;
using Azakaw.Domain.Repositories;
using Azakaw.Domain.Services;

namespace Azakaw.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IBaseRepository<User> baseRepository) : base(baseRepository)
        {
        }

        public ResponseModel<User> GetByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return ResponseModel<User>.GetValidationFailureResponse(null);

            var user = BaseRepository.Get(x => x.Email == email);

            return user?.Id > 0
                ? ResponseModel<User>.GetSuccessResponse(user)
                : ResponseModel<User>.GetNotFoundResponse();
        }
    }
}