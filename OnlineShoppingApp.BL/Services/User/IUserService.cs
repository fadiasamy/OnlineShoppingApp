using OnlineShoppingApp.DAL.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShoppingApp.BL.Services.User
{
    public interface IUserService
    {
        Task<ApplicationUser> RegisterAsync(ApplicationUser user, string password);
        Task<ApplicationUser> LoginAsync(string email, string password);
        Task<String> GenerateJwtToken(ApplicationUser user, IList<string> roles);
        Task<List<string>> GetUserRolesAsync(ApplicationUser user);
        Task<ApplicationUser> GetUserByIdAsync(string userId);

    }
}
