using SRDP.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SRDP.WebUI.ModelViews
{
    public class DeclaracionResumenView
    {
        public DeclaracionResumenModel DeclaracionVigente { get; set; }
        public List<DeclaracionResumenModel> DeclaracionesAnteriores { get; set; }
    }
}