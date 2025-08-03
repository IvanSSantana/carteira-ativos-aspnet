using System.Security.Cryptography;
using System.Text;

namespace CarteiraAtivos.Helpers
{
    public static class Criptografia
    {
        public static string GerarHash(this string valor)
        // "this" permite que o método seja chamado diretamente em uma string
        // Exemplo: "Senha".GerarHash();
        {
            var sha256 = SHA256.Create();
            var encoding = new ASCIIEncoding();
            var bytes = encoding.GetBytes(valor);

            var hashBytes = sha256.ComputeHash(bytes);

            var strBuilder = new StringBuilder();

            foreach (var item in hashBytes)
            {
                strBuilder.Append(item.ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}