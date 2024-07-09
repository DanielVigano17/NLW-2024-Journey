using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;

namespace Journey.Application.UseCases.Trips.Register
{
    public class RegisterTripUseCase
    {
        public ResponseShortTripJson Execute(RequestRegisterTripJson req) {

            Validate(req);

            var dbContext = new JourneyDbContext();

            var entity = new Trip
            {
                Name = req.Name,
                EndDate = req.EndDate,
                StartDate = req.StartDate,
            };

            dbContext.Add(entity);
            dbContext.SaveChanges();

            return new ResponseShortTripJson
            {
                Name = entity.Name,
                EndDate = entity.EndDate,
                StartDate = entity.StartDate,
                Id = entity.Id,
            };
            
        }

        private void Validate(RequestRegisterTripJson req) {

            if (string.IsNullOrWhiteSpace(req.Name)) {

                throw new JourneyException(ResourceErrorMessages.NAME_EMPTY);
            
            }

            if (req.StartDate.Date < DateTime.UtcNow.Date) {

                throw new JourneyException(ResourceErrorMessages.DATE_TRIP_MUST_BE_LATER_THAN_TODAY);

            }

            if (req.EndDate.Date < req.StartDate.Date)
            {

                throw new JourneyException(ResourceErrorMessages.END_DATE_TRIP_MUST_BE_LATER_START_DATE);

            }

        }
    }
}
