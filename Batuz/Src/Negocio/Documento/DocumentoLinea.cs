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

namespace Batuz.Negocio.Documento
{

    /// <summary>
    /// Representa un línea de factura.
    /// </summary>
    public class DocumentoLinea
    {

        #region Propiedades Públicas de Instancia

        /// <summary>
        /// Código producto o servicio.
        /// </summary>
        public string ProductoIdentificador { get; set; }

        /// <summary>
        /// Unidades producto o servicio.
        /// </summary>
        public string ProductoDescripcion { get; set; }

        /// <summary>
        /// Cantidad producto o servicio.
        /// </summary>
        public decimal Cantidad { get; set; }

        /// <summary>
        /// Precio producto o servicio.
        /// </summary>
        public decimal Precio { get; set; }

        /// <summary>
        /// Porcentaje de descuento.
        /// </summary>
        public decimal Descuento { get; set; }

        /// <summary>
        /// Total neto línea sin impuestos.
        /// </summary>
        public decimal TotalSinImpuestos { get; set; }

        /// <summary>
        /// Código de impuesto del impuesto soportado.
        /// </summary>
        public string IdentificadorImpuestosSoportados { get; set; }

        /// <summary>
        /// Tipo impositivo impuestos soportados.
        /// </summary>
        public decimal TipoImpuestosSoportados { get; set; }

        /// <summary>
        /// Cuata impositiva impuestos soportados.
        /// </summary>
        public decimal CuotaImpuestosSoportados { get; set; }

        /// <summary>
        /// Tipo impositivo impuestos soportados.
        /// </summary>
        public decimal TipoImpuestosSoportadosRecargo { get; set; }

        /// <summary>
        /// Cuata impositiva impuestos soportados.
        /// </summary>
        public decimal CuotaImpuestosSoportadosRecargo { get; set; }

        /// <summary>
        /// Código de impuesto del impuesto retenido.
        /// </summary>
        public string IdentificadorImpuestosRetenidos { get; set; }

        /// <summary>
        /// Tipo impositivo impuestos retenidos.
        /// </summary>
        public decimal TipoImpuestosRetenidos { get; set; }

        /// <summary>
        /// Cuota impositiva impuestos retenidos.
        /// </summary>
        public decimal CuotaImpuestosRetenidos { get; set; }

        /// <summary>
        /// Total neto línea sin impuestos.
        /// </summary>
        public decimal TotalConImpuestos 
        {
            get 
            {
                return TotalSinImpuestos + CuotaImpuestosSoportados + 
                    CuotaImpuestosSoportadosRecargo - CuotaImpuestosRetenidos; 
            } 
        }

        #endregion

        #region Métodos Públicos de Instancia

        /// <summary>
        /// Representación textual de la instancia.
        /// </summary>
        /// <returns>Representación textual de la instancia.</returns>
        public override string ToString()
        {
            return $"({ProductoDescripcion}) {TotalSinImpuestos}";
        }

        #endregion


    }
}
