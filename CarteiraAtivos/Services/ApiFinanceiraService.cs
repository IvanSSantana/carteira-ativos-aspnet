using System.Text.Json;
using CarteiraAtivos.Models;
using CarteiraAtivos.Dtos;
using DotNetEnv;
using CarteiraAtivos.Helpers;

namespace CarteiraAtivos.Services
{   
    public class ApiFinanceiraService : IApiFinanceiraService
    {
        //  Injeção De Dependência:
        // funciona exatamente igual ao se conectar com um banco de dados
        private readonly HttpClient _httpClient;
        private readonly ISessao _sessao;

        public ApiFinanceiraService(HttpClient httpClient, ISessao sessao)
        {
            _httpClient = httpClient;
            _sessao = sessao;
        }

        public async Task<AtivoModel> ObterDadosDoAtivo(AtivoCreateDto ativoDto)
        {
            // Configuração do token para iniciar requisições
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Env.GetString("API_KEY")}");

            // Busca dos dados
            var response = await _httpClient.GetAsync($"https://brapi.dev/api/quote/list?search={ativoDto.Ticker}&limit=1&page=1");

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
                Ticker = ativoDto.Ticker,
                Cotas = ativoDto.Cotas,
                ValorTotal = dados!.stocks[0].Cotacao * ativoDto.Cotas,
                Nome = dados.stocks[0].Nome,
                Tipo = dados.stocks[0].Tipo,
                Setor = dados.stocks[0].Setor,
                LoginUsuarioId = _sessao.VerificarSessaoLogin()!.Id
            };

            return dadosAtivoModel!;
        }
    }
}   