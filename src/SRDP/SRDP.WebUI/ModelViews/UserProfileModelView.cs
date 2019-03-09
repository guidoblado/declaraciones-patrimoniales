using SRDP.WebUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SRDP.WebUI.ModelViews
{
    public class UserProfileModelView
    {
        public IList<UserProfileModel> Data { get; set; }
        [Display(Name = "Mostrar solo usuarios Admin")]
        public bool SoloAdmin { get; set; }
    }
}