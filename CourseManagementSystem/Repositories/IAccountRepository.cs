using CourseManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CourseManagementSystem.Repositories
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(RegisterViewModel userModel);
        Task<bool> IsUserExist(string email);
        Task<SignInResult> Login(LoginViewModel userModel);
        Task Logout();
        Task<string[]> ResetPassword(string email);
        Task<IdentityResult> ResetPassword(ApplicationUser user, ResetPasswordViewModel userModel);
        Task<ApplicationUser> FindByIdAsync(string uid);
        Task<ApplicationUser> FindByEmailAsync(string email);
        bool isUserSignedInAsync(ClaimsPrincipal user);
    }
}
