using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Real_Estate.Core.Application.DTOs.Account;
using Real_Estate.Core.Application.Interfaces.Services;

namespace Real_Estate.Core.Application.Features.Accounts.Queries.Authenticate
{
    public class AuthenticateUserQuery : IRequest<AuthenticationResponse>
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password s required.")]
        public string Password { get; set; }
    }

    public class AuthenticateUserQueryHandler : IRequestHandler<AuthenticateUserQuery, 
        AuthenticationResponse>
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AuthenticateUserQueryHandler(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> Handle(AuthenticateUserQuery query,
            CancellationToken cancellationToken)
        {
            var data = _mapper.Map<AuthenticationRequest>(query);
            return await _accountService.AuthenticateAsync(data);
        }
    }
}
