/*
    This file is part of the EasySII (R) project.
    Copyright (c) 2017-2018 Irene Solutions SL
    Authors: Irene Solutions SL.

    This program is free software; you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License version 3
    as published by the Free Software Foundation with the addition of the
    following permission added to Section 15 as permitted in Section 7(a):
    FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
    IRENE SOLUTIONS SL. IRENE SOLUTIONS SL DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
    OF THIRD PARTY RIGHTS
    
    This program is distributed in the hope that it will be useful, but
    WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
    or FITNESS FOR A PARTICULAR PURPOSE.
    See the GNU Affero General Public License for more details.
    You should have received a copy of the GNU Affero General Public License
    along with this program; if not, see http://www.gnu.org/licenses or write to
    the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
    Boston, MA, 02110-1301 USA, or download the license from the following URL:
        http://www.irenesolutions.com/terms-of-use.pdf
    
    The interactive user interfaces in modified source and object code versions
    of this program must display Appropriate Legal Notices, as required under
    Section 5 of the GNU Affero General Public License.
    
    You can be released from the requirements of the license by purchasing
    a commercial license. Buying such a license is mandatory as soon as you
    develop commercial activities involving the EasySII software without
    disclosing the source code of your own applications.
    These activities include: offering paid services to customers as an ASP,
    serving sii XML data on the fly in a web application, shipping EasySII
    with a closed source product.
    
    For more information, please contact Irene Solutions SL. at this
    address: info@irenesolutions.com
 */

using Batuz.TicketBai.Listas;
using System;
using System.Collections.Generic;

namespace Batuz.TicketBai
{

    /// <summary>
    /// Datos factura rectificativa.
    /// </summary>
    [Serializable()]
    public class FacturaCabeceraFacturaFacturaRectificativa
    {

        #region Propiedades Públicas de Instancia

        /// <summary>
        /// Código que identifica el tipo de factura
        /// rectificativa. Alfanumérico (2) L7.
        /// </summary>
        public CodigoFacturaRectificativa Codigo { get; set; }

        /// <summary>
        /// True para incluir bloque 'Codigo'.
        /// </summary>
        public bool CodigoSpecified { get; set; }

        /// <summary>
        /// Identifica si el tipo de factura rectificativa es por
        /// sustitución o por diferencias. Alfanumérico (1) L8.
        /// </summary>
        public TipoFacturaRectificativa Tipo { get; set; }

        /// <summary>
        /// True para incluir bloque 'Tipo'.
        /// </summary>
        public bool TipoSpecified { get; set; }

        /// <summary>
        /// Importes impuesto factura rectificativa.
        /// </summary>
        public FacturaCabeceraFacturaFacturaRectificativaImporteRectificacionSustitutiva ImporteRectificacionSustitutiva { get; set; }

        /// <summary>
        /// Lista de facturas sustituidas.
        /// </summary>
        public List<FacturaCabeceraFacturaFacturaRectificativaIDFacturaRectificadaSustituida> FacturasRectificadasSustituidas { get; set; }

        #endregion

        #region Métodos Públicos de Instancia

        /// <summary>
        /// Representación textual de la instancia.
        /// </summary>
        /// <returns>Representación textual de la instancia.</returns>
        public override string ToString()
        {
            return $"{base.ToString()}";
        }

        #endregion

    }
}
