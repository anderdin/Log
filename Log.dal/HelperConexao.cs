using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.dal
{
    /// <summary>
    /// Classe de controle de acesso e execução para o sql
    /// </summary>
    public class HelperConexao
    {
        /// <summary>
        /// Propriedade para guardar a conexão com banco de dados
        /// </summary>
        public SqlConnection Conexao { get; set; }

        /// <summary>
        /// Propriedade para armazenar sqlcommend e suas funções
        /// </summary>
        private SqlCommand Comando { get; set; }

        /// <summary>
        /// Contrutor do Helper que carrega os dados de conexão e prepara as variaveis
        /// </summary>
        public HelperConexao()
        {
            this.Conexao = new SqlConnection(ConfigurationManager.AppSettings["string_conexao"]);
            this.Comando = new SqlCommand();
            this.Comando.Connection = Conexao;
        }




        /// <summary>
        /// Metodo que executa comandos sql sem retornar resultados
        /// </summary>
        /// <param name="sql">String contendo um comendo sql que não retorna resultados</param>
        /// <param name="parametros">Lista de parametros sql para agregar a query de execução</param>
        public void ExecutarComando(string sql, SqlParameter[] parametros = null)
        {
            try
            {
                Comando.CommandText = sql;
                Comando.CommandType = System.Data.CommandType.Text;
                if (!(parametros is null))
                    Comando.Parameters.AddRange(parametros);

                Comando.Connection.Open();
                Comando.ExecuteNonQuery();
                Comando.Connection.Close();

            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um erro ao executar o comendo sql, mais detalhes no inner exception", e);
            }
            finally
            {
                this.Comando = new SqlCommand();
            }
        }

        /// <summary>
        /// Metodo que executa comandos sql porem traz um DataTable como retorno
        /// </summary>
        /// <param name="sql">String contendo um comendo sql que retorna resultados como um SELECT por exemplo</param>
        /// <param name="parametros">Lista de parametros sql para agregar a query de execução</param>
        /// <returns>Retorna um DataTable com resultados da consulta</returns>
        public DataTable ExecutarComandoRetorno(string sql, SqlParameter[] parametros = null)
        {
            try
            {
                Comando.CommandText = sql;
                Comando.CommandType = System.Data.CommandType.Text;
                if (!(parametros is null))
                    Comando.Parameters.AddRange(parametros);

                Comando.Connection.Open();
                SqlDataReader dr = Comando.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(dr);

                Comando.Connection.Close();

                return dt;
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um erro ao executar o comendo sql com retorno, mais detalhes no inner exception", e);
            }
            finally
            {
                this.Comando = new SqlCommand();
            }
        }
    }
}
