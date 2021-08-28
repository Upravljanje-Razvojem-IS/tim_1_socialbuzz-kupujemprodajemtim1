using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinantialService.MockServices.UserService
{
    public class PersonalAccount
    {
        //Korisnicki nalog
        public Guid PersonalAccountId { get; set; }

        //Racun sa novcem
        public Guid AccountId { get; set; }
    }
}
