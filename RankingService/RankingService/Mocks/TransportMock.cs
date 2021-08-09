using System.Collections.Generic;

namespace RankingService.Mocks
{
    public static class TransportMock
    {
        public static List<Transport> Transports = new List<Transport>()
        {
            new Transport()
            {
                Id = 1,
                Name = "Transport one"
            },
            new Transport()
            {
                Id = 2,
                Name = "Transport two"
            }
        };
    }

    public class Transport
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
