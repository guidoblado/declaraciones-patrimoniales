using System;

namespace SRDP.Persitence.Entities
{
    public class DeclaracionPatrimonial
    {
        public Guid ID { get; set; }
        public int Gestion { get; set; }
        public int FuncionarioID { get; set; }
        public string Estado { get; set; }
        public string Checksum { get; set; }
        public Guid DeclaracionAnteriorID { get; set; }
        public DateTime FechaLlenado { get; set; }


        public DeclaracionPatrimonial()
        {

        }
        
    }
}
