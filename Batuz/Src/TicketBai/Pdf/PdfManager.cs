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

using iText.Html2pdf;
using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Font;
using System.IO;

namespace Batuz.TicketBai.Pdf
{

    /// <summary>
    /// Se encarga de realizar los trabajos de generación de 
    /// dpocumentos pdf.
    /// </summary>
    public class PdfManager
    {

        /// <summary>
        ///  Custom tagworkerfactory for pdfHTML for tag qr.
        /// </summary>
        public QRCodeTagWorkerFactory QRCodeTagWorkerFactory { get; private set; }

        /// <summary>
        /// Convierte texto html de entrada en un archivo pdf.
        /// </summary>
        /// <param name="html">Html a convertir en pdf.</param>
        /// <param name="orientation">Orientación.</param>
        /// <param name="fontData">Byte content of the font program file.</param>
        /// <returns>Datos binarios del pdf.</returns>
        public byte[] GetPdfFormHtml(string html, 
            string orientation = "PORTRAIT", byte[] fontData = null)
        {

            QRCodeTagWorkerFactory = new QRCodeTagWorkerFactory();

            ConverterProperties properties = new ConverterProperties();
            properties.SetTagWorkerFactory(QRCodeTagWorkerFactory);
            properties.SetCssApplierFactory(new QRCodeTagCssApplierFactory());

            if(fontData != null) 
            {

                FontProvider fontProvider = new FontProvider();
                fontProvider.AddFont(fontData, PdfEncodings.IDENTITY_H);

                properties.SetFontProvider(fontProvider);

            }

            byte[] result = null;

            using (var ms = new MemoryStream())
            {
                using (var pdfDocument = new PdfDocument(new PdfWriter(ms)))
                {

                    if (orientation.ToUpper() == "LANDSCAPE")
                        pdfDocument.SetDefaultPageSize(PageSize.A4.Rotate());

                    HtmlConverter.ConvertToPdf(html, pdfDocument, properties);
                    result = ms.ToArray();
                }
            }

            return result;

        }

        /// <summary>
        /// Devuelve el código QR como una imágen
        /// de itext.
        /// </summary>
        /// <returns>Código QR como una imágen de itext.</returns>
        public Image GetQrCodeAsImage() 
        {
            return QRCodeTagWorkerFactory?.QRCodeTagWorker?.GetElementResult() as Image;
        }
    
    }
}
