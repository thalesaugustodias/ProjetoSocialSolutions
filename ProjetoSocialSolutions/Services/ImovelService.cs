using Microsoft.EntityFrameworkCore;
using ProjetoSocialSolutions.Data;
using ProjetoSocialSolutions.Models;

namespace ProjetoSocialSolutions.Services
{
    public class ImovelService
    {
        private readonly Context _context;

        public ImovelService(Context context)
        {
            _context = context;
        }

        public async Task <List<Imovel>> FindAllAsync()
        {
            return await _context.Imovel.Where(x => x.Status == 0).OrderBy(x => x.Descricao).ToListAsync();
        }
    }
}
