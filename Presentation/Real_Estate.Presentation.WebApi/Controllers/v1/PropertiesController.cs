using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Real_Estate.Core.Application.Features.Properties.Queries.GetAllProperties;
using Real_Estate.Core.Application.Features.Properties.Queries.GetPropertiesById;
using Real_Estate.Core.Application.Interfaces.Services;
using Real_Estate.Core.Application.ViewModels.Properties;
using Swashbuckle.AspNetCore.Annotations;

namespace Real_Estate.Presentation.WebApi.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [SwaggerTag("Consultations Properties")]
    public class PropertiesController : BaseApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "RequireOnlyAdminAndDeveloper")]
        [HttpGet]
        [SwaggerOperation(
            Summary = "List of properties",
            Description = "Get all the properties."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertiesViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List()
        {
            try
            {
                var properties = await Mediator.Send(
                    new GetAllPropertiesQuery());

                return Ok(properties);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Policy = "RequireOnlyAdminAndDeveloper")]
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Property by ID",
            Description = "Get the property with using the id as a filter."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertiesViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var property = await Mediator.Send(
                    new GetPropertiesByIdQuery { Id = id });

                return Ok(property);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Policy = "RequireOnlyAdminAndDeveloper")]
        [HttpGet("{code}")]
        [SwaggerOperation(
            Summary = "Property by code",
            Description = "Get the property with using the code as a filter."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertiesViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByCode(string code)
        {
            try
            {
                var property = await Mediator.Send(
                    new GetPropertiesByCodeQuery { Code = code });
                
                return Ok(property);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
