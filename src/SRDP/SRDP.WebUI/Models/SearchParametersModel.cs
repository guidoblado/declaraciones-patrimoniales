using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SRDP.WebUI.Models
{
    public class SearchParametersModel
    {
        [Display(Name = "Areas")]
        public string CodArea { get; set; }
        public IEnumerable<SelectListItem> Areas { get; set; }

        [Display(Name = "Cargos")]
        public string CodCargo { get; set; }
        public IEnumerable<SelectListItem> Cargos { get; set; }

        [Display(Name = "Ubicacion Geográfica")]
        public string CodGeog { get; set; }
        public IEnumerable<SelectListItem> UbicacionesGeograficas { get; set; }

        [Display(Name = "Centros de Costo")]
        public string CodCentroCosto { get; set; }
        public IEnumerable<SelectListItem> CentrosCosto { get; set; }

        [Display(Name = "Roles")]
        public string TipoRol { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }

        public static IEnumerable<SelectListItem> GetSelectList(IReadOnlyDictionary<string, string> catalogo)
        {
            List<SelectListItem> selectListItems = catalogo.OrderBy(o => o.Value)
                .Select(n => new SelectListItem
                {
                    Value = n.Key,
                    Text = n.Value
                }).ToList();
            var firstItem = new SelectListItem()
            {
                Value = null,
                Text = "-- Todos --"
            };
            selectListItems.Insert(0, firstItem);
            return new SelectList(selectListItems, "Value", "Text");
        }
    }
}
