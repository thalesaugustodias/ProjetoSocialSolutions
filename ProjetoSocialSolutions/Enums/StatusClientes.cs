using System.ComponentModel.DataAnnotations;

namespace ProjetoSocialSolutions.Enums
{
    public enum StatusClientes
    {
        [Display(Name = "Cliente Ativo")]
        Ativo,
        [Display(Name = "Cliente Inativo")]
        Inativo
    }
}
