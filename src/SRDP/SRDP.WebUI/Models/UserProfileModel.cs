using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SRDP.WebUI.Models
{
    public class UserProfileModel
    {
        public string UserName { get; set; }
        public int FuncionarioID { get; set; }
        public int GestionActual { get; set; }
        public List<string> Roles { get; set; }
    }
}