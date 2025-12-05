using OnionVb02.Application.CqrsAndMediatr.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Application.CqrsAndMediatr.CQRS.Commands.OrderCommands
{
    public class RemoveOrderCommand : ICommand
    {
        public int Id { get; set; }

        public RemoveOrderCommand(int id)
        {
            Id = id;
        }
    }
}

