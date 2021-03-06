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
using System.Xml.Serialization;

namespace Batuz.TicketBai
{

    /// <summary>
    /// Datos generales factura.
    /// </summary>
    [Serializable()]
    [XmlType(AnonymousType = true)]
    public class FacturaDatosFactura
    {

        #region Propiedades Públicas de Instancia

        /// <summary>
        /// Fecha de operación de la factura.
        /// Formato Fecha (10) (dd-mm-aaaa).
        /// </summary>
        public string FechaOperacion { get; set; }

        /// <summary>
        /// Descripción general de las operaciones.
        /// Alfanumérico (250)
        /// </summary>
        public string DescripcionFactura { get; set; }

        /// <summary>
        /// Importe total de la factura.
        /// Decimal (12,2).
        /// </summary>
        public decimal ImporteTotalFactura { get; set; }

        /// <summary>
        /// Retención soportada.
        /// Decimal (12,2).
        /// </summary>
        public decimal RetencionSoportada { get; set; }

        /// <summary>
        /// Indica si se serializa la RetencionSoportada.
        /// </summary>
        [XmlIgnore]
        public bool RetencionSoportadaSpecified { get { return RetencionSoportada != 0; } }

        /// <summary>
        /// Base imponible a coste (para grupos de IVA–nivel avanzado) 
        /// Decimal (12,2).
        /// </summary>
        public decimal BaseImponibleACoste { get; set; }

        /// <summary>
        /// Indica si se serializa la BaseImponibleACoste.
        /// </summary>
        [XmlIgnore]
        public bool BaseImponibleACosteSpecified { get { return BaseImponibleACoste != 0; } }

        /// <summary>
        /// Clave que identificará el tipo de régimen del IVA
        /// o una operación con trascendencia tributaria.
        /// Alfanumérico (2) L9.
        /// </summary>
        public FacturaDatosFacturaClaves Claves { get; set; }

        #endregion

        #region Métodos Públicos de Instancia

        /// <summary>
        /// Representación textual de la instancia.
        /// </summary>
        /// <returns>Representación textual de la instancia.</returns>
        public override string ToString()
        {
            return $"{FechaOperacion}, {DescripcionFactura}, {ImporteTotalFactura}";
        }

        #endregion

    }
}
