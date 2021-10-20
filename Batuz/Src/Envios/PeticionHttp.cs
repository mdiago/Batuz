/*
    Este archivo forma parte del proyecto Batuz(R).
    Copyright (c) 2021 Irene Solutions SL
    Autores: Manuel Diago García, Juan Bautista Garcia Traver.

    Este programa es software libre; lo puede distribuir y/o modificar
    según los terminos de la licencia GNU Affero General Public License
    versión 3 según su redacción de la Free Software Foundation con la
    siguiente condición añadida en la sección 15 según se establece en
    la sección 7(a):

    PARA CUALQUIER PARTE DEL CÓGIO PROPIEDAD DE IRENE SOLUTIONS. IRENE 
    SOLUTIONS NO SE HACE RESPONSABLE DE LA VULNERACIÓN DE DERECHOS 
    DE TERCEROS.

    Este programa se distribuye con la esperanza de que sea útil, pero
    SIN GARANTÍA DE NINGÚN TIPO; ni siquiera la derivada de un acuerdo
    comercial o utilización para un propósito particular.
   
    Para más información puede consultar la licencia GNU Affero General
    Public http://www.gnu.org/licenses o escribir a la Free Software 
    Foundation, Inc. , 51 Franklin Street, Fifth Floor,
    Boston, MA, 02110-1301 USA, o descargarla en la siguiente URL:
        http://www.irenesolutions.com/terms-of-use.pdf 

    Las interfaces de usuario con versiones del código fuente del presente 
    proyecto, modificado o no, o código de objeto del mismo, deben incluir
    de manera visible los correspondientes avisos legales exigidos en la
    sección 5 de la licencia GNU Affero General Public.
    
    Puede evitar el cumplimiento de lo establecido 
    anteriormente comprando una licencia comercial. 
    La compra de una licencia comercial es obligatoria
    desde el momento en que usted desarrolle software comercial incluyendo
    funcionalidades de Batuz sin la publicación del código fuente
    de sus propias aplicaciones.
    Estas actividades incluyen: La oferta de servicios de pago mediante
    aplicaciones web de cualquier tipo que incluyan la funcionalidad
    de Batuz.
    
    Para más información, contacte con la dirección: info@irenesolutions.com    
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Batuz.Envios
{

    /// <summary>
    /// Representa una petición http.
    /// </summary>
    public class PeticionHttp
    {

        string _Url;
        CabeceraPeticionHttp _CabeceraPeticionHttp;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="url">Url de la petición.</param>
        /// <param name="encoding">Encoding de la petición. 
        /// UTF8 por defecto.</param>
        /// <param name="method">Método de la petición. 
        /// 'POST' por defecto.</param>
        public PeticionHttp(string url, Encoding encoding = null, 
            string method = "POST") 
        {

            Encoding = (encoding == null) ? Encoding.UTF8 : encoding;
            Method = method;

            _Url = url;
            _CabeceraPeticionHttp = new CabeceraPeticionHttp();

            Peticion = GetHttpRequest();
            Peticion.Headers = _CabeceraPeticionHttp.Encabezados;

        }

        /// <summary>
        /// Encabezados hhtp.
        /// </summary>
        public WebHeaderCollection Encabezados { get; private set; }

        /// <summary>
        /// Petición http.
        /// </summary>
        public HttpWebRequest Peticion { get; private set; }

        /// <summary>
        /// Encoding utilizado en la petición.
        /// </summary>
        public Encoding Encoding { get; set; }

        /// <summary>
        /// Método petición http.
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Devuelve una petición http.
        /// </summary>
        /// <returns>Petición http.</returns>
        private HttpWebRequest GetHttpRequest() 
        {

            var result = (HttpWebRequest)WebRequest.Create(_Url);
            result.Method = Method;
            result.ContentType = "application/xml;charset=UTF-8";

            return result;

        }

    }
}
