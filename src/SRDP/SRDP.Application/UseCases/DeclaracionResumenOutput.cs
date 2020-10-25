using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases
{
    public class DeclaracionResumenOutput
    {

        public Guid DeclaracionID { get; }

        public int Gestion { get; }

        public string Estado { get; }

        public IReadOnlyList<DeclaracionResumenOutput> DeclaracionesAnteriores { get; }

        public DeclaracionResumenOutput(Guid declaracionID, int gestion, string estado, List<DeclaracionResumenOutput> declaracionesAnteriores)
        {
            DeclaracionID = declaracionID;
            Gestion = gestion;
            Estado = estado;
            DeclaracionesAnteriores = declaracionesAnteriores;
        }
    }
}
