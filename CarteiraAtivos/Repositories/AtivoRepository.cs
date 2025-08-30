using CarteiraAtivos.Models;
using CarteiraAtivos.Data;
using CarteiraAtivos.Services;
using CarteiraAtivos.Dtos;
using CarteiraAtivos.Helpers;

namespace CarteiraAtivos.Repositories
{
    public class AtivoRepository : IAtivoRepository
    {
        private readonly DatabaseContext _DbContext;
        private readonly ISessao _sessao;

        public AtivoRepository(DatabaseContext bancoContext,
                    ISessao sessao)
        {
            _DbContext = bancoContext;
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

        public AtivoModel CadastrarAtivo(AtivoModel ativo)
        {
            _DbContext.Ativos.Add(ativo);
            _DbContext.SaveChanges();

            return ativo;
        }

        public AtivoModel EditarAtivo(AtivoModel ativo)
        {
            AtivoModel ativoDB = BuscarPorIdEUsuarioId(ativo.Id, _sessao.VerificarSessaoLogin()!.Id);

            if (ativoDB == null)
            {
                throw new System.Exception("Houve um erro na busca do ativo.");
            }

            ativoDB.Ticker = ativo.Ticker;
            ativoDB.Cotas = ativo.Cotas;
            ativoDB.ValorTotal = ativo.ValorTotal;

            _DbContext.Ativos.Update(ativoDB);
            _DbContext.SaveChanges();

            return ativoDB;
        }

        public AtivoModel BuscarPorTickerEUsuarioId(string ticker, int usuarioId)
        {
            return _DbContext.Ativos.FirstOrDefault(x => x.Ticker == ticker && x.LoginUsuarioId == usuarioId);
        }

        public bool DeletarAtivo(int ativoId)
        {
            AtivoModel ativoDB = BuscarPorIdEUsuarioId(ativoId, _sessao.VerificarSessaoLogin()!.Id);

            if (ativoDB == null)
            {
                throw new System.Exception("Houve um erro na busca do ativo.");
            }

            _DbContext.Ativos.Remove(ativoDB);
            _DbContext.SaveChanges();
            return true;
        }
   }
}