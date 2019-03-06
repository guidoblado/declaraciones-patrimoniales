using System;

namespace SRDP.Persitence.Entities
{
    internal class DeudaBancaria
    {
        public Guid ID { get; set; }
        public Guid DeclaracionID { get; set; }
        public string InstitucionFinanciera { get; set; }
        public decimal Monto { get; set; }
        public string  Tipo { get; set; }
    }
}
