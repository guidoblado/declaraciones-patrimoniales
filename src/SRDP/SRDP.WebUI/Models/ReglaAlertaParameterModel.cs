using SRDP.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRDP.WebUI.Models
{
    public class ReglaAlertaParameterModel
    {
        public Guid ID { get; set; }
        public ReglaAlertaModel ReglaAlerta { get; set; }
        public IEnumerable<SelectListItem> Reglas { get; set; }

        public static IEnumerable<SelectListItem> GetSelectList(ICollection<ReglaAlertaOutput> reglasAlerta)
        {
            if (reglasAlerta == null || reglasAlerta.Count ==0)
            {
                return new SelectList(new List<SelectListItem> { new SelectListItem { Value = Guid.Empty.ToString(), Text = "--" } });
            }
            List<SelectListItem> selectListItems = reglasAlerta
                .Select(n => new SelectListItem
                {
                    Value = n.ID.ToString(),
                    Text = n.Descripcion
                }).ToList();
            return new SelectList(selectListItems, "Value", "Text");
        }
    }
}