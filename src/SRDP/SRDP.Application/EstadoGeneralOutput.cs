using SRDP.Domain.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application
{
    public class EstadoGeneralOutput
    {
        public Guid DeclaracionID { get; private set; }
        public int FuncionarioID { get; private set; }
        public string NombreCompleto { get; private set; }
        public string Cargo { get; private set; }
        public string Area { get; private set; }
        public string UbicacionGeografica { get; private set; }
        public string CentroCosto { get; private set; }
        public string Rol { get; private set; }
        public string EstadoDeclaracion { get; private set; }

        private EstadoGeneralOutput() { }

        public EstadoGeneralOutput(Guid declaracionID, int funcionarioID, string nombreCompleto, string cargo, string area, string ubicaionGeografica,
            string centroDeCosto, string rol, string estadoDeclaracion)
        {
            DeclaracionID = declaracionID;
            FuncionarioID = funcionarioID;
            NombreCompleto = nombreCompleto;
            Cargo = cargo;
            Area = area;
            UbicacionGeografica = ubicaionGeografica;
            CentroCosto = centroDeCosto;
            Rol = rol;
            EstadoDeclaracion = estadoDeclaracion;
        }

        public EstadoGeneralOutput(EstadoGeneral estadoGeneral)
        {
            DeclaracionID = estadoGeneral.DeclaracionID;
            FuncionarioID = estadoGeneral.FuncionarioID;
            NombreCompleto = estadoGeneral.Funcionario.NombreCompleto.ToString();
            Cargo = estadoGeneral.Funcionario.Cargo;
            Area = estadoGeneral.Funcionario.Area;
            UbicacionGeografica = estadoGeneral.Funcionario.UbicacionGeografica;
            CentroCosto = estadoGeneral.Funcionario.CentroDeCosto;
            Rol = estadoGeneral.Funcionario.Rol;
            EstadoDeclaracion = estadoGeneral.Estado.ToString();
        }

    }
}
