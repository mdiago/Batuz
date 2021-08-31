﻿/*
    This file is part of the Batuz (R) project.
    Copyright (c) 2021-2022 Irene Solutions SL
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
    develop commercial activities involving the Batuz software without
    disclosing the source code of your own applications.
    These activities include: offering paid services to customers as an ASP,
    serving Batuz services on the fly in a web application, 
    shipping Batuz with a closed source product.
    
    For more information, please contact Irene Solutions SL. at this
    address: info@irenesolutions.com
 */

using System;
using System.Xml.Serialization;

namespace Batuz.TicketBai.Xades.Xml.Signature
{

    /// <summary>
    /// Datos del objeto firmado.
    /// </summary>
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    public class QualifyingPropertiesSignedPropertiesSignedDataObjectPropertiesDataObjectFormat
    {

        #region Propiedades Públicas de Instancia

        /// <summary>
        /// Descripción del objeto-
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Indicador de si está especificada la descripción.
        /// Si es true, el serializador incluirá la descripción
        /// con cadena vacía como una etiqueta vacía.
        /// </summary>
        [XmlIgnore]
        public bool DescriptionSpecified { get; set; }

        /// <summary>
        /// Identificador del objeto según IETF RFC 3061.
        /// </summary>
        public QualifyingPropertiesSignedPropertiesSignedDataObjectPropertiesDataObjectFormatObjectIdentifier ObjectIdentifier { get; set; }

        /// <summary>
        /// Tipo mime.
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Codificación de texto.
        /// </summary>
        public string Encoding { get; set; }

        /// <summary>
        /// Indicador para el serializador.
        /// </summary>
        [XmlIgnore]
        public bool EncodingSpecified { get; set; }

        /// <summary>
        /// Referencia al objeto.
        /// </summary>
        [XmlAttribute()]
        public string ObjectReference { get; set; }

        #endregion

        #region Métodos Públicos de Instancia

        /// <summary>
        /// Representación textual de la instancia.
        /// </summary>
        /// <returns>Representación textual de la instancia.</returns>
        public override string ToString()
        {
            return $"{ObjectReference}";
        }

        #endregion

    }
}
