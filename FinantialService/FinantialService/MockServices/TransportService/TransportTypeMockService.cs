using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinantialService.MockServices.TransportService
{
    public class TransportTypeMockService : ITransportTypeMockRepository
    {
        public static List<TransportType> Products { get; set; } = new List<TransportType>()
        {
                new TransportType
                {
                    TransportTypeId = Guid.Parse("14b114d3-af35-4322-9fc4-7e5b618ccd2e"),
                    Name = "Posta Srbije"

                },
                new TransportType
                {
                    TransportTypeId = Guid.Parse("c8a40dbb-7ec7-43fd-9181-30091a4bc26c"),
                    Name = "DExpress"

                },

        };

        public bool TransportTypeExistsById(Guid transportTypeId)
        {
            if (Products.FirstOrDefault(p => p.TransportTypeId == transportTypeId) == null)
            {
                return false;
            }
            return true;
        }
    }
}
