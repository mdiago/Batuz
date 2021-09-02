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

using System.Globalization;
using System.Text;
using System.Web;

namespace Batuz.TicketBai.Identificador
{

    /// <summary>
    /// el código QR identifica a la factura o justificante generado
    /// mediante la utilización del software garante y asegura su relación con su correspondiente fichero de alta de
    /// operación con software garante al que se refiere el artículo 3 de la presente Orden Foral.
    /// https://batuz.eus/QRTBAI/?id=TBAI-00000006Y-251019-btFpwP8dcLGAF-237&s=T&nf=27174&i=4.70&cr=007
    /// </summary>
    public class CodigoQR
    {

        #region Variables Privadas de Instancia

        /// <summary>
        /// Representa la instancia de factura o justificante
        /// a la que pertenece el código QR.
        /// </summary>
        TicketBai _TicketBai;

        #endregion

        #region Propiedades Privadas de Instacia

        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="ticketBai"> Factura o justificante
        /// a la que pertenece el código QR.</param>
        public CodigoQR(TicketBai ticketBai)
        {

            _TicketBai = ticketBai;

        }

        #endregion

        #region Propiedades Públicas Estáticas

        /// <summary>
        /// Raiz de la url.
        /// </summary>
        public readonly static string RootUrl = "https://batuz.eus/QRTBAI/";

        /// <summary>
        /// Separador parametros Url.
        /// </summary>
        public readonly static string Separador = "?";

        #endregion

        #region Propiedades Públicas de Instancia

        /// <summary>
        /// Código identificativo Sus
        /// </summary>
        public string Id
        {
            get
            {

                return $"{_TicketBai?.CodigoIdentificativo}";


            }
        }

        /// <summary>
        /// Serie de la factura o justificante.
        /// Valor del campo “SerieFactura” incluido en el fichero de alta
        /// de operación con software garante.
        /// </summary>
        public string SerieFactura
        {
            get
            {

                return _TicketBai?.Factura?.CabeceraFactura?.SerieFactura;     

            }
        }

        /// <summary>
        /// 13 primeros caracteres de la firma del fichero de alta de operación 
        /// con software garante, es decir, del campo “SignatureValue” del 
        /// fichero correspondiente a la factura o justificante.
        /// </summary>
        public string NumFactura
        {
            get
            {

                return _TicketBai?.Factura?.CabeceraFactura?.NumFactura; 

            }
        }

        /// <summary>
        /// Importe total de la factura o justificante
        /// Valor y formato del campo “ImporteTotalFactura” incluido
        /// en el fichero de alta de operación con software garante.
        /// </summary>
        public string ImporteTotalFactura
        {
            get
            {

                NumberFormatInfo numberFormatInfo = new NumberFormatInfo() 
                { 
                    NumberDecimalSeparator = "."
                };
                return _TicketBai?.Factura?.DatosFactura.ImporteTotalFactura.ToString(numberFormatInfo);

            }
        }

        /// <summary>
        /// contenido del código QR
        /// generado hasta ese momento previo a una codificación UTF-8.
        /// Por tanto, no se incluirá ni el propio parámetro cr ni su
        /// símbolo asociado “&” utilizado para añadirlo al resto de los
        /// parámetros(query string)..
        /// </summary>
        public string UrlPrevia
        {
            get
            {

               return $"{RootUrl}{Separador}id={Id}&s={SerieFactura}&nf={NumFactura}&i={ImporteTotalFactura}";  

            }
        }

        /// <summary>
        /// UrlPrevia url encoded.
        /// </summary>
        public string UrlPreviaEncoded
        {
            get
            {

                return HttpUtility.UrlEncode(UrlPrevia);

            }
        }

        /// <summary>
        /// CRC-8. Código de detección de
        /// errores que se incluye con el
        /// objetivo de detectar cambios
        /// accidentales en el contenido del
        /// código QR.
        /// </summary>
        public string ControlCRC8
        {
            get
            {

                var input = Encoding.UTF8.GetBytes(UrlPreviaEncoded);

                var crc = CRC8.ComputeChecksum(input);

                return $"{crc}".PadLeft(3, '0');


            }
        }

        /// <summary>
        /// Url final sin url encoded.
        /// </summary>
        public string UrlFinal
        {
            get
            {

                return $"{UrlPrevia}cr={ControlCRC8}";

            }
        }

        /// <summary>
        /// CRC-8. Código de detección de
        /// errores que se incluye con el
        /// objetivo de detectar cambios
        /// accidentales en el contenido del
        /// código QR.
        /// </summary>
        public string UrlFinalEncoded
        {
            get
            {

                return HttpUtility.UrlEncode(UrlFinal);

            }
        }

        #endregion

        #region Métodos Públicos de Instancia

        /// <summary>
        /// Representación textual de la instancia.
        /// </summary>
        /// <returns>Representación textual de la instancia.</returns>
        public override string ToString()
        {
            return $"{UrlFinalEncoded}";
        }

        #endregion

    }
}
