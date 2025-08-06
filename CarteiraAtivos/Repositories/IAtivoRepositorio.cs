using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarteiraAtivos.Models;

namespace CarteiraAtivos.Repositories
{
    public interface IAtivoRepositorio
    {
        Task<AtivoModel> CadastrarAtivo(AtivoModel ativo);
        List<AtivoModel> BuscarTodosAtivos();
    }
}