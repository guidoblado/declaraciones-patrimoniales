using SRDP.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRDP.WebUI.ModelViews
{
    public class EstadoGeneralModelView
    {
        public int AnioGestion { get; set; }
        public IList<EstadoGeneralModel> Data { get; set; }
        public SearchParametersModel SearchParameters { get; set; }
    }
}
