using OnionVb02.Application.CqrsAndMediatr.Common;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Results.AppUserProfileResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Application.CqrsAndMediatr.CQRS.Queries.AppUserProfileQueries
{
    public class GetAllAppUserProfilesQuery : IQuery<List<AppUserProfileQueryResult>>
    {
    }
}

