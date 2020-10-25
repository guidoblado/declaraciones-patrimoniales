using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Persitence.Entities
{
    internal class DeclaracionResumen
    {
        public Guid ID { get; set; }
        public int Gestion { get; set; }
        public int FuncionarioID { get; set; }
        public int Estado { get; set; }
    }
}
