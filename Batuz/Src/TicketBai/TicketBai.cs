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

using Batuz.TicketBai.Identificador;
using Batuz.TicketBai.Xades.Xml.Signature;
using System.Xml;
using System.Xml.Serialization;

namespace Batuz.TicketBai
{
    /// <summary>
    /// Representa un documento de factura o justificante según las especificaciones de
    /// TicketBai.
    /// </summary>
    [XmlRoot(Namespace = "urn:ticketbai:emision", IsNullable = false)]
    public class TicketBai
    {

        #region Construtores de Instancia

        /// <summary>
        /// Constructor.
        /// </summary>
        public TicketBai() 
        {
            CodigoIdentificativo = new CodigoIdentificativo(this);
            CodigoQR = new CodigoQR(this);
        }

        #endregion

        #region Propiedades Públicas de Instancia  

        /// <summary>
        /// Código de identificativo de factura o justificante 
        /// generados por el software garante. Según lo estaablecido 
        /// en el artículo 6 de la presente Orden Foral será un dígito
        /// de 39 posiciones 36 + 3 digito de control.
        /// TBAI-NNNNNNNNN-DDMMAA-FFFFFFFFFFFFF-CRC
        /// </summary>
        [XmlIgnore]
        public CodigoIdentificativo CodigoIdentificativo { get; private set; }

        /// <summary>
        /// Código QR identifica a la factura o justificante generado
        /// mediante la utilización del software garante y asegura su 
        /// relación con su correspondiente fichero de alta.
        /// </summary>
        [XmlIgnore]
        public CodigoQR CodigoQR { get; private set; }

        /// <summary>
        /// Cabecera de TicketBai.
        /// </summary>
        [XmlElement(Namespace = "")]
        public Cabecera Cabecera { get; set; }

        /// <summary>
        /// Bloque en el que se detallan el emisor y destinatarios
        /// de la factura.
        /// </summary>
        [XmlElement(Namespace = "")]
        public Sujetos Sujetos { get; set; }

        /// <summary>
        /// Factura representada mediante las especificaciones de TicketBai.
        /// </summary>
        [XmlElement(Namespace = "")]
        public Factura Factura { get; set; }

        /// <summary>
        /// Huella de TicketBai que asegura la integridad y encadenamiento
        /// de los datos.
        /// </summary>
        [XmlElement(Namespace = "")]
        public HuellaTBAI HuellaTBAI { get; set; }

        /// <summary>
        /// Firma.
        /// </summary>
        [XmlElement(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public Signature Signature { get; set; }

        #endregion

        #region Métodos Públicos de Instancia

        /// <summary>
        /// Representación textual de la instancia.
        /// </summary>
        /// <returns>Representación textual de la instancia.</returns>
        public override string ToString()
        {
            return $"{Cabecera}";
        }

        #endregion

    }
}
