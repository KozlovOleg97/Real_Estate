﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Real_Estate.Core.Application.DTOs.Account;
using Real_Estate.Core.Application.Helpers;
using Real_Estate.Core.Application.Interfaces.Repositories;
using Real_Estate.Core.Application.Interfaces.Services;
using Real_Estate.Core.Application.ViewModels.Properties;
using Real_Estate.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate.Core.Application.Services
{
    public class PropertiesService : GenericService<SavePropertiesViewModel, 
        PropertiesViewModel, Properties>, IPropertiesService
    {
        private readonly IGenericRepositoryAsync<Properties> _repository;
        private readonly IPropertiesRepository _propertiesRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly AuthenticationResponse userviewModel;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly IImprovementsRepository _improvementsRepository;
        private readonly ITypeOfPropertiesRepository _typeOfPropertiesRepository;
        private readonly ITypeOfSalesRepository _typeOfSalesRepository;

        public PropertiesService(IGenericRepositoryAsync<Properties> repository, 
            IPropertiesRepository propertiesRepository, IHttpContextAccessor httpContextAccessor, 
            IMapper mapper, IAccountService accountService, 
            IImprovementsRepository improvementsRepository, 
            ITypeOfPropertiesRepository typeOfPropertiesRepository, 
            ITypeOfSalesRepository typeOfSalesRepository) : base(propertiesRepository, mapper)
        {
            _repository = repository;
            _propertiesRepository = propertiesRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _improvementsRepository = improvementsRepository;
            _typeOfPropertiesRepository = typeOfPropertiesRepository;
            _typeOfSalesRepository = typeOfSalesRepository;
            //userviewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _accountService = accountService;
        }

        public async Task<SaveAgentProfileViewModel> UpdateAgentProfile(SaveAgentProfileViewModel agentProfileViewModel)
        {
            var agentProfileToUpdate = _mapper.Map<UpdateAgentUserRequest>(agentProfileViewModel);
            //agentProfileToUpdate.UserName = userviewModel.UserName;


            var response = await _accountService.UpdateAgentUserByUserNameAsync(agentProfileToUpdate);
            return _mapper.Map<SaveAgentProfileViewModel>(response);
        }

        public async Task<SaveAgentProfileViewModel> GetAgentUserByUserNameAsync(string userName)
        {
            var agentUser = await _accountService.GetAgentUserByUserNameAsync(userName);
            return _mapper.Map<SaveAgentProfileViewModel>(agentUser);
        }

        public async Task<SavePropertiesViewModel> CustomAdd(SavePropertiesViewModel savePropertiesViewModel)
        {
            var records = await _repository.GetAllAsync();
            var exisCode = records.FirstOrDefault(x => 
                x.Code == savePropertiesViewModel.Code);

            if (exisCode is not null) 
                throw new Exception("The Code Exists.");

            var existImprovement = await _improvementsRepository.GetByIdAsync(savePropertiesViewModel.ImprovementsId);
            if (existImprovement is null) 
                throw new Exception("The specified improvement doesn't exist");

            var existTypeOfPropertie = await _typeOfPropertiesRepository.GetByIdAsync(
                savePropertiesViewModel.TypeOfPropertyId);
            if (existTypeOfPropertie is null) 
                throw new Exception("The specified property type doesn't exist.");

            var existTypeOfSales = await _typeOfSalesRepository.GetByIdAsync(
                savePropertiesViewModel.TypeOfSaleId);

            if (existTypeOfSales is null) throw new Exception(
                "The specified sale type doesn't exist.");

            if (savePropertiesViewModel.Price < 0) 
                throw new Exception("The price must be greater than 0.");

            if (savePropertiesViewModel.NumberOfBathrooms < 0) 
                throw new Exception("The number of bathrooms must be greater than 0.");

            if (savePropertiesViewModel.NumberOfRooms < 0) 
                throw new Exception("The number of rooms must be greater than 0.");

            var propertyEntity = _mapper.Map<Properties>(savePropertiesViewModel);
            await _repository.AddAsync(propertyEntity);

            return savePropertiesViewModel;
        }

        public async Task<List<PropertiesViewModel>> GetAllWithData()
        {
            var result = await _repository.GetAllWithIncludeAsync(new 
                List<string> { "Improvements", "TypeOfProperty", "TypeOfSale" });

            result.OrderByDescending(x => x.Created);

            return _mapper.Map<List<PropertiesViewModel>>(result);
        }

        public async Task<PropertiesViewModel> GetByIdWithData(int id)
        {
            var exists = await _repository.GetByIdAsync(id);

            if (exists is null) 
                throw new Exception("Property Doesn't exist.");

            var result = await _repository.GetAllWithIncludeAsync(new 
                List<string> { "Improvements", "TypeOfProperty", "TypeOfSale" });

            var data = result.FirstOrDefault(x => x.Id == id);

            return _mapper.Map<PropertiesViewModel>(data);
        }

        public async Task<PropertiesViewModel> GetByCode(string code)
        {
            var properties = await _repository.GetAllAsync();

            var property = properties.FirstOrDefault(x => x.Code == code);

            if (property is null) 
                throw new Exception("Property doesn't exist.");

            var result = await _repository.GetAllWithIncludeAsync(new 
                List<string> { "Improvements", "TypeOfProperty", "TypeOfSale" });

            return _mapper.Map<PropertiesViewModel>(property);
        }
    }
}