using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarteiraAtivos.Dtos
{
    public class AtivoApiDto // Utilizado para requisições da API
    {
        [JsonPropertyName("close")]
        public float? Cotacao { get; set; } // Cotação atual

        [JsonPropertyName("name")]
        public string? Nome { get; set; } // Nome da empresa

        [JsonPropertyName("type")]
        public string? Tipo { get; set; } // Ação, FII ou BDR

        [JsonPropertyName("sector")]
        public string? Setor { get; set; } // Energia, Petróleo etc.

        [JsonPropertyName("stock")]
        public string? Ticker { get; set; }
    }


    public class RaizJsonApi // Utilizado para desserializar a resposta da API
    {
        public List<AtivoApiDto> stocks { get; set; }
    }

    public class AtivoCreateDto // Utilizado para o input de cadastro do usuário
    {
        [Required(ErrorMessage = "O ticker da ação é obrigatório!")]
        [StringLength(6, ErrorMessage = "O ticker deve ter entre 5 e 6 caracteres.", MinimumLength = 5)]
        public required string Ticker { get; set; } // Sigla (ou como na API, símbolo) Exemplo: PETR4

        [Required(ErrorMessage = "A quantidade de cotas é obrigatória!")]
        [Range(1, 100, ErrorMessage = "O valor de cotas deve ser entre 1 e 100.")]
        public required int Cotas { get; set; } // Quantas cotas você comprará da ação.

        public int Id { get; set; }
    }

    public class PrecoHistoricoDto
    {
        [JsonPropertyName("date")]
        public long Data { get; set; } // timestamp UNIX (pode ser convertido para DateTime/Date depois)

        [JsonPropertyName("open")]
        public decimal PrecoAbertura { get; set; }

        [JsonPropertyName("close")]
        public decimal PrecoFechamento { get; set; }

        public decimal PrecoMedio => (PrecoAbertura + PrecoFechamento) / 2; // Preço a ser utilizado
    }

    public class AtivoHistoricoDto // Utilizado para filtrar a busca do histórico de preço
    {
        [JsonPropertyName("symbol")]
        public string? Ticker { get; set; }

        [JsonPropertyName("usedInterval")]
        public string? Intervalo { get; set; }

        [JsonPropertyName("usedRange")]
        public string? Range { get; set; }

        [JsonPropertyName("historicalDataPrice")]
        public List<PrecoHistoricoDto>? HistoricoPreco { get; set; }
    }

    public class RaizHistoricoJson // Utilizado para desserializar a resposta da API
    {
        public List<AtivoHistoricoDto> results { get; set; }
    }
}