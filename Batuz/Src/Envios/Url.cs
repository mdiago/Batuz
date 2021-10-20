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

namespace Batuz.Envios
{

    /// <summary>
    /// Direcciónes url de los servicios para Batuz.
    /// </summary>
    public static class Url
    {

        #region Propiedades Públicas Estáticas

        /// <summary>
        /// servidor pruebas.
        /// </summary>
        public static string ServerPruebas = "https://pruesarrerak.bizkaia.eus";

        /// <summary>
        /// Servidor producción.
        /// </summary>
        public static string ServerProduccion = "https://sarrerak.bizkaia.eus";

        /// <summary>
        /// Servicio envíos.
        /// </summary>
        public static string ServicioEnvios = "/N3B4000M/aurkezpena";

        /// <summary>
        /// Servicio consulta.
        /// </summary>
        public static string ServicioConsulta = "/N3B4000M/kontsulta";

        /// <summary>
        /// URL del servicio de entradas en el 
        /// entorno de pruebas para el alta, modificación y anulación.
        /// </summary>
        public static string UrlPruebasEnvio = $"{ServerPruebas}{ServicioEnvios}";

        /// <summary>
        ///  URL del servicio de entradas en el entorno 
        ///  de pruebas para las consultas.
        /// </summary>
        public static string UrlPruebasConsulta = $"{ServerPruebas}{ServicioConsulta}";

        /// <summary>
        /// URL del servicio de entradas en el 
        /// entorno de pruebas para el alta, modificación y anulación.
        /// </summary>
        public static string UrlProduccionEnvio = $"{ServerProduccion}{ServicioEnvios}";

        /// <summary>
        ///  URL del servicio de entradas en el entorno 
        ///  de pruebas para las consultas.
        /// </summary>
        public static string UrlProduccionConsulta = $"{ServerProduccion}{ServicioConsulta}";

        #endregion

    }

}
