using SRDP.Application.Repositories;
using SRDP.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetRolesUsuario
{
    public class GetRolesUsuarioUserCase : IGetRolesUsuarioUserCase
    {
        private IRolesUsuarioReadOnlyRepository _rolesUsuarioReadOnlyRepository;

        public GetRolesUsuarioUserCase(IRolesUsuarioReadOnlyRepository rolesUsuarioReadOnlyRepository)
        {
            _rolesUsuarioReadOnlyRepository = rolesUsuarioReadOnlyRepository;
        }

        public async Task<List<string>> Execute(string username)
        {
            var adUser = AdAccount.For(username).Name;

            var outputList = await _rolesUsuarioReadOnlyRepository.Get(adUser);
            return outputList;
        }

        public List<string> ExecuteSync(string username)
        {
            var task = Task.Run(async () => {
                return await _rolesUsuarioReadOnlyRepository.Get(username);
            });
            return task.Result;
        }
    }
}
