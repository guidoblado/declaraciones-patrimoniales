using SRDP.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRDP.WebUI.ModelViews
{
    public class AlertaGeneralModelView
    {
        public List<AlertaGeneralModel> Data { get; set; }
        public AlertaParametersModel AlertaParameters { get; set; }
    }
}
