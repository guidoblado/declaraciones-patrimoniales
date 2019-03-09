using SRDP.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.UpdateProfile
{
    public class UpdateProfileUserCase : IUpdateProfileUserCase
    {
        private readonly IRolesUsuarioWriteOnlyRepository _rolesUsuarioWriteOnlyRepository;
        private readonly IRolesUsuarioReadOnlyRepository _rolesUsuarioReadOnlyRepository;

        public UpdateProfileUserCase(IRolesUsuarioWriteOnlyRepository rolesUsuarioWriteOnlyRepository, IRolesUsuarioReadOnlyRepository rolesUsuarioReadOnlyRepository)
        {
            _rolesUsuarioWriteOnlyRepository = rolesUsuarioWriteOnlyRepository;
            _rolesUsuarioReadOnlyRepository = rolesUsuarioReadOnlyRepository;
        }

        public async Task AddToAdmin(string userName, int funcionarioID)
        {
            await _rolesUsuarioWriteOnlyRepository.Add(userName, funcionarioID);
        }

        public async Task DeleteFromAdmin(string userName)
        {
            var adminUsers = await _rolesUsuarioReadOnlyRepository.GetAdminUsers();
            if (adminUsers.Count == 1)
                throw new ApplicationException("No se puede eliminar todos los usuarios Admin. Al menos uno debe quedar configurado.");

            await _rolesUsuarioWriteOnlyRepository.Delete(userName);
        }
    }
}
