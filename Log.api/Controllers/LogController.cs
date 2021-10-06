using Log.api.Models;
using Log.dal;
using Log.dal.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Log.api.Controllers
{
    /// <summary>
    /// Classe de endpoint da API para manuzeio de dados de Log
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LogController : ApiController
    {
        /// <summary>
        /// Endpoint que lista todos os log do usuario a qual o token pertence
        /// </summary>
        /// <param name="prToken">Sequencia de caracteres que identifica as sessão do usuario</param>
        /// <returns>Um objeto "Resposta" com dado da lista de registro de logs</returns>
        [Route("api/Log/ListarLog")]
        public Resposta ListarLog(string prToken)
        {
            if (Helper.ValidaSessao(prToken))
            {
                Usuario usuarioSessao = Helper.BuscaUsuarioSessao(prToken);

                HelperConexao conn = new HelperConexao();
                List<SqlParameter> listaParametros = new List<SqlParameter>();
                listaParametros.Add(new SqlParameter("usuario_id", usuarioSessao.UsuarioId));

                DataTable dt = conn.ExecutarComandoRetorno(
                    "select a.data_hora_acesso, b.nome, a.endereco_ip from log_acesso a left join usuario b on b.usuario_id = a.usuario_id where a.usuario_id = @usuario_id order by a.data_hora_acesso desc", 
                    listaParametros.ToArray()
                    );

                List<object> listaLogs = new List<object>();

                foreach(DataRow item in dt.Rows)
                {
                    listaLogs.Add(new 
                    {
                        DataHora = DateTime.Parse(item.ItemArray[0].ToString()).ToString("dd/MM/yyyy hh:mm:ss"),
                        Usuario = item.ItemArray[1].ToString(),
                        ip = item.ItemArray[2].ToString()
                    });
                }

                return new Resposta(true, "", listaLogs);
            }
            else
            {
                return new Resposta(false, "Token invalido");
            }
        }

        /// <summary>
        /// Endpoint que busca dados de log do usuario ao qual o token se refere para uso do grafico
        /// </summary>
        /// <param name="prToken">Sequencia de caracteres que identifica as sessão do usuario</param>
        /// <returns>Um objeto "Resposta" com dado da lista de registro de logs no formato pre moldado para o gráfico</returns>
        [Route("api/Log/BuscarDadosGrafico")]
        public Resposta BuscarDadosGrafico(string prToken)
        {
            if (Helper.ValidaSessao(prToken))
            {
                Usuario usuarioSessao = Helper.BuscaUsuarioSessao(prToken);

                HelperConexao conn = new HelperConexao();
                List<SqlParameter> listaParametros = new List<SqlParameter>();
                listaParametros.Add(new SqlParameter("usuario_id", usuarioSessao.UsuarioId));

                DataTable dt = conn.ExecutarComandoRetorno(
                    "select a.data_hora_acesso from log_acesso a left join usuario b on b.usuario_id = a.usuario_id where a.usuario_id = @usuario_id order by a.data_hora_acesso desc",
                    listaParametros.ToArray()
                    );

                List<dynamic> listaLogs = new List<dynamic>();
                int time = 0;
                for (var i = 0; i < 24; i++)
                {
                    dynamic objeto = new ExpandoObject();
                    objeto.hora = i;
                    objeto.quantidade = 0;
                    objeto.timeMili = time;

                    listaLogs.Add(objeto);
                    time = time + 3600000;
                }

                foreach (DataRow item in dt.Rows)
                {
                    int hora = Int32.Parse(DateTime.Parse(item.ItemArray[0].ToString()).ToString("HH"));

                    listaLogs[hora].quantidade = listaLogs[hora].quantidade + 1;
                }

                return new Resposta(true, "", listaLogs);
            }
            else
            {
                return new Resposta(false, "Token invalido");
            }
        }
    }
}