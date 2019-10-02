using SRDP.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRDP.WebUI.ModelViews
{
    public class HistoricoIndividualModelView
    {
        public SearchParametersModel SearchParameters { get; set; }
        public IList<HistoricoIndividualPivotModel> Data { get; set; }
        
    }
}
