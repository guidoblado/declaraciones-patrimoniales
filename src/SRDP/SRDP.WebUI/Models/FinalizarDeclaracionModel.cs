using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SRDP.WebUI.Models
{
    public class FinalizarDeclaracionModel
    {
        public Guid DeclaracionID { get; set; }
        public string Mensaje { get; set; }
    }
}