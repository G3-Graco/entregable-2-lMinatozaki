using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PersonajeRepository : BaseRepository<Personaje>, IPersonajeRepository
    {
        public PersonajeRepository(AppDbContext context) : base(context){}

        public override async Task<IEnumerable<Personaje>> GetAllAsync()
        {
            return await base.dbSet.Include(x => x.habilidades).ToListAsync();
        }

    }
}