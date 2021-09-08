using AccountService.Data.Interfaces;
using AccountService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AccountService.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountContext _context;

        public AccountRepository(AccountContext context)
        {
            _context = context;
        }

        public void Create(Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();
        }

        public IEnumerable<Account> Get()
        {
            return _context.Accounts.Include(a => a.Currency);
        }

        public Account Get(int id)
        {
            return _context.Accounts
                .Where(account => account.Id == id)
                .Include(x => x.Currency)
                .FirstOrDefault();
        }

        public void Update(Account account)
        {
            _context.Entry(account).State = EntityState.Modified;

            _context.SaveChanges();

        }

        public void Delete(Account account)
        {
            _context.Accounts.Remove(account);
            _context.SaveChanges();
        }
    }
}
