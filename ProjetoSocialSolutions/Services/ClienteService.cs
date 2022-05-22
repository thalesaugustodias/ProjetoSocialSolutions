using Microsoft.EntityFrameworkCore;
using ProjetoSocialSolutions.Data;
using ProjetoSocialSolutions.Models;
using ProjetoSocialSolutions.Services.Exceptions;

namespace ProjetoSocialSolutions.Services
{
    public class ClienteService
    {
        private readonly Context _context;

        public ClienteService(Context context)
        {
            _context = context;
        }

        public async Task <List<Clientes>> FindAllAsync()
        {
            return await _context.Clientes.Include(obj => obj.Imovel).OrderBy(x => x.Name).ToListAsync();
        }

        public async Task InsertAsync(Clientes obj)
        {            
           _context.Add(obj);
           await _context.SaveChangesAsync();
        }

        public async Task <Clientes> FindByIdAsync(int id)
        {
            return await _context.Clientes.Include(obj => obj.Imovel).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Clientes obj)
        {
            bool hasAny = await _context.Clientes.AnyAsync(x => x.Id == obj.Id);

            if (!hasAny)
            {
                throw new NotFoundException("Id não encontrado");
            }

            try { 
             _context.Update(obj);
            await _context.SaveChangesAsync();

            } catch(DbUpdateConcurrencyException e)
            
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
