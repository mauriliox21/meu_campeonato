using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Util
{
    public class UtilValidacao
    {
        /// <summary>
        /// Verifica se um parâmetro passado é um inteiro valído, também valída se o parâmetro é nulo caso seja obrigatorio
        /// </summary>
        public static void ValidarParametroInteiro(object parametro, string nomeParametro, ref string mensagem, bool obrigatorio)
        {
            if(parametro == null && obrigatorio)
            {
                mensagem += $"O parâmetro \"{nomeParametro}\" é obrigatório. ";
            }
            else if (parametro != null)
            {
                if(!int.TryParse(parametro.ToString(), out int i))
                    mensagem += $"O parâmetro \"{nomeParametro}\" é inválido. ";
            }
        }

        /// <summary>
        /// Verifica se um parâmetro passado é uma string com o tamanho maximo passado por parâmetro da função, também valída se o parâmetro é nulo caso seja obrigatorio
        /// </summary>
        public static void ValidarParametroAlfanumerico(object parametro, string nomeParametro, ref string mensagem, int tamanhoMax, bool obrigatorio)
        {
            string paramString = "";
            if (parametro != null)
                paramString = parametro.ToString();

            if (string.IsNullOrEmpty(paramString) && obrigatorio)
            {
                mensagem += $"O parâmetro \"{nomeParametro}\" é obrigatório. ";
            }
            else if (!string.IsNullOrEmpty(paramString))
            {
                if (paramString.ToString().Length > tamanhoMax)
                    mensagem += $"O parâmetro \"{nomeParametro}\" não pode ter mais que {tamanhoMax} caracteres. ";
            }
        }
    }
}
