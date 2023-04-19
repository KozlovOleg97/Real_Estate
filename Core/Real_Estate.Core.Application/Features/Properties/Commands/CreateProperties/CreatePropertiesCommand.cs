using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate.Core.Application.Features.Properties.Commands.CreateProperties
{
    public class CreatePropertiesCommand
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Code is required.")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Price is required.")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Land size is required.")]
        public int LandSize { get; set; }
        [Required(ErrorMessage = "Number of Rooms are required.")]
        public int NumberOfRooms { get; set; }
        [Required(ErrorMessage = "Number of Bathrooms is required.")]
        public int NumberOfBathrooms { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        public string ImagePathOne { get; set; }
        public string? ImagePathTwo { get; set; }
        public string? ImagePathThree { get; set; }
        public string? ImagePathFour { get; set; }
        [Required(ErrorMessage = "The list of Improvements are required.")]
        public List<int> Improvements { get; set; }
        [Required(ErrorMessage = "Property Type is required.")]
        public int TypeOfPropertyId { get; set; }
        [Required(ErrorMessage = "Type of Sale is required.")]
        public int TypeOfSaleId { get; set; }
    }
}
