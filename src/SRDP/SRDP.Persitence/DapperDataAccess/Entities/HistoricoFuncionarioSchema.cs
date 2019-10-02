﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Persitence.Entities
{
    public class HistoricoFuncionarioSchema
    {
        public int FuncionarioID { get; set; }
        public string NombreCompleto { get; set; }
        public string CodCargo { get; set; }
        public string Cargo { get; set; }
        public string CodArea { get; set; }
        public string Area { get; set; }
        public string CodGeografico { get; set; }
        public string UbicacionGeografica { get; set; }
        public string CodCentroCosto { get; set; }
        public string CentroCosto { get; set; }
        public int TipoRol { get; set; }
        public string Rol { get; set; }
    }
}
