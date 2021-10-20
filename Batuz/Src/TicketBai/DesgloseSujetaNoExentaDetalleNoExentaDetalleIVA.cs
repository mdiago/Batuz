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
using System.Globalization;
using System.Xml.Serialization;

namespace Batuz.TicketBai
{

    /// <summary>
    /// Datos de línea de IVA.
    /// </summary>
    [Serializable()]
    [XmlType(AnonymousType = true)]
    public class DesgloseSujetaNoExentaDetalleNoExentaDetalleIVA
    {

        #region Propiedades Públicas de Instancia

        /// <summary>
        /// Base imponible.
        /// Decimal (12,2).
        /// </summary>
        public decimal BaseImponible { get; set; }

        /// <summary>
        /// Tipo impositivo.
        /// Decimal (3,2).
        /// </summary>
        [XmlIgnore()]
        public decimal TipoImpositivo { get; set; }

        /// <summary>
        /// Tipo con formato.
        /// </summary>
        [XmlElement("TipoImpositivo")]
        public string TipoImpositivoString 
        { 
            get 
            {

                if (TipoImpositivo == 0)
                    return null;

                var nfi = new NumberFormatInfo() { NumberDecimalSeparator = "." };
                return TipoImpositivo.ToString("0.00", nfi); 
            }
            set
            {

                var nfi = new NumberFormatInfo() { NumberDecimalSeparator = "." };
                decimal amount = 0;

                if (Decimal.TryParse(value, NumberStyles.Number, nfi, out amount))
                    TipoImpositivo = amount;

            }
        }

        /// <summary>
        /// Cuota tipo impositivo.
        /// Decimal (12,2).
        /// </summary>
        public decimal CuotaImpuesto { get; set; }

        /// <summary>
        /// Porcentaje asociado en función del tipo de IVA. 
        /// Decimal (3,2).
        /// </summary>
        public decimal TipoRecargoEquivalencia { get; set; }

        /// <summary>
        /// Indica si se serializa la TipoRecargoEquivalencia.
        /// </summary>
        [XmlIgnore]
        public bool TipoRecargoEquivalenciaSpecified { get { return TipoRecargoEquivalencia != 0; } }


        /// <summary>
        /// Cuota resultante de aplicar a la base imponible el
        /// tipo de recargo de equivalencia. Decimal (12,2).
        /// </summary>
        public decimal CuotaRecargoEquivalencia { get; set; }

        /// <summary>
        /// Indica si se serializa la CuotaRecargoEquivalencia.
        /// </summary>
        [XmlIgnore]
        public bool CuotaRecargoEquivalenciaSpecified { get { return CuotaRecargoEquivalencia != 0; } }

        /// <summary>
        /// Identificador que especifica si se trata de una
        /// factura expedida por un o una contribuyente en
        /// régimen simplificado o en régimen de recargo de
        /// equivalencia.Si no se informa este campo se
        /// entenderá que tiene valor «N». Alfanumérico (1). L12. 
        /// </summary>
        public string OperacionEnRecargoDeEquivalenciaORegimenSimplificado { get; set; }

        #endregion

        #region Métodos Públicos de Instancia

        /// <summary>
        /// Representación textual de la instancia.
        /// </summary>
        /// <returns>Representación textual de la instancia.</returns>
        public override string ToString()
        {
            return $"{OperacionEnRecargoDeEquivalenciaORegimenSimplificado}, " +
                        $"{BaseImponible:#,##0.00}, {TipoImpositivo:#,##0.00}, {CuotaImpuesto:#,##0.00}, " +
                        $"{TipoRecargoEquivalencia:#,##0.00}, {CuotaRecargoEquivalencia:#,##0.00}"; ;
        }

        #endregion

    }

}
