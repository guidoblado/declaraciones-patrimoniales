using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Persitence.Entities
{
    public class FuncionarioUsuario
    {
        public int FuncionarioID { get; set; }
        public string NombreUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int EstadoID { get; set; }
        public string Estado { get; set; }
        public string Email { get; set; }
    }
}
