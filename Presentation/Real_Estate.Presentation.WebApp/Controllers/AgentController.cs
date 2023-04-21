﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Real_Estate.Core.Application.DTOs.Account;
using Real_Estate.Core.Application.Helpers;
using Real_Estate.Core.Application.Interfaces.Services;
using Real_Estate.Core.Application.ViewModels.Improvements;
using Real_Estate.Core.Application.ViewModels.Properties;
using System.Runtime.CompilerServices;

namespace Real_Estate.Presentation.WebApp.Controllers
{
    //[Authorize(Roles = "Agent")]
    public class AgentController : Controller
    {
        private readonly IPropertiesService _propertiesService;
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse userviewModel;
        private readonly IMapper _mapper;
        private readonly ITypeOfPropertiesService _typeOfPropertiesService;
        private readonly ITypeOfSalesService _typeOfSalesService;
        private readonly IImprovementsService _improvementsService;

        public AgentController(IPropertiesService propertiesService, IAccountService accountService, 
            IHttpContextAccessor httpContextAccessor, IMapper mapper, 
            ITypeOfPropertiesService typeOfPropertiesService, IImprovementsService improvementsService, 
            ITypeOfSalesService typeOfSalesService)
        {
            _propertiesService = propertiesService;
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
            userviewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _mapper = mapper;
            _typeOfPropertiesService = typeOfPropertiesService;
            _improvementsService = improvementsService;
            _typeOfSalesService = typeOfSalesService;
        }
        public async Task<IActionResult> Index()
        {
            var properties = await _propertiesService.GetAllByAgentIdWithInclude(
                userviewModel.Id);

            return View(properties);
        }

        public async Task<IActionResult> GetAll()
        {
            var properties = await _propertiesService.GetAllByAgentIdWithInclude(userviewModel.Id);
            return View(properties);
        }

        public async Task<IActionResult> Create()
        {
            SavePropertiesViewModel vm = new SavePropertiesViewModel();
            vm.TypeOfProperties = await _typeOfPropertiesService.GetAllViewModel();
            vm.TypeOfSales = await _typeOfSalesService.GetAllViewModel();
            vm.Improvements = await _improvementsService.GetAllViewModel();
            return View("SaveProperty", vm);

        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePropertiesViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.TypeOfProperties = await _typeOfPropertiesService.GetAllViewModel();
                vm.TypeOfSales = await _typeOfSalesService.GetAllViewModel();
                vm.Improvements = await _improvementsService.GetAllViewModel();
                return View("SaveProperty", vm);
            }
            vm.TypeOfProperties = await _typeOfPropertiesService.GetAllViewModel();
            vm.TypeOfSales = await _typeOfSalesService.GetAllViewModel();
            vm.Improvements = await _improvementsService.GetAllViewModel();

            vm.AgentId = userviewModel.Id;

            vm.Code = CodeGenerator.PropertyCodeGenerator();

            List<ImprovementsViewModel> improvementsList = new List<ImprovementsViewModel>();
            foreach (var item in vm.ImprovementsId)
            {
                improvementsList.Add(_mapper.Map<ImprovementsViewModel>(await _improvementsService.GetByIdSaveViewModel(item)));
            }


            SavePropertiesViewModel savePropertiesVmAdded = await _propertiesService.Add(vm);

            if (savePropertiesVmAdded != null && savePropertiesVmAdded.Id != 0)
            {
                savePropertiesVmAdded.ImagePathOne = UploadImagesHelper.UploadPropertyImage(
                    vm.ImageFileOne, savePropertiesVmAdded.Id);

                if (vm.ImageFileTwo != null)
                {
                    savePropertiesVmAdded.ImagePathTwo = UploadImagesHelper.UploadPropertyImage(
                        vm.ImageFileTwo, savePropertiesVmAdded.Id);
                }

                else if (vm.ImageFileThree != null)
                {
                    savePropertiesVmAdded.ImagePathThree = UploadImagesHelper.UploadPropertyImage(
                        vm.ImageFileThree, savePropertiesVmAdded.Id);

                }

                else if (vm.ImageFileFour != null)
                {
                    savePropertiesVmAdded.ImagePathFour = UploadImagesHelper.UploadPropertyImage(
                        vm.ImageFileFour, savePropertiesVmAdded.Id);
                }
            }

            savePropertiesVmAdded.Improvements = improvementsList;

            await _propertiesService.AddImprovementsAsync(savePropertiesVmAdded);

            await _propertiesService.Update(savePropertiesVmAdded, savePropertiesVmAdded.Id);

            return RedirectToRoute(new { controller = "Agent", action = "GetAll" });

        }
        public async Task<IActionResult> Edit(int id)
        {
            var vm = await _propertiesService.GetByIdWithInclude(id);

            vm.TypeOfProperties = await _typeOfPropertiesService.GetAllViewModel();
            vm.TypeOfSales = await _typeOfSalesService.GetAllViewModel();
            ViewBag.AllImprovements = await _improvementsService.GetAllViewModel();

            return View("SaveProperty", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePropertiesViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.TypeOfProperties = await _typeOfPropertiesService.GetAllViewModel();
                vm.TypeOfSales = await _typeOfSalesService.GetAllViewModel();
                return View("SaveProperty", vm);

            }

            SavePropertiesViewModel savePropertiesVmToUpdate = await _propertiesService.GetByIdWithInclude(vm.Id);

            if (vm.ImageFileOne != null)
            {
                if (savePropertiesVmToUpdate.ImagePathOne == null || savePropertiesVmToUpdate.ImagePathOne == "")
                {
                    savePropertiesVmToUpdate.ImagePathOne = UploadImagesHelper.UploadPropertyImage(vm.ImageFileOne, savePropertiesVmToUpdate.Id);
                }

                else if (savePropertiesVmToUpdate.ImagePathOne != null && savePropertiesVmToUpdate.ImagePathOne != "")
                {
                    savePropertiesVmToUpdate.ImagePathOne = UploadImagesHelper.UploadPropertyImage(vm.ImageFileOne, savePropertiesVmToUpdate.Id, true, savePropertiesVmToUpdate.ImagePathOne);
                }
            }

            else if (vm.ImageFileTwo != null)
            {
                if (savePropertiesVmToUpdate.ImagePathTwo == null || savePropertiesVmToUpdate.ImagePathTwo == "")
                {
                    savePropertiesVmToUpdate.ImagePathTwo = UploadImagesHelper.UploadPropertyImage(vm.ImageFileTwo, savePropertiesVmToUpdate.Id);
                }

                else if (savePropertiesVmToUpdate.ImagePathTwo != null && savePropertiesVmToUpdate.ImagePathTwo != "")
                {
                    savePropertiesVmToUpdate.ImagePathTwo = UploadImagesHelper.UploadPropertyImage(vm.ImageFileTwo, savePropertiesVmToUpdate.Id, true, savePropertiesVmToUpdate.ImagePathTwo);
                }
            }

            else if (vm.ImageFileThree != null)
            {
                if (savePropertiesVmToUpdate.ImagePathThree == null || savePropertiesVmToUpdate.ImagePathThree == "")
                {
                    savePropertiesVmToUpdate.ImagePathThree = UploadImagesHelper.UploadPropertyImage(vm.ImageFileThree, savePropertiesVmToUpdate.Id);
                }

                else if (savePropertiesVmToUpdate.ImagePathThree != null && savePropertiesVmToUpdate.ImagePathThree != "")
                {
                    savePropertiesVmToUpdate.ImagePathThree = UploadImagesHelper.UploadPropertyImage(vm.ImageFileThree, savePropertiesVmToUpdate.Id, true, savePropertiesVmToUpdate.ImagePathThree);
                }
            }

            else if (vm.ImageFileFour != null)
            {
                if (savePropertiesVmToUpdate.ImagePathFour == null || savePropertiesVmToUpdate.ImagePathFour == "")
                {
                    savePropertiesVmToUpdate.ImagePathFour = UploadImagesHelper.UploadPropertyImage(vm.ImageFileFour, savePropertiesVmToUpdate.Id);
                }

                else if (savePropertiesVmToUpdate.ImagePathFour != null && savePropertiesVmToUpdate.ImagePathFour != "")
                {
                    savePropertiesVmToUpdate.ImagePathFour = UploadImagesHelper.UploadPropertyImage(vm.ImageFileFour, savePropertiesVmToUpdate.Id, true, savePropertiesVmToUpdate.ImagePathFour);
                }
            }

            List<ImprovementsViewModel> improvementsList = new List<ImprovementsViewModel>();
            foreach (var item in vm.ImprovementsId)
            {
                improvementsList.Add(_mapper.Map<ImprovementsViewModel>(await _improvementsService.GetByIdSaveViewModel(item)));
            }

            savePropertiesVmToUpdate.Improvements = improvementsList;
            savePropertiesVmToUpdate.Price = vm.Price;
            savePropertiesVmToUpdate.LandSize = vm.LandSize;
            savePropertiesVmToUpdate.NumberOfRooms = vm.NumberOfRooms;
            savePropertiesVmToUpdate.NumberOfBathrooms = vm.NumberOfBathrooms;
            savePropertiesVmToUpdate.Description = vm.Description;
            savePropertiesVmToUpdate.TypeOfPropertyId = vm.TypeOfPropertyId;
            savePropertiesVmToUpdate.TypeOfSaleId = vm.TypeOfSaleId;
            savePropertiesVmToUpdate.ImprovementsId = vm.ImprovementsId;

            await _propertiesService.UpdatePropertyWithImprovementsAsync(savePropertiesVmToUpdate, savePropertiesVmToUpdate.Id);
            return RedirectToRoute(new { controller = "Agent", action = "GetAll" });

        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _propertiesService.GetByIdWithInclude(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _propertiesService.Delete(id);
            UploadImagesHelper.DeletePropertyImage(id);
            return RedirectToRoute(new { controller = "Agent", action = "GetAll" });
        }

        public async Task<IActionResult> MyProfile()
        {
            return View("UpdateAgentProfile", await _propertiesService.GetAgentUserByUserNameAsync(
                userviewModel.UserName));
        }

        [HttpPost]
        public async Task<IActionResult> MyProfile(SaveAgentProfileViewModel agentProfileViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("UpdateAgentProfile", await _propertiesService.GetAgentUserByUserNameAsync(
                    userviewModel.UserName));

            }

            SaveAgentProfileViewModel agentProfile = await _propertiesService.GetAgentUserByUserNameAsync(
                userviewModel.UserName);

            if (agentProfileViewModel.File != null)
            {
                if (agentProfile.ImagePath == null || agentProfile.ImagePath == "")
                {
                    agentProfileViewModel.ImagePath = UploadImagesHelper.UploadAgentUserImage(
                        agentProfileViewModel.File, userviewModel.UserName);
                }

                else if (agentProfile.ImagePath != null && agentProfile.ImagePath != "")
                {
                    agentProfileViewModel.ImagePath = UploadImagesHelper.UploadAgentUserImage(
                        agentProfileViewModel.File, userviewModel.UserName, true, agentProfile.ImagePath);
                }
            }

            var result = await _propertiesService.UpdateAgentProfile(
                agentProfileViewModel);

            if (result.HasError)
            {
                return View("UpdateAgentProfile", await _propertiesService.GetAgentUserByUserNameAsync(
                    userviewModel.UserName));
            }

            return RedirectToRoute(new { controller = "Agent", action = "Index" });

        }
    }
}
