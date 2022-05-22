using Microsoft.EntityFrameworkCore;
using ProjetoSocialSolutions.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoSocialSolutions.Models
{
    [Table("Clientes")]

    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(Cpf), IsUnique = true)]

    public class Clientes
    {
        
        [Display(Name = "Código")]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nome")]
        [Column("Nome")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [Column("Email")]       
        public string Email { get; set; }

        [Required]
        [Display(Name = "Cpf")]
        [Column("CPF")]
        public string Cpf { get; set; }

        [Display(Name = "Status")]
        [Column("Status")]
        public StatusClientes Status { get; set; }

        public int ImovelId { get; set; }

        public Imovel Imovel { get; set; }

        public Clientes()
        {
        }

        public Clientes(int id, string name, string email, string cpf, StatusClientes status, Imovel imovel)
        {
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
            Status = status;
            Imovel = imovel;
        }
    }
}
