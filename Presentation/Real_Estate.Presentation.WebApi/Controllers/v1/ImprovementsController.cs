using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Real_Estate.Core.Application.Features.Improvements.Commands.CreateImprovements;
using Real_Estate.Core.Application.Features.Improvements.Commands.DeleteImprovomentsById;
using Real_Estate.Core.Application.Features.Improvements.Commands.UpdateImprovements;
using Real_Estate.Core.Application.Features.Improvements.Queries.GetAllImprovements;
using Real_Estate.Core.Application.Features.Improvements.Queries.GetImprovementsById;
using Real_Estate.Core.Application.Interfaces.Services;
using Real_Estate.Core.Application.ViewModels.Improvements;
using Swashbuckle.AspNetCore.Annotations;

namespace Real_Estate.Presentation.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Supporting of improvements")]
    public class ImprovementsController : BaseApiController
    {
        [Authorize(Policy = "RequireOnlyAdminAndDeveloper")]
        [HttpGet]
        [SwaggerOperation(
            Summary = "List of improvements",
            Description = "Get all the registered improvements."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImprovementsViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List()
        {
            try
            {
                var improvements = await Mediator.Send(
                    new GetAllImprovementsQuery());

                return Ok(improvements);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Policy = "RequireOnlyAdminAndDeveloper")]
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Upgrade by ID",
            Description = "Get an improvement with using the ID as a filter."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveImprovementsViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var improvement = await Mediator.Send(
                    new GetImprovementsByIdQuery { Id = id });

                return Ok(improvement);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [SwaggerOperation(
            Summary = "Create imrovement",
            Description = "Receive the necessary parameters to create a new improvement."
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateImprovementsCommand command)
        {
            try
            {
                await Mediator.Send(command);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Updating improvement",
            Description = "Receive the necessary parameters to modify an existing improvement."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveImprovementsViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, UpdateImprovementsCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                if (id != command.Id)
                {
                    return BadRequest();
                }

                return Ok(await Mediator.Send(command));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete an improvement",
            Description = "Receive the necessary parameters to eliminate an existing improvement."
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(
                    new DeleteImprovementsByIdCommand { Id = id });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
