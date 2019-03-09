using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SRDP.WebUI.Models
{
    public class UserProfileModel
    {
        [Display(Name = "Usuario")]
        public string UserName { get; set; }
        public int FuncionarioID { get; set; }
        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get; set; }
        public int EstadoID { get; set; }
        public string Estado { get; set; }
        public List<string> Roles { get; set; }
        [Display(Name = "Es Admin")]
        public bool IsAdmin { get; set; }
    }
}