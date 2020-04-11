using System;
using Azakaw.Domain.Models;

namespace Azakaw.Domain
{
    public static class WorkContext
    {
        public static CurrentUser CurrentUser { get; set; }

        public static void Authorized(User user, string token)
        {
            CurrentUser = new CurrentUser
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = Enum.GetName(typeof(Role), user.Role),
                Token = token
            };
        }

        public static void UnAuthorized()
        {
            CurrentUser = null;
        }
    }

    public class CurrentUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}