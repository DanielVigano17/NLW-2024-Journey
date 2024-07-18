using Journey.Application.UseCases.Activities.CompleteActivity;
using Journey.Application.UseCases.Activities.DeleteActivity;
using Journey.Application.UseCases.Activities.RegisterActivity;
using Journey.Application.UseCases.Trips.Delete;
using Journey.Application.UseCases.Trips.GetAll;
using Journey.Application.UseCases.Trips.GetById;
using Journey.Application.UseCases.Trips.Register;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseShortTripJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] RequestRegisterTripJson req) {

            var useCase = new RegisterTripUseCase();
            var response = useCase.Execute(req);

            return Created(string.Empty, response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseTripsJson), StatusCodes.Status200OK)]
        public IActionResult GetAll() {

            var useCase = new GetAllTripUseCase();
            var result = useCase.Execute();

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public IActionResult GetById([FromRoute] Guid id) {

            var useCase = new GetTripByIdUseCase();
            var response = useCase.Execute(id);
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public IActionResult Delete([FromRoute] Guid id)
        {

            var useCase = new DeleteTripByIdUseCase();
            useCase.Execute(id);
            return NoContent();
        }

        [HttpPost]
        [Route("{tripId}/activity")]
        [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public IActionResult RegisterActivity([FromRoute] Guid tripId,[FromBody] RequestRegisterActivityJson req) {

            var useCase = new RegisterActivityForTripUseCase();

            var response = useCase.Execute(tripId, req);

            return Created(string.Empty, response);

        }

        [HttpPut]
        [Route("{tripId}/activity/{activityId}/complete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public IActionResult CompleteActivity([FromRoute] Guid tripId, [FromRoute] Guid activityId)
        {

            var useCase = new CompleteActivityUseCase();

            useCase.Execute(tripId, activityId);

            return NoContent();

        }

        [HttpDelete]
        [Route("{tripId}/activity/{activityId}/")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public IActionResult DeleteActivity([FromRoute] Guid tripId, [FromRoute] Guid activityId)
        {

            var useCase = new DeleteActivityUseCase();

            useCase.Execute(tripId, activityId);

            return NoContent();

        }
    }


}
