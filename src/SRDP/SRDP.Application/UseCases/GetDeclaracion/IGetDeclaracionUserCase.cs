﻿using SRDP.Application.SearchParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetDeclaracion
{
    public interface IGetDeclaracionUserCase
    {
        Task<DeclaracionOutput> Execute(Guid declaracionID);
        Task<DeclaracionOutput> Execute(int gestion, int funcionarioID);
        Task<ICollection<DeclaracionOutput>> ExecuteList(int gestion);
    }
}
