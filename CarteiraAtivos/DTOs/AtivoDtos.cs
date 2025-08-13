using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarteiraAtivos.Dtos
{
    public class AtivoApiDto
    {
        [JsonPropertyName("close")]
        public float? Cotacao { get; set; } // Cotação atual

        [JsonPropertyName("name")]
        public string? Nome { get; set; } // Nome da empresa

        [JsonPropertyName("type")]
        public string? Tipo { get; set; } // Ação, FII ou BDR

        [JsonPropertyName("sector")]
        public string? Setor { get; set; } // Energia, Petróleo etc.
    }


    public class RaizJsonApi
    {
        public List<AtivoApiDto> stocks { get; set; }
    }

    public class AtivoCreateDto
    {
        [Required(ErrorMessage = "O ticker da ação é obrigatório!")]
        [StringLength(6, ErrorMessage = "O ticker deve ter entre 5 e 6 caracteres.", MinimumLength = 5)]
        public required string Ticker { get; set; } // Sigla (ou como na API, símbolo) Exemplo: PETR4

        [Required(ErrorMessage = "A quantidade de cotas é obrigatória!")]
        [Range(1, 100, ErrorMessage = "O valor de cotas deve ser entre 1 e 100.")]
        public required int Cotas { get; set; } // Quantas cotas você tem da ação.

        public int Id { get; set; }
    }
}