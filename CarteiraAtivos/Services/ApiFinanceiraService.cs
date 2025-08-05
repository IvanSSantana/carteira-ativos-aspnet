using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CarteiraAtivos.Models;
using CarteiraAtivos.Models.ApiModels;
using DotNetEnv;

namespace CarteiraAtivos.Services
{
    public class ApiFinanceiraService : IApiFinanceiraService
    {
        //  Injeção De Dependência:
        // funciona exatamente igual ao se conectar com um banco de dados
        private readonly HttpClient _httpClient;

        public ApiFinanceiraService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AtivoModel> ObterDadosDoAtivos(AtivoModel ativoModel)
        {   
            // Configuração do token para iniciar requisições
            Env.Load("./Environment/.env");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Env.GetString("API_KEY")}");

            // Busca dos dados
            var response = await client.GetAsync($"https://brapi.dev/api/quote/list?search={ativoModel.Ticker}&limit=1&page=1");

            // Caso não tenha sido sucesso, retorna uma exception HTTP
            response.EnsureSuccessStatusCode();

            // Transforma dados em Json
            var json = await response.Content.ReadAsStringAsync();
            var dados = JsonSerializer.Deserialize<DadosAtivoApi>(json);

            // Cria o model completo
            AtivoModel dadosAtivoModel = new AtivoModel
            {
                Ticker = ativoModel.Ticker,
                Cotas = ativoModel.Cotas,
                ValorTotal = dados!.Close * ativoModel.Cotas,
                Nome = dados!.Name,
                Tipo = dados!.Type,
                Setor = dados!.Sector
            };

            return dadosAtivoModel!;
        }
    }
}   