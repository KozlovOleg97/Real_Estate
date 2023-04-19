using Microsoft.AspNetCore.Mvc;
using Real_Estate.Core.Application.Features.Improvements.Commands.DeleteImprovomentsById;
using Real_Estate.Core.Application.Features.TypeOfProperties.Commands.CreateTypeOfProperties;
using Real_Estate.Core.Application.Features.TypeOfProperties.Commands.UpdateTypeOfProperties;
using Real_Estate.Core.Application.Features.TypeOfProperties.Queries.GetAllTypeOfProperties;
using Real_Estate.Core.Application.Features.TypeOfProperties.Queries.GetTypeOfPropertiesById;
using Real_Estate.Core.Application.Interfaces.Services;
using Real_Estate.Core.Application.ViewModels.TypeOfProperties;

namespace Real_Estate.Presentation.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeOfPropertiesController : BaseApiController
    {
        private readonly ITypeOfPropertiesService _typeOfPropertiesService;

        public TypeOfPropertiesController(ITypeOfPropertiesService typeOfPropertiesService)
        {
            _typeOfPropertiesService = typeOfPropertiesService;
        }

        [HttpGet]
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

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveTypeOfPropertiesViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var category = await Mediator.Send(
                    new GetTypeOfPropertiesByIdQuery { Id = id });

                if (category == null)
                {
                    return NotFound("There aren't property types");
                }

                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
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


        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteImprovementsByIdCommand { Id = id });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
