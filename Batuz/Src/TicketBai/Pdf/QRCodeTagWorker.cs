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

using iText.Barcodes;
using iText.Barcodes.Qrcode;
using iText.Html2pdf.Attach;
using iText.Kernel.Colors;
using iText.Layout;
using iText.Layout.Element;
using iText.StyledXmlParser.Node;
using System;
using System.Collections.Generic;

namespace Batuz.TicketBai.Pdf
{

    /// <summary>
    /// Custom tagworker implementation for pdfHTML.
    /// The tagworker processes a /<qr/> tag using iText Barcode functionality
    /// </summary>
    public class QRCodeTagWorker : ITagWorker
    {

        private static string[] allowedErrorCorrection = { "L", "M", "Q", "H" };
        private static string[] allowedCharset = { "Cp437", "Shift_JIS", "ISO-8859-1", "ISO-8859-16" };

        private BarcodeQRCode qrCode;
        private Image qrCodeAsImage;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="context"></param>
        public QRCodeTagWorker(IElementNode element, ProcessorContext context)
        {

            // Retrieve all necessary properties to create the barcode
            Dictionary<EncodeHintType, object> hints = new Dictionary<EncodeHintType, object>();

            // Character set
            string charset = element.GetAttribute("charset");
            if (CheckCharacterSet(charset))
            {
                hints.Add(EncodeHintType.CHARACTER_SET, charset);
            }

            // Error-correction level
            string errorCorrection = element.GetAttribute("errorcorrection");
            if (CheckErrorCorrectionAllowed(errorCorrection))
            {
                ErrorCorrectionLevel errorCorrectionLevel = GetErrorCorrectionLevel(errorCorrection);
                hints.Add(EncodeHintType.ERROR_CORRECTION, errorCorrectionLevel);
            }

            // Create the QR-code
            qrCode = new BarcodeQRCode("placeholder", hints);

        }

        /// <summary>
        /// Placeholder for what needs to be done after the content of a tag has been processed.
        /// </summary>
        /// <param name="element">the element node</param>
        /// <param name="context">the processor context</param>
        public void ProcessEnd(IElementNode element, ProcessorContext context)
        {
            float moduleSize = 1.8f;
            // Transform barcode into image
            qrCodeAsImage = new Image(qrCode.CreateFormXObject(ColorConstants.BLACK, 
                moduleSize, context.GetPdfDocument()));

        }

        /// <summary>
        /// Placeholder for what needs to be done while the content of a tag is being processed.
        /// </summary>
        /// <param name="content">the content of a node</param>
        /// <param name="context">the processor context</param>
        /// <returns>true, if content was successfully processed, otherwise false.</returns>
        public bool ProcessContent(String content, ProcessorContext context)
        {

            // Add content to the barcode
            qrCode.SetCode(content);
            return true;
        }

        /// <summary>
        /// Placeholder for what needs to be done when a child node is being processed.
        /// </summary>
        /// <param name="childTagWorker">the tag worker of the child node</param>
        /// <param name="context">the processor context</param>
        /// <returns> true, if child was successfully processed, otherwise false.</returns>
        public bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context)
        {
            return false;
        }

        /// <summary>
        ///  Gets a processed object if it can be expressed as an iText.Layout.IPropertyContainer
        ///  instance.
        /// </summary>
        /// <returns> The same object on every call. Might return null either if result is not yet
        /// produced or if this particular tag worker doesn't produce result in a form of
        /// iText.Layout.IPropertyContainer.</returns>
        public IPropertyContainer GetElementResult()
        {

            return qrCodeAsImage;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toCheck"></param>
        /// <returns></returns>
        private static bool CheckErrorCorrectionAllowed(string toCheck)
        {
            for (int i = 0; i < allowedErrorCorrection.Length; i++)
            {
                if (toCheck.ToUpper().Equals(allowedErrorCorrection[i]))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toCheck"></param>
        /// <returns></returns>
        private static bool CheckCharacterSet(string toCheck)
        {
            for (int i = 0; i < allowedCharset.Length; i++)
            {
                if (toCheck.Equals(allowedCharset[i]))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private static ErrorCorrectionLevel GetErrorCorrectionLevel(string level)
        {
            switch (level)
            {
                case "L":
                    return ErrorCorrectionLevel.L;
                case "M":
                    return ErrorCorrectionLevel.M;
                case "Q":
                    return ErrorCorrectionLevel.Q;
                case "H":
                    return ErrorCorrectionLevel.H;
            }
            return null;

        }

    }
}
