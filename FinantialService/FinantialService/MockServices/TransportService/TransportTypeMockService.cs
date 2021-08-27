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
                    TransportTypeId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c1"),
                    Name = "Posta Srbije"

                },
            new TransportType
                {
                    TransportTypeId = Guid.Parse("b6496c4a-f938-4863-b3b7-2db52e4271dd"),
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
