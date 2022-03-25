using KC.HMS.Core.Models;
using KC.HMS.Services.ViewModels;

namespace KC.HMS.Services.Contracts
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterModel model);
        Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<AuthenticationModel> RefreshTokenAsync(string jwtToken);

        bool RevokeToken(string token);
        ApplicationUser GetById(string id);
    }
}
