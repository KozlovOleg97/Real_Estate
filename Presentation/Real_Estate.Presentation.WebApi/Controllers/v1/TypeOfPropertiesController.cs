using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Real_Estate.Core.Application.Features.Improvements.Commands.DeleteImprovomentsById;
using Real_Estate.Core.Application.Features.TypeOfProperties.Commands.CreateTypeOfProperties;
using Real_Estate.Core.Application.Features.TypeOfProperties.Commands.DeleteTypeOfPropertiesById;
using Real_Estate.Core.Application.Features.TypeOfProperties.Commands.UpdateTypeOfProperties;
using Real_Estate.Core.Application.Features.TypeOfProperties.Queries.GetAllTypeOfProperties;
using Real_Estate.Core.Application.Features.TypeOfProperties.Queries.GetTypeOfPropertiesById;
using Real_Estate.Core.Application.Interfaces.Services;
using Real_Estate.Core.Application.ViewModels.TypeOfProperties;
using Swashbuckle.AspNetCore.Annotations;

namespace Real_Estate.Presentation.WebApi.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Supporting type of properties")]
    public class TypeOfPropertiesController : BaseApiController
    {
        [Authorize(Policy = "RequireOnlyAdminAndDeveloper")]
        [HttpGet]
        [SwaggerOperation(
            Summary = "List of Type Property",
            Description = "Get all types of registered sales."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TypeOfPropertiesViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List()
        {
            try
            {
                var typeOfProperties = await Mediator.Send(
                    new GetAllTypeOfPropertiesQuery());

                return Ok(typeOfProperties);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Policy = "RequireOnlyAdminAndDeveloper")]
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Property Type by Id",
            Description = "Get a property type with using the Id as a filter."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveTypeOfPropertiesViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var typeOfProperty = await Mediator.Send(new GetTypeOfPropertiesByIdQuery
                {
                    Id = id
                });

                return Ok(typeOfProperty);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [SwaggerOperation(
            Summary = "Creating of Property Type",
            Description = "Receive the necessary parameters for a new type of property."
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateTypeOfPropertiesCommand command)
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
            Summary = "Update of Property Type",
            Description = "Receive the necessary parameters to modify an existing property type."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveTypeOfPropertiesViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, UpdateTypeOfPropertiesCommand command)
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
            Summary = "Delete of Property Type",
            Description = "Receive the necessary parameters to delete an existing property type."
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteTypeOfPropertiesByIdCommand { Id = id });

                return NoContent();
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
