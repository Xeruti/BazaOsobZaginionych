using BazaOsobZaginionych.Models.DTO;

namespace BazaOsobZaginionych.Repositories.Abstract
{
    public interface IUserAuthenticationService
    {
        Task<Status> LoginAsync(Login model);
        Task<Status> RegisterAsync(RegistrationModel model);
        Task LogoutAsync();
    }
}
