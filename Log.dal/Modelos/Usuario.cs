using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.dal.Modelos
{
    /// <summary>
    /// Classe de Modelo com dados do usuario para acesso
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// ID unico de identificação do usuario
        /// </summary>
        public int UsuarioId { get; set; }
        /// <summary>
        /// Nome do usuario do modelo
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Login utilizado pelo usuario para acesso no sistema
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Senha utilizada pelo usuario para acesso no sistema
        /// </summary>
        public string Senha { get; set; }
    }
}
