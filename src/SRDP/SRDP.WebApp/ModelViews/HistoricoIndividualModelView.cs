using SRDP.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRDP.WebApp.ModelViews
{
    public class HistoricoIndividualModelView
    {
        public SearchParametersModel SearchParameters { get; set; }
        public IList<HistoricoIndividualModel> Data { get; set; }

        public int GestionAnterior
        {
            get {
                if (Data == null || Data.Count == 0 ) return 0;
                return Data[0].DepositoGestionAnterior;
            }
        }

        public int GestionActual
        {
            get
            {
                if (Data == null || Data.Count == 0) return 0;
                return Data[0].DepositoGestionActual;
            }
        }
    }
}
