using System.Text.Json;
using CarteiraAtivos.Models;
using CarteiraAtivos.Dtos;
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

        public async Task<AtivoModel> ObterDadosDoAtivo(AtivoCreateDto ativoModel)
        {
            // Configuração do token para iniciar requisições
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Env.GetString("API_KEY")}");

            // Busca dos dados
            var response = await _httpClient.GetAsync($"https://brapi.dev/api/quote/list?search={ativoModel.Ticker}&limit=1&page=1");

            // Caso não tenha sido sucesso, retorna uma exception HTTP
            response.EnsureSuccessStatusCode();

            // Transforma dados em Json
            var json = await response.Content.ReadAsStringAsync();

            // Contém uma só array, de onde devem ser resgatados valores
            // que serão convertidos no AtivoApiDto
            RaizJsonApi dados = JsonSerializer.Deserialize<RaizJsonApi>(json)!;

            if (dados == null || dados.stocks.Count == 0)
            {
                throw new Exception("A API retornou uma resposta nula.");
            }

            // Cria o model completo para retorno
            AtivoModel dadosAtivoModel = new()
            {
                Ticker = ativoModel.Ticker,
                Cotas = ativoModel.Cotas,
                ValorTotal = dados!.stocks[0].Cotacao * ativoModel.Cotas,
                Nome = dados.stocks[0].Nome,
                Tipo = dados.stocks[0].Tipo,
                Setor = dados.stocks[0].Setor,
                LoginUsuarioId = ativoModel.LoginUsuarioId
            };

            return dadosAtivoModel!;
        }
    }
}   