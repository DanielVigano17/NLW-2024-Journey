using Journey.Exception.ExceptionsBase;
using Journey.Exception;
using Journey.Infrastructure;
using Journey.Infrastructure.Enums;

namespace Journey.Application.UseCases.Activities.DeleteActivity
{
    public class DeleteActivityUseCase
    {
        public void Execute(Guid tripId, Guid ActivityId)
        {
            var dbContext = new JourneyDbContext();

            var activity = dbContext.Activities.FirstOrDefault(ac => ac.Id == ActivityId && ac.TripId == tripId);

            if (activity is null)
            {
                throw new NotFoundException(ResourceErrorMessages.ACTIVITY_NOT_FOUND);
            }

            dbContext.Activities.Remove(activity);

            dbContext.SaveChanges();
        }
    }
}
