﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate.Core.Application.ViewModels.PropertiesImprovements
{
    public class PropertiesImprovementsViewModel
    {
        public int PropertyId { get; set; }
        public int ImprovementId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
