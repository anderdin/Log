using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.dal.Modelos
{
    /// <summary>
    /// Classe de modelo para logs de acesso
    /// </summary>
    public class LogAcesso
    {
        /// <summary>
        /// ID unico para cada log de acesso
        /// </summary>
        public int LogAcessoId { get; set; }
        /// <summary>
        /// Usuario ao qual o log de acesso esta ligado
        /// </summary>
        public Usuario Usuario { get; set; }
        /// <summary>
        /// Data e hora em que o acesso foi executado
        /// </summary>
        public DateTime DataHoraAcesso { get; set; }
        /// <summary>
        /// Endereço de IP da maquina que realizou o acesso
        /// </summary>
        public string EnderecoIp { get; set; }
    }
}
