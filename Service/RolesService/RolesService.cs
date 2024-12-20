﻿using Dto;
using Microsoft.EntityFrameworkCore;
using Repo;

namespace Service
{
    public class RolesService : IRolesService
    {
        private readonly IRoleRepository _roleRepo;
        public RolesService
            (
                IRoleRepository roleRepo
            )
        {
            _roleRepo = roleRepo;
        }

        public async Task<List<DropDownDto>> GetAllRolesDDAsync()
        {
            return await _roleRepo.GetQueyable().Select(x => new DropDownDto { Text = x.RoleName, Value = x.Id }).ToListAsync();
        }

        public async Task<List<DropDownDto>> GetDefaultRolesDDAsync()
        {
            var roles = await _roleRepo.GetDefaultRolesAsync();
            return roles.Select( x => new DropDownDto { Text = x.RoleName, Value = x.Id}).ToList();
        }
    }
}
