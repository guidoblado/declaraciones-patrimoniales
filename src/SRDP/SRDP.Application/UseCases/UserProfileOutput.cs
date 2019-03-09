using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases
{
    public class UserProfileOutput
    {
        public string UserName { get; private set; }
        public int FuncionarioID { get; private set; }
        public string NombreCompleto { get; private set; }
        public int EstadoID { get; private set; }
        public string Estado { get; private set; }
        public List<string> Roles { get; private set; }
        public bool IsAdmin
        {
            get
            {
                return Roles.Contains("Administrador");
            }
        }

        private UserProfileOutput()
        {
            Roles = new List<string>();
        }

        public UserProfileOutput(string userName, int funcionarioID, string nombreCompleto, int estadoID, string estado)
        {
            UserName = userName;
            FuncionarioID = funcionarioID;
            NombreCompleto = nombreCompleto;
            EstadoID = estadoID;
            Estado = estado;
            Roles = new List<string>();
        }

        public static UserProfileOutput LoadRoles(string userName, int funcionarioID, string nombreCompleto, int estadoID, string estado, List<string> roles)
        {
            return new UserProfileOutput()
            {
                UserName = userName,
                FuncionarioID = funcionarioID,
                NombreCompleto = nombreCompleto,
                EstadoID = estadoID,
                Estado = estado,
                Roles = roles,
            };
        }

       
    }
}
