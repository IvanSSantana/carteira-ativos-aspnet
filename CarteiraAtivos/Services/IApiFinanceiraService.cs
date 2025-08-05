using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarteiraAtivos.Models;

namespace CarteiraAtivos.Services
{
    public interface IApiFinanceiraService
    {
        Task<AtivoModel> ObterDadosDoAtivos(AtivoModel ativoModel);
    }
}