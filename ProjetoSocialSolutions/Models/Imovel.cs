using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProjetoSocialSolutions.Enums;

namespace ProjetoSocialSolutions.Models
{
    [Table("Imovel")]
    public class Imovel
    {
       
        [Display(Name ="Código")]
        [Column("Id")]
        public int ImovelId { get; set; }

        [Display(Name = "Tipo")]
        [Column("Tipo")]
        public TipoImovel TipoImovel { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        [Column("Descricao")]
        public string Descricao { get; set; }

        [Required]
        [DisplayFormat(DataFormatString="{0:F2}")]
        [Display(Name = "Valor")]
        [Column("Valor")]
        public double Valor { get; set; }

        [Required]
        [Display(Name = "Status")]
        [Column("Status")]
        public StatusImovel Status { get; set; }

        public ICollection<Clientes> Clientes { get; set; } = new List<Clientes>();

        public Imovel()
        {
        }

        public Imovel(int id, string descricao, double valor, StatusImovel status, TipoImovel tipoImovel)
        {
            ImovelId = id;
            Descricao = descricao;
            Valor = valor;
            Status = status;
            TipoImovel = tipoImovel;
        }
    }
}
