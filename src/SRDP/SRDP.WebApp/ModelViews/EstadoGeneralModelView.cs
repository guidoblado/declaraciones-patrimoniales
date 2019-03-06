using SRDP.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRDP.WebApp.ModelViews
{
    public class EstadoGeneralModelView
    {
        public IList<EstadoGeneralModel> Data { get; set; }
        public SearchParametersModel SearchParameters { get; set; }
    }
}
