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

            var validator = new RegisterTripValidator();

            var result =  validator.Validate(req);

            if (!result.IsValid) {

                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
