using Azakaw.Domain.Models;

namespace Azakaw.Domain.Services
{
    public interface IUserService : IBaseService<User>
    {
        ResponseModel<User> GetByEmail(string email);
    }
}