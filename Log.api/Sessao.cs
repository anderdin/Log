using Log.dal.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Log.api
{
    /// <summary>
    /// classe usada para a construção de uma nova sessão após confirmação de Logon
    /// </summary>
    public class Sessao
    {
        /// <summary>
        /// Data e Hora de validade na sessão, normalmente 20 minutos sendo renovada a cada validação
        /// </summary>
        public DateTime Validade { get; set; }
        /// <summary>
        /// Objeto com dados do Usuario que estará logado na sessão
        /// </summary>
        public Usuario Usuario { get; set; }
        /// <summary>
        /// Sequencia de caracteres que identifica a sessão
        /// </summary>
        public string Token { get; set; }


        /// <summary>
        /// Construtor que obriga a entrada dos dados necessários para abertura de sessão
        /// </summary>
        /// <param name="prUsuario">Objeto com dados do usuario autenticado</param>
        public Sessao(Usuario prUsuario)
        {
            this.Usuario = prUsuario;
            this.Validade = DateTime.Now.AddMinutes(20);
            this.Token = Helper.GerarMD5(prUsuario.Login + DateTime.Now.ToString() + prUsuario.Senha);
        }
    }
}