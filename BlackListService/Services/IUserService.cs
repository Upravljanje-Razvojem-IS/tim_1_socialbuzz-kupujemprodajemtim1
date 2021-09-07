using BlackListService.Entities;

namespace BlackListService.Services
{
    public interface IUserService
    {
        User Get(int id);
    }
}
