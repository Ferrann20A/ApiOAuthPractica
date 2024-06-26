﻿using ApiOAuthPractica.Data;
using ApiOAuthPractica.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiOAuthPractica.Repositories
{
    public class RepositoryDoctores
    {
        private DoctoresContext context;

        public RepositoryDoctores(DoctoresContext context)
        {
            this.context = context;
        }

        public async Task<List<Doctor>> GetDoctoresAsync()
        {
            return await this.context.Doctores.ToListAsync();
        }

        public async Task<Doctor> FindDoctorAsync(int id)
        {
            return await this.context.Doctores.FirstOrDefaultAsync(x => x.IdDoctor == id);
        }

        public async Task<Doctor> LogInDoctorAsync(string apellido, int iddoctor)
        {
            return await this.context.Doctores.Where(x => x.Apellido == apellido
                && x.IdDoctor == iddoctor).FirstOrDefaultAsync();
        }
    }
}
