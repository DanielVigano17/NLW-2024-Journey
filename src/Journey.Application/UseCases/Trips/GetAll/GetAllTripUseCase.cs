using Journey.Communication.Responses;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.GetAll
{
    public class GetAllTripUseCase
    {
        public ResponseTripsJson Execute()
        {
            var dbContext = new JourneyDbContext();

            var trips = dbContext.Trips.AsNoTracking().ToList();

            return new ResponseTripsJson
            {
                Trips = trips.Select(trip => new ResponseShortTripJson { 
                    Id = trip.Id,
                    Name = trip.Name,
                    EndDate = trip.EndDate,
                    StartDate = trip.StartDate,
                }).ToList(),
            };

        }
    }
}
