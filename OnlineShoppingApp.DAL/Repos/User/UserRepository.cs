using Microsoft.AspNetCore.Identity;
using OnlineShoppingApp.DAL.Data.Models;
using OnlineShoppingApp.DAL.Repos.User;

public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<ApplicationUser> RegisterAsync(ApplicationUser user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            return user;
        }

        throw new InvalidOperationException("Registration failed");
    }

    public async Task<ApplicationUser> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return null;
        }

        var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);

        if (signInResult.Succeeded)
        {
            return user;
        }

        return null;
    }
}
