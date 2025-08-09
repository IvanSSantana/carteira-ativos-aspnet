using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarteiraAtivos.Models;
using CarteiraAtivos.Data;
using CarteiraAtivos.Services;

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

        public async Task<AtivoModel> CadastrarAtivo(AtivoModel ativo)
        {
            AtivoModel ativoCompleto = await _apiFinanceiraService.ObterDadosDoAtivo(ativo);
            _DbContext.Ativos.Add(ativoCompleto);
            await _DbContext.SaveChangesAsync();

            return ativoCompleto;
        }
    }
}