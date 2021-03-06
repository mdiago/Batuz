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
using System.Xml.Serialization;

namespace Batuz.TicketBai
{

    /// <summary>
    /// Bloque en el que se detallan el emisor y destinatarios
    /// de la factura o justificante.
    /// </summary>
    [Serializable()]
    [XmlType(AnonymousType = true)]
    [XmlRoot(IsNullable = false)]
    public class Sujetos
    {

        #region Propiedades Públicas de Instancia

        /// <summary>
        /// Información del emisor de la factura o justificante.
        /// </summary>
        public SujetosEmisor Emisor { get; set; }

        /// <summary>
        /// Información de los destinatarios de la factura o justificante.
        /// </summary>
        public List<IDDestinatario> Destinatarios { get; set; }

        /// <summary>
        /// Identificador que especifica si la factura tiene
        /// varios destinatarios o varias destinatarias.Si no
        /// se informa este campo se entenderá que tiene
        /// valor «N». Alfanumérico(1) L3.
        /// </summary>
        public string VariosDestinatarios { get; set; }

        /// <summary>
        /// Identificador que especifica si la factura ha sido
        /// emitida por un tercero o una tercera o por el
        /// destinatario o la destinataria.Si no se informa
        /// este campo se entenderá que tiene valor «N».
        /// </summary>
        public string EmitidaPorTercerosODestinatario { get; set; }

        #endregion

        #region Métodos Públicos de Instancia

        /// <summary>
        /// Representación textual de la instancia.
        /// </summary>
        /// <returns>Representación textual de la instancia.</returns>
        public override string ToString()
        {

            var result = "";

            if (Destinatarios != null)
                foreach (var destinatario in Destinatarios)
                    result += $"{(result == "" ? "" : ", ")}{destinatario}";


            return $"{Emisor}: {result}";
        }

        #endregion

    }

}
