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

        public List<Clientes> FindAll()
        {
            return _context.Clientes.OrderBy(x => x.Name).ToList();
        }

        public async Task InsertAsync(Clientes obj)
        {
            //obj.Imovel = _context.Imovel.First();
            _context.Add(obj);
           await _context.SaveChangesAsync();
        }

        public Clientes FindById(int id)
        {
            return _context.Clientes.Include(obj => obj.Imovel).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Clientes.Find(id);
            _context.Clientes.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Clientes obj)
        {
            if (!_context.Clientes.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id não encontrado");
            }

            try { 
            _context.Update(obj);
            _context.SaveChanges();

            } catch(DbUpdateConcurrencyException e)
            
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
