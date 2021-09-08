using AccountService.Models;

namespace AccountService.Data.Interfaces
{
    public interface IUserRepository
    {
        User Get(string email);
        User Get(int id);
    }
}
