using System.ComponentModel.DataAnnotations;

// Model responsável por armazenar os dados dos ativos a serem cadastrados
namespace CarteiraAtivos.Models
{
   public class AtivoModel
   {
      [Key]
      public int Id { get; set; }

      [Required(ErrorMessage="O ticker da ação é obrigatório!")]
      [StringLength(5, ErrorMessage = "O ticker deve ter entre 5 caracteres.")]
      public required string Ticker { get; set; } // Sigla (ou como na API, símbolo) Exemplo: PETR4

      [Required(ErrorMessage="A quantidade de cotas é obrigatória!")]
      [Range(1, 100, ErrorMessage="O valor de cotas deve ser entre 1 e 100.")]
      public required int Cotas { get; set; } // Quantas cotas você tem da ação.

      // Os valores abaixo não serão inseridos pelo usuário, mas resgatados pela API   
      public float? ValorTotal { get; set; } // Valor total em R$ do usuário

      public string? Nome { get; set; } // Nome copleto da empresa

      public string? Tipo { get; set; } // Ação, FII ou BDR

      public string? Setor { get; set; } // Energia, Petróleo etc.
   }
}