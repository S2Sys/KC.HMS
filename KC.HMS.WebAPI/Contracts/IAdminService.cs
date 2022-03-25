
namespace KC.HMS.WebAPI.Contracts
{
    public interface IAdminService
    {
        Task<string> AddRoleAsync(AddRoleModel model);

    }
}
