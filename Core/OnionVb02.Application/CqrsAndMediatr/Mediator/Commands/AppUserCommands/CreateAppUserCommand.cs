using MediatR;
using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Results.AppUserResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Application.CqrsAndMediatr.Mediator.Commands.AppUserCommands
{
    public class CreateAppUserCommand : ICommand<GetAppUserByIdQueryResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
