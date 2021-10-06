using Log.api.Models;
using Log.dal;
using Log.dal.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Log.api.Controllers
{
    /// <summary>
    /// Classe de endpoint da API para processos de autenticação de usuario
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        /// <summary>
        /// Metodo para realizar Logon na API
        /// </summary>
        /// <param name="prUsuario">Objeto usuario para receber os dados de Login</param>
        /// <returns>Retorna um objeto de "Resposta" e um  token de acesso em caso de sucesso</returns>
        [Route("api/Login/Logon")]
        public Resposta Logon(Usuario prUsuario)
        {
            if(string.IsNullOrEmpty(prUsuario.Login) || string.IsNullOrEmpty(prUsuario.Senha))
            {
                return new Resposta(false, "Dados de Login e Senha são obrigatorios");
            }

            prUsuario.Senha = Helper.GerarMD5(prUsuario.Senha);

            HelperConexao conn = new HelperConexao();

            List<SqlParameter> listaParametro = new List<SqlParameter>();
            listaParametro.Add(new SqlParameter("login", prUsuario.Login));
            listaParametro.Add(new SqlParameter("senha", prUsuario.Senha));

            DataTable dt = conn.ExecutarComandoRetorno("select * from usuario where login=@login and senha=@senha", listaParametro.ToArray());

            if(dt.Rows.Count > 0)
            {
                prUsuario.Nome = dt.Rows[0].ItemArray[1].ToString();
                prUsuario.UsuarioId = Int32.Parse(dt.Rows[0].ItemArray[0].ToString());

                Sessao sessaoNova = new Sessao(prUsuario);
                
                Helper.AdicionarSessao(sessaoNova);

                listaParametro.Clear();
                listaParametro.Add(new SqlParameter("usuario_id", prUsuario.UsuarioId));
                listaParametro.Add(new SqlParameter("data", DateTime.Now));
                listaParametro.Add(new SqlParameter("ip", HttpContext.Current.Request.UserHostAddress));

                conn = new HelperConexao();
                conn.ExecutarComando("insert into log_acesso(usuario_id, data_hora_acesso, endereco_ip) values (@usuario_id, @data, @ip)", listaParametro.ToArray());

                return new Resposta(true, "", new { LoginSucesso = true, Token = sessaoNova.Token});
            }
            else
            {
                return new Resposta(true, "Usuario ou senha invalidos!", new { LoginSucesso = false, Token = "" });
            }
        }

        /// <summary>
        /// Metodo para realizar Logoff na API
        /// </summary>
        /// <param name="prToken">Sequencia de caracteres que identifica a sessão na API</param>
        /// <returns>Retorna um objeto de "Resposta"</returns>
        [Route("api/Login/Logoff")]
        public Resposta Logoff(string prToken)
        {
            Helper.RemoverSessao(prToken);
            return new Resposta(true);
        }

        /// <summary>
        /// Metodo para verificar se uma sessão é valida atualmente
        /// </summary>
        /// <param name="prToken">Sequencia de caracteres que identifica a sessão na API</param>
        /// <returns>Retorna um objeto "Resposta" com TRUE em caso de sessão valida ou FALSE caso contrario</returns>
        [Route("api/Login/VerificaSessao")]
        public Resposta VerificaSessaoAtiva(string prToken)
        {
            if (Helper.ValidaSessao(prToken))
            {
                return new Resposta(true, "", new { sessaoValida = true, usuario = Helper.BuscaUsuarioSessao(prToken).Nome});
            }
            else
            {
                return new Resposta(true, "", new { sessaoValida = false });
            }
        }
    }
}