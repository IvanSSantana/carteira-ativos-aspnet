using CarteiraAtivos.Models;
using CarteiraAtivos.Data;
using CarteiraAtivos.Services;
using CarteiraAtivos.Dtos;

namespace CarteiraAtivos.Repositories
{
    public class AtivoRepositorio : IAtivoRepositorio
    {
        private readonly DatabaseContext _DbContext;
        private readonly IApiFinanceiraService _apiFinanceiraService;

        public AtivoRepositorio(DatabaseContext bancoContext,
                    IApiFinanceiraService apiFinanceiraService)
        {
            _DbContext = bancoContext;
            _apiFinanceiraService = apiFinanceiraService; 
        }

      public List<AtivoModel> BuscarTodosAtivos(int usuarioId)
      {
            return _DbContext.Ativos.Where(u => u.LoginUsuarioId == usuarioId).ToList();
      }

        public async Task<AtivoModel> CadastrarAtivo(AtivoCreateDto ativoDto)
        {
            AtivoModel ativoModel = await _apiFinanceiraService.ObterDadosDoAtivo(ativoDto);
            _DbContext.Ativos.Add(ativoModel);
            await _DbContext.SaveChangesAsync();

            return ativoModel;
        }
    }
}