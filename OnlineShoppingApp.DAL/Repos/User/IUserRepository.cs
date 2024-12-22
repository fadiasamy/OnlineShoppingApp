using OnlineShoppingApp.DAL.Data.Models;
using System.Threading.Tasks;

namespace OnlineShoppingApp.DAL.Repos.User
{
    public interface IUserRepository
    {
        Task<ApplicationUser> RegisterAsync(ApplicationUser user, string password);
        Task<ApplicationUser> LoginAsync(string email, string password);
    }
}
