using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarteiraAtivos.Models.ApiModels
{
    public class StockModel
    {
        [JsonPropertyName("close")]
        public float? Close { get; set; } // Cotação atual

        [JsonPropertyName("name")]
        public string? Name { get; set; } // Nome da empresa

        [JsonPropertyName("type")]
        public string? Type { get; set; } // Ação, FII ou BDR

        [JsonPropertyName("sector")]
        public string? Sector { get; set; } // Energia, Petróleo etc.
    }

    
    public class RaizJsonModel
    {
        [JsonPropertyName("stocks")]
        public List<StockModel> Stocks { get; set; } = new(); // Evita nulos
    }
}