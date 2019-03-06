using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.SearchParameters
{
    public class FiltroFuncionario
    {
        public string CodArea { get; set; }
        public string CodGeografico { get; set; }
        public string CodCentroCosto { get; set; }
        public string CodCargo { get; set; }
        public int? Rol { get; set; }

        public FiltroFuncionario(string codArea, string codGeografico, string codCentroCosto, string codCargo, string tipoRol)
        {
            CodArea = codArea;
            CodGeografico = codGeografico;
            CodCentroCosto = codCentroCosto;
            CodCargo = codCargo;
            if (!String.IsNullOrEmpty(tipoRol))
                Rol = Convert.ToInt32(tipoRol);

        }
    }
}
