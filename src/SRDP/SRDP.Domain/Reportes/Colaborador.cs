using SRDP.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.Reportes
{
    public class Colaborador
    {
        public int FuncionarioID { get; private set; }
        public NombreCompleto NombreCompleto { get; private set; }
        public string CodCargo  { get; private set; }
        public string Cargo  { get; private set; }
        public string CodArea { get; private set; }
        public string Area { get; private set; }
        public string CodUbicacionGeografica { get; private set; }
        public string UbicacionGeografica { get; private set; }
        public string CodCentroDeCosto { get; private set; }
        public string CentroDeCosto { get; private set; }
        public int TipoRol { get; private set; }
        public string Rol { get; private set; }

        #region Constructors
        private Colaborador() { }

        public Colaborador(int funcionarioID, NombreCompleto nombreCompleto, string codCargo, string cargo, 
            string codArea, string area, string codUbicacionGeografica,  string ubicacionGeografica,
            string codCentroCosto, string centroDeCosto, int tipoRol, string rol)
        {
            FuncionarioID = funcionarioID;
            NombreCompleto = nombreCompleto;
            CodCargo = codCargo;
            Cargo = cargo;
            CodArea = codArea;
            Area = area;
            CodUbicacionGeografica = codUbicacionGeografica;
            UbicacionGeografica = ubicacionGeografica;
            CodCentroDeCosto = codCentroCosto;
            CentroDeCosto = centroDeCosto;
            TipoRol = tipoRol;
            Rol = rol;
        }
        #endregion

        public static Colaborador Load(int funcionarioID, NombreCompleto nombreCompleto, string codCargo, string cargo,
            string codArea, string area, string codUbicacionGeografica, string ubicacionGeografica,
            string codCentroCosto, string centroDeCosto, int tipoRol, string rol)
        {
            var colaborador = new Colaborador
            {
                FuncionarioID = funcionarioID,
                NombreCompleto = nombreCompleto,
                CodCargo = codCargo,
                Cargo = cargo,
                CodArea = codArea, 
                Area = area,
                CodUbicacionGeografica = codUbicacionGeografica,
                UbicacionGeografica = ubicacionGeografica,
                CodCentroDeCosto = codCentroCosto,
                CentroDeCosto = centroDeCosto,
                TipoRol = tipoRol,
                Rol = rol
            };
            return colaborador;
        }
    }
}
