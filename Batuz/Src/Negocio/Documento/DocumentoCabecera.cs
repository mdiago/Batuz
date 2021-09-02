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

namespace Batuz.Negocio.Documento
{

    /// <summary>
    /// Representa una cabecera de documento.
    /// </summary>
    public class DocumentoCabecera
    {

        #region Propiedades Públicas de Instancia

        /// <summary>
        /// Tipo de documento.
        /// </summary>
        public DocumentoTipo DocumentoTipo { get; set; }

        /// <summary>
        /// Emisor del documento.
        /// </summary>
        public DocumentoSujeto Emisor { get; set; }

        /// <summary>
        /// Destinatario del documento.
        /// </summary>
        public DocumentoSujeto Destinatario { get; set; }

        /// <summary>
        /// Número de serie que identifica a la factura.
        /// Alfanumérico (20).
        /// </summary>
        public string SerieFactura { get; set; }

        /// <summary>
        /// Número de factura que identifica a la factura.
        /// Alfanumérico (20).
        /// </summary>
        public string NumFactura { get; set; }

        /// <summary>
        /// Fecha de expedición de la factura.
        /// Formato Fecha (10) (dd-mm-aaaa).
        /// </summary>
        public DateTime FechaExpedicionFactura { get; set; }

        /// <summary>
        /// Descripción operación.
        /// </summary>
        public string DescripcionFactura { get; set; }

        /// <summary>
        /// Identificador que especifica si se trata de una
        /// factura simplificada o una factura completa.Si no
        /// se informa este campo se entenderá que tiene
        /// valor «N», entendiéndose que se trata de una
        /// factura completa.
        /// </summary>
        public bool FacturaSimplificada { get; set; }

        /// <summary>
        /// Identificador que especifica si se trata de una
        /// factura emitida en sustitución de una factura
        /// simplificada.Si no se informa este campo se
        /// entenderá que tiene valor «N».
        /// </summary>
        public bool FacturaEmitidaSustitucionSimplificada { get; set; }

        /// <summary>
        /// Código ISO divisa.
        /// </summary>
        public string Moneda { get; set; }

        #endregion

        #region Métodos Públicos de Instancia

        /// <summary>
        /// Representación textual de la instancia.
        /// </summary>
        /// <returns>Representación textual de la instancia.</returns>
        public override string ToString()
        {
            return $"({DocumentoTipo}) {SerieFactura}-{NumFactura} {FechaExpedicionFactura}";
        }

        #endregion



    }
}
