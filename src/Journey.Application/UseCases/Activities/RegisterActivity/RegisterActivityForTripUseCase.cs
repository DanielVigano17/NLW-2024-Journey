using FluentValidation.Results;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Activities.RegisterActivity
{
    public class RegisterActivityForTripUseCase
    {
        public ResponseActivityJson Execute(Guid tripId,RequestRegisterActivityJson req)
        {
            var dbContext = new JourneyDbContext();

            var trip = dbContext.Trips
                .FirstOrDefault(trip => trip.Id == tripId);

            Validate(trip, req);

            var entity = new Activity
            {
                Name = req.Name,
                Date = req.Date,
                TripId = tripId,
            };

            dbContext.Activities.Add(entity);
            dbContext.SaveChanges();

            return new ResponseActivityJson
            {
                Id = entity.Id,
                Name = entity.Name,
                Date = entity.Date,
                Status = (Communication.Enums.ActivityStatus)entity.Status,
            };

        }

        private void Validate(Trip? trip,RequestRegisterActivityJson req) {
            if (trip is null)
            {
                throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);
            }

            var validator =  new RegisterActovityValidator();

           var result = validator.Validate(req);

            if ((req.Date >= trip.StartDate && req.Date <= trip.EndDate) == false) 
            {
                result.Errors.Add(new ValidationFailure("Date", ResourceErrorMessages.DATE_NOT_WHITHIN_TRAVEL_PERIOD));
            }

           if (!result.IsValid) {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);     
           }
        }
    }
}
