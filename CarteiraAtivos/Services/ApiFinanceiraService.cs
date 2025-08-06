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
        Env.Load("./Environment/.env");
    }

        public async Task<AtivoModel> ObterDadosDoAtivos(AtivoModel ativoModel)
        {
            // Configuração do token para iniciar requisições
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Env.GetString("API_KEY")}");

            // Busca dos dados
                var response = await client.GetAsync($"https://brapi.dev/api/quote/list?search={ativoModel.Ticker}&limit=1&page=1");

            // Caso não tenha sido sucesso, retorna uma exception HTTP
            response.EnsureSuccessStatusCode();

            // Transforma dados em Json
            var json = await response.Content.ReadAsStringAsync();

            // Contém uma só array, de onde devem ser resgatados valores
            // que serão convertidos no StockModel
            RaizJsonModel dados = JsonSerializer.Deserialize<RaizJsonModel>(json)!;

            if (dados == null || dados.Stocks.Count == 0)
            {
                throw new Exception("A API retornou uma resposta nula.");
            }

            // Cria o model completo para retorno
            AtivoModel dadosAtivoModel = new()
            {
                Ticker = ativoModel.Ticker,
                Cotas = ativoModel.Cotas,
                ValorTotal = dados!.Stocks[0].Close * ativoModel.Cotas,
                Nome = dados.Stocks[0].Name,
                Tipo = dados.Stocks[0].Type,
                Setor = dados.Stocks[0].Sector
            };

            return dadosAtivoModel!;
        }
    }
}   