using SRDP.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRDP.WebApp.ModelViews
{
    public class AlertaGeneralModelView
    {
        public List<AlertaGeneralModel> Data { get; set; }
        public AlertaParametersModel AlertaParameters { get; set; }
    }
}
