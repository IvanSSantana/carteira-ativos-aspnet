using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarteiraAtivos.Models;

// Tabela de apoio para lógica de redefinição de senha
public class RedefinicaoSenhaModel
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public required string Token { get; set; }
    public DateTime ExpiraEm { get; set; }
    public bool Utilizado { get; set; } = false; // Valor padrão
}

