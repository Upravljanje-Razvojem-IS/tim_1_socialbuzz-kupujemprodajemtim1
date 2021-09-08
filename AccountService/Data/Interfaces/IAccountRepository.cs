using AccountService.Models;
using System.Collections.Generic;

namespace AccountService.Data.Interfaces
{
    public interface IAccountRepository
    {
        IEnumerable<Account> Get();
        Account Get(int id);
        void Create(Account account);
        void Update(Account account);
        void Delete(Account account);
    }
}
