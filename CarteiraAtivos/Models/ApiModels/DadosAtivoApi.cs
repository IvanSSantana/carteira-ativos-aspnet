using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarteiraAtivos.Models.ApiModels
{
    public class DadosAtivoApi
    {
        public float? Close { get; set; } // Cotação atual

        public string? Name { get; set; } // Nome da empresa

        public string? Type { get; set; } // Ação, FII ou BDR

        public string? Sector { get; set; } // Energia, Petróleo etc.
    }
}