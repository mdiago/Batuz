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

using Batuz.Negocio.Serializadores;
using System;
using System.Collections.Generic;

namespace Batuz.Negocio
{

    /// <summary>
    /// Gestiona la creación de instancias de TicketBai. La idea es unificar 
    /// la generación de los distintos tipos de documentos: con IVA normal, 
    /// con inversión del sujeto pasivo, de regímenes especiales...Utilizando
    /// el valor de la propiedad 'IdentificadorImpuestos' de la línea de 
    /// impuetos de un documento. Este tratamiento de los impuestos es similar
    /// al utilizado en los sistemas ERP más populares, como SAP, IFS, Ms Dynamics...
    /// La clase TicketBaiFactory cálcula los distintos tipos de docmentos TicketBai
    /// a partir de la entrada como argumento de una instancia de la clase 'Documento'.
    /// La utilización de códigos de impuesto en las líneas de impuesto 
    /// ('IdentificadorImpuestos') permite realizar los mapeo con los distintos
    /// generadores de TicketBai.
    /// </summary>
    public class TicketBaiFactory
    {

        #region Variables Privadas Estáticas

        /// <summary>
        /// Serializador básico.
        /// </summary>
        static Serializadores.Basico _Basico = new Serializadores.Basico();

        /// <summary>
        /// Diccionario de serializadores por cadena de acceso.
        /// </summary>
        static Dictionary<string, ISerializador> _Serializadores = new Dictionary<string, ISerializador>()
        {

            // Regimen general
            {"IRAC0400.SINVALOR",       _Basico},
            {"IRAC1000.SINVALOR",       _Basico},
            {"IRAC2100.SINVALOR",       _Basico},

            // Recargo de equivalencia
            {"IRAC0400.IRAD0050",       _Basico},
            {"IRAC1000.IRAD0140",       _Basico},
            {"IRAC2100.IRAD0520",       _Basico},

            // Recargo de equivalencia tabaco
            {"IRAC2100.IRAD0175",       _Basico},

        };

        #endregion

        #region Métodos Privados Estáticos

        /// <summary>
        /// Devuelve un serializador para una cadena de acceso
        /// utilizada como clave en el diccionario de serializadores.
        /// La cadena de acceso consiste en la concatenación de los
        /// indentificadores de impuestos.
        /// </summary>
        /// <param name="documento">Documento a serializar.</param>
        /// <returns>Serializador para el documento.</returns>
        private static ISerializador GetSerializador(Documento.Documento documento)
        {

            if (documento.DocumentoImpuestos == null)
                throw new Exception($"El documento no tiene impuestos calculados.");

            ISerializador serializador = null;
            string keys = "";

            foreach (var iva in documento.DocumentoImpuestos)
            {

                var key = $"{iva.IdentificadorImpuestos}.{iva.IdentificadorImpuestosRecargo}";
                keys = $"\n{key}";

                if (_Serializadores.ContainsKey(key))
                {

                    var candidato = _Serializadores[key];

                    if (serializador != null)
                    {
                        if (!serializador.Equals(candidato))
                            throw new InvalidOperationException($"En el documento conviven" +
                                $" indentificadores de impuestos que determinan serializadores" +
                                $" diferentes. {candidato.GetType().FullName}" +
                                $" and {serializador.GetType().FullName}");
                    }
                    else
                    {
                        serializador = _Serializadores[key];
                    }

                }

            }

            if (serializador == null)
                throw new Exception($"No se encontró serializador para las claves:{keys}");

            return serializador;


        }

        #endregion

        #region Métodos Públicos Estáticos

        /// <summary>
        /// Devuelve una instancia de TicketBai
        /// a partir de un documento.
        /// </summary>
        /// <param name="documento">Factura o justificante del cual se
        /// va a generar el TicketBai.</param>
        /// <returns>TicketBai que representa la factura o jusficante facilitados
        /// como argumento.</returns>
        public static TicketBai.TicketBai GetTicketBai(Documento.Documento documento)
        {

            ISerializador serializador = GetSerializador(documento);
            return serializador.GetTicketBai(documento);

        }

        #endregion

    }
}
