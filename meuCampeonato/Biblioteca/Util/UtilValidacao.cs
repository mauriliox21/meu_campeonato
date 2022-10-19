using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Util
{
    public class UtilValidacao
    {
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
