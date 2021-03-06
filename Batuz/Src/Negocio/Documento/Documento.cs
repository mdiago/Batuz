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

using System.Collections.Generic;

namespace Batuz.Negocio.Documento
{

    /// <summary>
    /// Representa un factura o un justificante.
    /// </summary>
    public class Documento : DocumentoCabecera
    {

        #region Propiedades Públicas de Instancia

        /// <summary>
        /// Total neto línea sin impuestos.
        /// </summary>
        public decimal TotalSinImpuestos 
        {
            get 
            {
                
                decimal totalSinImpuestos = 0;

                foreach (var linea in DocumentoLineas)
                    totalSinImpuestos += linea.TotalSinImpuestos;

                return totalSinImpuestos;
            }  
        }

        /// <summary>
        /// Cuota impositiva impuestos.
        /// </summary>
        public decimal CuotaImpuestos
        {
            get
            {

                decimal cuotaImpuestos = 0;

                foreach (var linea in DocumentoLineas)
                    cuotaImpuestos += linea.CuotaImpuestos;

                return cuotaImpuestos;
            }
        }

        /// <summary>
        /// Cuota impositiva impuestos valor añadido recargo.
        /// </summary>
        public decimal CuotaImpuestosRecargo
        {
            get
            {

                decimal cuotaImpuestosRecargo = 0;

                foreach (var linea in DocumentoLineas)
                    cuotaImpuestosRecargo += linea.CuotaImpuestosRecargo;

                return cuotaImpuestosRecargo;
            }
        }

        /// <summary>
        /// Cuota impositiva impuestos retenidos.
        /// </summary>
        public decimal CuotaImpuestosRetenidos
        {
            get
            {

                decimal cuotaImpuestosRetenidos = 0;

                foreach (var linea in DocumentoLineas)
                    cuotaImpuestosRetenidos += linea.CuotaImpuestosRetenidos;

                return cuotaImpuestosRetenidos;
            }
        }

        /// <summary>
        /// Total factura.
        /// </summary>
        public decimal TotalFactura
        {
            get
            {

                return TotalSinImpuestos + CuotaImpuestos + 
                    CuotaImpuestosRecargo - CuotaImpuestosRetenidos;
            }
        }

        /// <summary>
        /// Líneas de detalle del documento.
        /// </summary>
        public List<DocumentoLinea> DocumentoLineas { get; set; }

        /// <summary>
        /// Impuestos valor añadido.
        /// </summary>
        public List<DocumentoImpuesto> DocumentoImpuestos { get; set; }

        /// <summary>
        /// Impuestos retenidos.
        /// </summary>
        public List<DocumentoImpuesto> DocumentoImpuestosRetenidos { get; set; }

        /// <summary>
        /// Vencimientos.
        /// </summary>
        public List<DocumentoVencimiento> DocumentoVencimientos { get; set; }

        #endregion

        #region Métodos Públicos de Instancia

        /// <summary>
        /// Calcula los totales de impuestos.
        /// </summary>
        public void CalcularImpuestos() 
        {

            DocumentoImpuestos = new List<DocumentoImpuesto>();
            Dictionary<string, DocumentoImpuesto> totalPorTipoSoportados = new Dictionary<string, DocumentoImpuesto>();

            foreach (var linea in DocumentoLineas) 
            {

                var key = $"{linea.TipoImpuestos}.{linea.IdentificadorImpuestos}" +
                    $".{linea.TipoImpuestosRecargo}.{linea.IdentificadorImpuestosRecargo}";

                if (totalPorTipoSoportados.ContainsKey(key)) 
                {
                    
                    totalPorTipoSoportados[key].BaseImpuestos += linea.TotalSinImpuestos;
                    totalPorTipoSoportados[key].CuotaImpuestos += linea.CuotaImpuestos;
                    totalPorTipoSoportados[key].CuotaImpuestosRecargo += linea.CuotaImpuestosRecargo;

                }
                else 
                {

                    var impuesto = new DocumentoImpuesto()
                    {

                        BaseImpuestos = linea.TotalSinImpuestos,
                        IdentificadorImpuestos = linea.IdentificadorImpuestos,
                        TipoImpuestos = linea.TipoImpuestos,
                        CuotaImpuestos = linea.CuotaImpuestos,
                        IdentificadorImpuestosRecargo = linea.IdentificadorImpuestosRecargo,
                        TipoImpuestosRecargo = linea.TipoImpuestosRecargo,
                        CuotaImpuestosRecargo = linea.CuotaImpuestosRecargo

                    };

                    DocumentoImpuestos.Add(impuesto);

                    totalPorTipoSoportados.Add(key, impuesto);

                }

            }

            DocumentoImpuestosRetenidos = new List<DocumentoImpuesto>();
            Dictionary<decimal, DocumentoImpuesto> totalPorTipoRetenidos = new Dictionary<decimal, DocumentoImpuesto>();

            foreach (var linea in DocumentoLineas)
            {

                if (linea.CuotaImpuestosRetenidos != 0)
                {

                    if (totalPorTipoRetenidos.ContainsKey(linea.TipoImpuestosRetenidos))
                    {

                        totalPorTipoRetenidos[linea.TipoImpuestosRetenidos].BaseImpuestos += linea.TotalSinImpuestos;
                        totalPorTipoRetenidos[linea.TipoImpuestosRetenidos].CuotaImpuestos += linea.CuotaImpuestosRetenidos;

                    }
                    else
                    {

                        var impuesto = new DocumentoImpuesto()
                        {

                            BaseImpuestos = linea.TotalSinImpuestos,
                            IdentificadorImpuestos = linea.IdentificadorImpuestosRetenidos,
                            TipoImpuestos = linea.TipoImpuestosRetenidos,
                            CuotaImpuestos = linea.CuotaImpuestosRetenidos,

                        };

                        DocumentoImpuestosRetenidos.Add(impuesto);

                        totalPorTipoRetenidos.Add(linea.TipoImpuestosRetenidos, impuesto);

                    }

                }
            }

        }

        #endregion

    }

}
