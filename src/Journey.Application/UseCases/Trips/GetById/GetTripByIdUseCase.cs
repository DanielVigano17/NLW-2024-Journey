using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.GetById
{
    public class GetTripByIdUseCase
    {
        public ResponseTripJson Execute(Guid id) {

            var dbContext = new JourneyDbContext();

            var trip = dbContext.Trips
                .Include(trip => trip.Activities)
                .AsNoTracking()
                .FirstOrDefault(trip => trip.Id.Equals(id));

            if (trip is null) {

                throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);
            }

            return new ResponseTripJson { 
                Id = trip.Id,
                EndDate = trip.EndDate,
                Name = trip.Name,
                StartDate = trip.StartDate,
                Activities = trip.Activities.Select(activity => new ResponseActivityJson {
                    Id = activity.Id,
                    Name = activity.Name,
                    Date = activity.Date,
                    Status = (Communication.Enums.ActivityStatus)activity.Status,
                }).ToList(),
            };
        
        }
    }
}
