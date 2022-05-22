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

        public List<Imovel> FindAll()
        {
            return _context.Imovel.Where(x => x.Status == 0).OrderBy(x => x.Descricao).ToList();
        }
    }
}
