using AccountService.Data.Interfaces;
using AccountService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AccountService.Data.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly AccountContext _context;

        public CurrencyRepository(AccountContext context)
        {
            _context = context;
        }

        public void Create(Currency currency)
        {
            _context.Currencies.Add(currency);
            _context.SaveChanges();
        }

        public void Delete(Currency currency)
        {
            _context.Currencies.Remove(currency);
            _context.SaveChanges();
        }

        public IEnumerable<Currency> Get()
        {
            return _context.Currencies;
        }

        public Currency Get(int id)
        {
            return _context.Currencies.FirstOrDefault(currency => currency.Id == id);
        }

        public void Update(Currency currency)
        {
            _context.Entry(currency).State = EntityState.Modified;

            _context.SaveChanges();
        }
    }
}
