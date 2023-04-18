﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Real_Estate.Core.Application.DTOs.Account;
using Real_Estate.Core.Application.Helpers;
using Real_Estate.Core.Application.Interfaces.Services;
using Real_Estate.Core.Application.ViewModels.Improvements;

namespace Real_Estate.Presentation.WebApp.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class ImprovementsController : Controller
    {
        private readonly IImprovementsService _improvementsService;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse currentlyUser;
        private readonly IMapper _mapper;

        public ImprovementsController(IUserService userService, IImprovementsService improvementsService, IAccountService accountService, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _improvementsService = improvementsService;
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
            currentlyUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            List<ImprovementsViewModel> improvementsList = await _improvementsService.GetAllViewModel();
            return View(improvementsList);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string ImprovementName, string ImprovementDescription)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("userVaidation", "Something was wrong");
                return RedirectToRoute(new { controller = "Improvements", action = "Index" });
            }

            SaveImprovementsViewModel improvementsSaveViewModel = new()
            {
                Name = ImprovementName,
                Description = ImprovementDescription
            };

            await _improvementsService.Add(improvementsSaveViewModel);

            return RedirectToRoute(new { controller = "Improvements", action = "Index" });
        }
    }
}