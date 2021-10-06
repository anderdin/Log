using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Log.api.Models
{
    /// <summary>
    /// Classe de resposta padrão da API
    /// </summary>
    public class Resposta
    {
        /// <summary>
        /// Propriedade que mostra se a requisição não teve erros
        /// </summary>
        public bool Sucesso { get; set; }
        /// <summary>
        /// propriedade reservada para retornar menssagens ao requisitante
        /// </summary>
        public string Mensagem { get; set; }
        /// <summary>
        /// Propridade para retornar objetos ao requisitante
        /// </summary>
        public object Dados { get; set; }



        /// <summary>
        /// Construtor usado para obrigar a entrada de dados na resposta
        /// </summary>
        /// <param name="prSucesso">Dados boleanos para representar os sucesso na requisição</param>
        /// <param name="prMensagem">Mensagem opcional para retornar ao requisitante</param>
        /// <param name="prDados">Dados opcionais para retornar ao requisitante</param>
        public Resposta(bool prSucesso, string prMensagem = "", object prDados = null)
        {
            this.Sucesso = prSucesso;
            this.Mensagem = prMensagem;
            this.Dados = prDados;
        }
    }
}