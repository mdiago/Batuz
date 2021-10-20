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

using Batuz.Negocio.Documento;
using Batuz.TicketBai;
using System.Web.Script.Serialization;

namespace Batuz.Envios.Json
{

    /// <summary>
    /// Representa el contenido json del encabezado http
    /// eus-bizkaia-n3-data.
    /// </summary>
    public class data
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="apartado">Apartado LROE:
        /// <para>1.1: Ingresos con facturas emitidas con software garante.</para>
        /// <para>1.2: Ingresos con facturas emitidas sin software garante.</para>
        /// <para>2.1: Gastos con factura.</para>
        /// <para>2.2: Gastos sin factura.</para>
        /// <para>3.1: Alta de bienes afectos o de inversión.</para>
        /// <para>3.2: Alta de bienes afectos o de inversión.</para>
        /// <para>3.3: Baja de bienes afectos o de inversión.</para>
        /// <para>3.4: Regularización anual de bienes de inversión.</para>
        /// <para>4.1: Transferencias intracomunitarias, informes periciales y otros trabajos.</para>
        /// <para>4.2: Venta de bienes en consigna.</para>
        /// <para>5.1: Cobros.</para>
        /// <para>5.2: Pagos.</para>
        /// <para>7.1: Variación de existencias.</para>
        /// <para>7.2: Arrendamientos de locales de negocios.</para>
        /// <para>7.3: Transmisiones de inmuebles sujetas a IVA.</para>
        /// <para>7.4: Importes superiores a 6.000 euros percibidos en metálico.</para>
        /// <para>8.1: Alta de agrupaciones de bienes.</para>
        /// <para>8.2: Baja de agrupaciones de bienes.</para>
        /// </param>
        /// <param name="interesado">Interesado: datos del obligado tributario, 
        /// persona (tanto física como jurídica).</param>
        /// <param name="modelo">Datos relevantes: son los datos necesarios para identificar el modelo.</param>
        /// <param name="ejercicio">Ejercicio: ejercicio del modelo.</param>
        public data(string apartado, inte interesado, string modelo, string ejercicio) 
        {
            
            apa = apartado;
            inte = interesado;

            drs = new drs() 
            { 
                mode = modelo,
                ejer = ejercicio
            };

            con = "LROE";            

        }

        #region Propiedades Públicas de Instancia

        /// <summary>
        /// Concepto: es el código del concepto asociado a lo que se quiere registrar:
        /// <para>LROE</para>
        /// </summary>
        public string con { get; set; }

        /// <summary>
        /// Apartado: hay que indicar obligatoriamente el número correspondiente a la 
        /// estructura del LROE.Por ejemplo, en Ingresos con facturas con Software
        /// garante hay que indicar el valor 1.1 y en Facturas recibidas hay que indicar
        /// el valor 2.
        /// </summary>
        public string apa { get; private set; }

        /// <summary>
        /// Interesado: datos del obligado tributario, persona (tanto física como jurídica) 
        /// sobre la que se hace el registro.
        /// </summary>
        public inte inte { get; private set; }

        /// <summary>
        /// Datos relevantes: son los datos necesarios para identificar el modelo que se 
        /// quiere presentar:
        /// <para>Modelo: es el modelo a presentar (140/240).</para>
        /// <para>Ejercicio: ejercicio del modelo, a cumplimentar con el ejercicio que 
        /// corresponda</para>
        /// </summary>
        public drs drs { get; private set; }

        #endregion

        /// <summary>
        /// Representación textual de la instancia.
        /// </summary>
        /// <returns>Representación textual de la instancia.</returns>
        public override string ToString()
        {

            var jds = new JavaScriptSerializer();
            return jds.Serialize(this);

        }

    }
}
