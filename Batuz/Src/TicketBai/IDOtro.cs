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
using System.Xml.Serialization;

namespace Batuz.TicketBai
{

    /// <summary>
    /// Identificador distinto al NIF.
    /// </summary>
    [Serializable]
    [XmlRoot("IDOtro")]
    public class IDOtro
    {

        #region Propiedades Públicas de Instancia

        /// <summary>
        /// Código del país asociado al
        /// emisor de la factura.
        /// Alfanumérico(2) (ISO 3166-1 alpha-2 codes) L1.
        /// </summary>
        public CodigoPais CodigoPais { get; set; }

        /// <summary>
        /// Con false no serializa Código País.
        /// </summary>
        public bool CodigoPaisSpecified { get; set; }

        /// <summary>
        /// Clave para establecer el tipo de
        /// identificación en el pais de
        /// residencia.Alfanumérico(2) L2.
        /// </summary>
        public IDOtroType IDType { get; set; }

        /// <summary>
        /// Con false no serializa.
        /// </summary>
        public bool IDTypeSpecified { get; set; }

        /// <summary>
        /// Número de identificación en el país de residencia.
        /// Alfanumérico(20).
        /// </summary>
        public string ID { get; set; }

        #endregion

        #region Métodos Públicos de Instancia

        /// <summary>
        /// Representación textual de esta instancia de Parte.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ($"{CodigoPais}" ?? "") + ($"{IDType}" ?? "") +
                ", " + (ID ?? "");
        }

        #endregion

    }

}
