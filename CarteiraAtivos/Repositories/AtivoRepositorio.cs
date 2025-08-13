using CarteiraAtivos.Models;
using CarteiraAtivos.Data;
using CarteiraAtivos.Services;
using CarteiraAtivos.Dtos;
using CarteiraAtivos.Helpers;

namespace CarteiraAtivos.Repositories
{
    public class AtivoRepositorio : IAtivoRepositorio
    {
        private readonly DatabaseContext _DbContext;
        private readonly IApiFinanceiraService _apiFinanceiraService;
        private readonly ISessao _sessao;

        public AtivoRepositorio(DatabaseContext bancoContext,
                    IApiFinanceiraService apiFinanceiraService, ISessao sessao)
        {
            _DbContext = bancoContext;
            _apiFinanceiraService = apiFinanceiraService;
            _sessao = sessao;
        }

        public List<AtivoModel> BuscarTodosAtivos(int usuarioId)
        {
            return _DbContext.Ativos.Where(u => u.LoginUsuarioId == usuarioId).ToList();
        }
      
        public AtivoModel BuscarPorId(int ativoId)
        {
            return _DbContext.Ativos.FirstOrDefault(x => x.Id == ativoId);
        }

        public AtivoModel BuscarPorIdEUsuarioId(int ativoId, int usuarioId)
        {
            return _DbContext.Ativos.FirstOrDefault(x => x.Id == ativoId && x.LoginUsuarioId == usuarioId);
        }

        public async Task<AtivoModel> CadastrarAtivo(AtivoCreateDto ativoDto)
        {
            AtivoModel ativoModel = await _apiFinanceiraService.ObterDadosDoAtivo(ativoDto);
            ativoModel.LoginUsuarioId = _sessao.VerificarSessaoLogin()!.Id;
            _DbContext.Ativos.Add(ativoModel);
            await _DbContext.SaveChangesAsync();

            return ativoModel;
        }

      public async Task<AtivoModel> EditarAtivo(AtivoCreateDto ativo)
        {
            AtivoModel ativoDB = BuscarPorIdEUsuarioId(ativo.Id, _sessao.VerificarSessaoLogin()!.Id);

            if (ativoDB == null)
            {
                throw new System.Exception("Houve um erro na busca do ativo.");
            }

            AtivoModel ativoAtualizado = await _apiFinanceiraService.ObterDadosDoAtivo(ativo);

            ativoDB.Ticker = ativo.Ticker;
            ativoDB.Cotas = ativo.Cotas;
            ativoDB.ValorTotal = ativoAtualizado.ValorTotal;

            _DbContext.Ativos.Update(ativoDB);
            _DbContext.SaveChanges();

            return ativoDB;
        }

        public AtivoModel BuscarPorTickerEUsuarioId(string ticker, int usuarioId)
        {
            return _DbContext.Ativos.FirstOrDefault(x => x.Ticker == ticker && x.LoginUsuarioId == usuarioId);
        }

   }
}