﻿/*
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


using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl;
using iText.StyledXmlParser.Node;

namespace Batuz.TicketBai.Pdf
{

    /// <summary>
    /// Custom tagworkerfactory for pdfHTML for tag qr.
    /// </summary>
    public class QRCodeTagWorkerFactory : DefaultTagWorkerFactory
    {

        /// <summary>
        /// Custom tagworkerfactory for pdfHTML
        /// The tag /<qr/> is mapped on a QRCode tagworker. Every other tag is mapped to the default.
        /// This is a hook method. Users wanting to provide a custom mapping or introduce
        /// their own ITagWorkers should implement this method.
        /// </summary>
        /// <param name="tag"> the tag</param>
        /// <param name="context"> the context</param>
        /// <returns>the custom tag worker</returns>
        public override ITagWorker GetCustomTagWorker(IElementNode tag, ProcessorContext context)
        {
            
            if (tag.Name().Equals("qr"))
                return new QRCodeTagWorker(tag, context);

            return null;
        }

    }
}
