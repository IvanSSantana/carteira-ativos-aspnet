using CarteiraAtivos.Data;
using CarteiraAtivos.Models;

namespace CarteiraAtivos.Repositories;

public class RedefinicaoSenhaRepository : IRedefinicaoSenhaRepository
{
   // Injeção de dependência
   private readonly DatabaseContext _DbContext;

   public RedefinicaoSenhaRepository(DatabaseContext context)
   {
      _DbContext = context;
   }

   public void Criar(string email, string token)
   {
      RedefinicaoSenhaModel redefinicao = new()
      {
         Email = email,
         Token = token,
         ExpiraEm = DateTime.Now.AddMinutes(30) // Expira após 30 minutos
      };

      _DbContext.RedefinicoesSenha.Add(redefinicao);
      _DbContext.SaveChanges();
   }

   public RedefinicaoSenhaModel? BuscarPorToken(string token)
   {
      // Só busca por não utilizados ainda no prazo
      return _DbContext.RedefinicoesSenha
          .FirstOrDefault(r => r.Token == token && !r.Utilizado && r.ExpiraEm > DateTime.Now);
   }

   public void MarcarComoUtilizado(RedefinicaoSenhaModel redefinicao)
   {
      redefinicao.Utilizado = true;
      _DbContext.RedefinicoesSenha.Update(redefinicao);
      _DbContext.SaveChanges();
   }
}
