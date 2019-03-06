using SRDP.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.Repositories
{
    public interface IAlertaGeneralReadOnlyRepository
    {
        Task<ICollection<AlertaGeneralOutput>> GetFromGestion(int gestion);
    }
}
