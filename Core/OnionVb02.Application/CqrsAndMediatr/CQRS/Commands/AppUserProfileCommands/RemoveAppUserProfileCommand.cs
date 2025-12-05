using OnionVb02.Application.CqrsAndMediatr.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Application.CqrsAndMediatr.CQRS.Commands.AppUserProfileCommands
{
    public class RemoveAppUserProfileCommand : ICommand
    {
        public int Id { get; set; }

        public RemoveAppUserProfileCommand(int id)
        {
            Id = id;
        }
    }
}

