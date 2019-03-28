using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases
{
    [Serializable]
    public class CabeceraEmailOutput
    {
        public string De { get; private set; }
        public string Para { get; private set; }
        public string ConCopia { get; private set; }
        public string Asunto { get; private set; }
        public DateTime FechaEnvio { get; private set; }

        public CabeceraEmailOutput(string de, string para, string conCopia, string asunto, DateTime fechaEnvio)
        {
            De = de;
            Para = para;
            ConCopia = conCopia;
            Asunto = asunto;
            FechaEnvio = fechaEnvio;
        }

        public string JsonSerialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static CabeceraEmailOutput JsonDeserialize(string text)
        {
            CabeceraEmailOutput result = JsonConvert.DeserializeObject<CabeceraEmailOutput>(text);
            return result;
        }
        
    }
}
