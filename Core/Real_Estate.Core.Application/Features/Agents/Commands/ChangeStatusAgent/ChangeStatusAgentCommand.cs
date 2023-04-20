using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Real_Estate.Core.Application.Interfaces.Services;

namespace Real_Estate.Core.Application.Features.Agents.Commands.ChangeStatusAgent
{
    public class ChangeStatusAgentCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public bool Status { get; set; }
    }
    public class ChangeStatusAgentCommandHandler : IRequestHandler<ChangeStatusAgentCommand, bool>
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public ChangeStatusAgentCommandHandler(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }
        public async Task<bool> Handle(ChangeStatusAgentCommand command, 
            CancellationToken cancellationToken)
        {
            var users = await _accountService.GetAllUsers();

            var user = users.FirstOrDefault(x => x.Id == command.Id);

            if (user is null) 
                throw new Exception("The agent doesn't exist");

            var result = await _accountService.ChangesStatusUser(command.Id, command.Status);

            return result;
        }
    }
}
