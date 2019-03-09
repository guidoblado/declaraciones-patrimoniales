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
        public List<string> Roles { get; private set; }

        public UserProfileOutput(string userName, int funcionarioID, List<string> roles)
        {
            UserName = userName;
            FuncionarioID = funcionarioID;
            Roles = roles;
        }
    }
}
