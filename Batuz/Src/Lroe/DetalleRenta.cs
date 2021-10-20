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

namespace Batuz.Lroe
{

    /// <summary>
    /// Datos detalle de renta.
    /// </summary>
    [Serializable()]
    [XmlType(AnonymousType = true)]
    public class DetalleRenta
    {

        #region Propiedades Públicas de Instancia

        /// <summary>
        /// Epígrafe de la actividad ejercida.
        /// Alfanumérico (7).
        /// </summary>
        public string Epigrafe { get; set; }

        /// <summary>
        /// Identificador que especifica si el ingreso a computar en el IRPF es 
        /// diferente a la base imponible del IVA o en su caso de la base imponible
        /// más cuota repercutida.Si no se informa este campo se entenderá que
        /// tiene valor “N”.
        /// Alfanumérico (1) L15.
        /// </summary>
        public string IngresoAComputarIRPFDiferenteBaseImpoIVA { get; set; }

        /// <summary>
        /// Ingreso a computar en el IRPF, siempre que este ingreso sea diferente a la 
        /// base imponible del IVA.
        /// Decimal (12,2).
        /// </summary>
        public decimal ImporteIngresoIRPF { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public bool ImporteIngresoIRPFSpecified { get { return ImporteIngresoIRPF != 0; } }

        #endregion

    }
}
