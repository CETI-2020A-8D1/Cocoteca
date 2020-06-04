using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cocoteca.Helper
{
    /// <summary>
    /// Retorna elementos de conexión con el Cocoppel API
    /// </summary>
    public class CocoppelAPI
    {
        /// <summary>
        /// Retorna una conexión en forma de string.
        /// </summary>
        /// <returns>Un string de la conexión a el Cocontrolador API</returns>
        public static string Initial()
        {
            return "https://localhost:44309/";
        }
    }
}
