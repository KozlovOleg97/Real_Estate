﻿using Microsoft.AspNetCore.Mvc;
using Real_Estate.Core.Application.Interfaces.Services;
using Real_Estate.Core.Application.ViewModels.TypeOfSales;

namespace Real_Estate.Presentation.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeOfSalesController : ControllerBase
    {
        private readonly ITypeOfSalesService _typeOfSalesService;

        public TypeOfSalesController(ITypeOfSalesService typeOfSalesService)
        {
            _typeOfSalesService = typeOfSalesService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TypeOfSalesViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List()
        {
            try
            {
                var typeOfProperties = await _typeOfSalesService.GetAllViewModel();

                if (typeOfProperties == null || typeOfProperties.Count == 0)
                {
                    return NotFound("Not exists Sale of Types");
                }

                return Ok(typeOfProperties);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveTypeOfSalesViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var category = await _typeOfSalesService.GetByIdSaveViewModel(id);

                if (category == null)
                {
                    return NotFound("Not exists Sale of Types");
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
        public async Task<IActionResult> Create(SaveTypeOfSalesViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await _typeOfSalesService.Add(vm);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveTypeOfSalesViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, SaveTypeOfSalesViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await _typeOfSalesService.Update(vm, id);
                return Ok(vm);
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
                await _typeOfSalesService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
