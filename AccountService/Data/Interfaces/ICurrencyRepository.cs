using AccountService.Models;
using System.Collections.Generic;

namespace AccountService.Data.Interfaces
{
    public interface ICurrencyRepository
    {
        IEnumerable<Currency> Get();
        Currency Get(int id);
        void Create(Currency currency);
        void Update(Currency currency);
        void Delete(Currency currency);
    }
}
