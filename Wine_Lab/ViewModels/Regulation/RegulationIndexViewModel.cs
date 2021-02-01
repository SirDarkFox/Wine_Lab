using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wine_Lab.Data.Models;

namespace Wine_Lab.ViewModels.Regulation
{
    public class RegulationIndexViewModel
    {
        public RegulationViewModel ChosenRegulation { get; set; }
        public IEnumerable<RegulationViewModel> Regulations { get; set; }
    }
}
