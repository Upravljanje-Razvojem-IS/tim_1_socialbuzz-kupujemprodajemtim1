using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinantialService.MockServices.TransportService
{
    public interface ITransportTypeMockRepository
    {
        bool TransportTypeExistsById(Guid transportTypeId);
    }
}
