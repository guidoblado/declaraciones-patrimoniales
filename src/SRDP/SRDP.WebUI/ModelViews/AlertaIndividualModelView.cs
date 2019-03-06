using SRDP.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SRDP.WebUI.ModelViews
{
    public class AlertaIndividualModelView
    {
        public IList<AlertaGeneralModel> Data { get; set; }
        public ReglaAlertaParameterModel ReglaAlertaParameters { get; set; }
    }
}