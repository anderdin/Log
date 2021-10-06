using Log.dal.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Log.api
{
    /// <summary>
    /// Classe estatica com metodos de ajuda para API e com controle de sessão em memoria
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Propriedade onde é guardada a lista de sessões ativas e seus respectivos tokens
        /// </summary>
        private static List<Sessao> SessoesOnline { get; set; } = new List<Sessao>();



        /// <summary>
        /// Metodo para gerar um texto em Hash MD5
        /// </summary>
        /// <param name="texto">Texto a ser convertido para MD5</param>
        /// <returns>Retorna uma string com o texto ja convertido para Md5</returns>
        public static string GerarMD5(string texto)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        /// <summary>
        /// Metodo para adicionar uma nova sessão a lista de sessões
        /// </summary>
        /// <param name="prSessao">Objeto "Sessao" com os dados apara a sessao</param>
        public static void AdicionarSessao(Sessao prSessao)
        {
            SessoesOnline.Add(prSessao);
            SessoesOnline.RemoveAll(x => x.Validade < DateTime.Now);
        }

        /// <summary>
        /// Metodo para retira uma sessão da lista de sessões ativas
        /// </summary>
        /// <param name="prToken">Sequencia de caracteres que identifica uma sessão</param>
        public static void RemoverSessao(string prToken)
        {
            SessoesOnline.RemoveAll(x => x.Token == prToken);
            SessoesOnline.RemoveAll(x => x.Validade < DateTime.Now);
        }

        /// <summary>
        /// Metodo para encontrar uma sessão na lista de sessões e validar se existe e se esta com periodo valido
        /// </summary>
        /// <param name="prToken">Sequencia de caracteres que identifica uma sessão</param>
        /// <returns></returns>
        public static bool ValidaSessao(string prToken)
        {
            SessoesOnline.RemoveAll(x => x.Validade < DateTime.Now);

            Sessao sessaoEncontrada = SessoesOnline.FirstOrDefault(x => x.Token == prToken);
            if(sessaoEncontrada != null)
            {
                SessoesOnline.FirstOrDefault(x => x.Token == prToken).Validade = DateTime.Now.AddMinutes(20);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Metodo para recuperar Usuario na sessao
        /// </summary>
        /// <param name="prToken">Sequencia de caracteres que identifica uma sessão</param>
        /// <returns>Retorna um objeto "Usuario" que se encontra na sessão</returns>
        public static Usuario BuscaUsuarioSessao(string prToken)
        {
            return SessoesOnline.FirstOrDefault(x => x.Token == prToken).Usuario;
        }
    }
}