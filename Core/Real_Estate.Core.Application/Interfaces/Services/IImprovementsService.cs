using Real_Estate.Core.Application.ViewModels.Improvements;
using Real_Estate.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate.Core.Application.Interfaces.Services
{
    public interface IImprovementsService : 
        IGenericService<SaveImprovementsViewModel, ImprovementsViewModel, Improvements>
    {
    }
}
