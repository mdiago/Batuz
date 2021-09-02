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
using System.Text;
using System.Threading.Tasks;

namespace Batuz.TicketBai.Identificador
{

    /// <summary>
    /// Código de identificativo de factura o justificante 
    /// generados por el software garante. Según lo estaablecido 
    /// en el artículo 6 de la presente Orden Foral será un dígito
    /// de 39 posiciones 36 + 3 digito de control.
    /// TBAI-NNNNNNNNN-DDMMAA-FFFFFFFFFFFFF-CRC
    /// </summary>
    public class CodigoIdentificativo
    {

        #region Variables Privadas de Instancia

        /// <summary>
        /// Representa la instancia de factura o justificante
        /// a la que pertenece el código identificativo.
        /// </summary>
        TicketBai _TicketBai;

        #endregion

        #region Propiedades Privadas de Instacia

        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="ticketBai"> Factura o justificante
        /// a la que pertenece el código identificativo.</param>
        public CodigoIdentificativo(TicketBai ticketBai)
        {

            _TicketBai = ticketBai;

        }

        #endregion

        #region Propiedades Públicas Estáticas

        /// <summary>
        /// Texto fijo para las posiciones 1-4 del código identificativo.
        /// Según el anexo de la Orden Foral 4 caracteres de texto fijo en mayúscula: TBAI.
        /// </summary>
        public readonly static string TextoFijo = "TBAI";

        /// <summary>
        /// Separador entre segmentos del código indentificativo.
        /// </summary>
        public readonly static string Separador = "-";

        #endregion

        #region Propiedades Públicas de Instancia

        /// <summary>
        /// 9 caracteres del NIF de la persona o entidad emisora de la factura o justificante.
        /// </summary>
        public string NifEmisor
        {
            get
            {

                return _TicketBai?.Sujetos?.Emisor?.NIF;


            }
        }

        /// <summary>
        /// 9 caracteres del NIF de la persona o entidad emisora de la factura o justificante.
        /// </summary>
        public string FechaExpedicionFactura
        {
            get
            {

                var f = _TicketBai?.Factura?.CabeceraFactura?.FechaExpedicionFactura;

                if (f.Length < 10)
                    return null;

                var dd = f.Substring(0, 2);
                var mm = f.Substring(3, 2);
                var yy = f.Substring(8, 2);

                return $"{dd}{mm}{yy}";


            }
        }

        /// <summary>
        /// 13 primeros caracteres de la firma del fichero de alta de operación 
        /// con software garante, es decir, del campo “SignatureValue” del 
        /// fichero correspondiente a la factura o justificante.
        /// </summary>
        public string InicioFirma
        {
            get
            {

                var signatue = _TicketBai?.Signature?.SignatureValue?.Value;

                if (signatue.Length < 13)
                    return null;

                return signatue.Substring(0, 13);


            }
        }

        /// <summary>
        /// Código identificativo previo que constituye la entrada
        /// al calculo del CRC.
        /// </summary>
        public string CodigoIdentificativoPrevio
        {
            get
            {

                return $"{TextoFijo}{Separador}{NifEmisor}{Separador}{FechaExpedicionFactura}{Separador}{InicioFirma}";

            }
        }

        /// <summary>
        /// 3 caracteres que se corresponden con un código de detección de errores 
        /// cuyo objetivo es garantizar que el contenido del código identificativo es correcto.
        /// Este dato debe ser calculado por el software garante y será el resultado de aplicar el algoritmo CRC-8 a la
        /// cadena de caracteres anteriormente definidos, es decir, será el resultado de aplicar dicho algoritmo sobre
        /// los 36 caracteres anteriores.
        /// La entrada al algoritmo será el contenido del código identificativo generado hasta ese momento (los 36
        /// primeros caracteres del código identificativo) con una codificación UTF-8.
        /// La salida del algoritmo se escribirá en formato decimal completando, en caso de ser necesario, con ceros a la
        /// izquierda los 3 últimos caracteres del código identificativo.
        /// </summary>
        public string ControlCRC8
        {
            get
            {

                var input = Encoding.UTF8.GetBytes(CodigoIdentificativoPrevio);

                var crc = CRC8.ComputeChecksum(input);

                return $"{crc}".PadLeft(3, '0');


            }
        }

        /// <summary>
        /// Código identificativo previo que constituye la entrada
        /// al calculo del CRC.
        /// </summary>
        public string CodigoIdentificativoFinal
        {
            get
            {

                return $"{CodigoIdentificativoPrevio}{Separador}{ControlCRC8}";

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
            return $"{CodigoIdentificativoFinal}";
        }

        #endregion

    }
}
