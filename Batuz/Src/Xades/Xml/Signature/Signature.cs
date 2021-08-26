/*
    This file is part of the Irene.Solutions.Xades (R) project.
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
    develop commercial activities involving the Irene.Solutions.Xades software without
    disclosing the source code of your own applications.
    These activities include: offering paid services to customers as an ASP,
    serving Irene.Solutions.Xades services on the fly in a web application, 
    shipping Irene.Solutions.Xades with a closed source product.
    
    For more information, please contact Irene Solutions SL. at this
    address: info@irenesolutions.com
 */

using System;
using System.Xml.Serialization;

namespace Batuz.TicketBai.Xades.Xml.Signature
{

    /// <summary>
    /// Representa el bloque que contiene la firma en el estandard Xades enveloped.
    /// </summary>
    [Serializable()]
    [XmlRoot(Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public class Signature : XmlElementBase
    {

        /// <summary>
        /// Contiene información sobre qué es lo que se firma y cómo se firma, es decir, 
        /// contiene la información necesaria para crear y validar la firma.
        /// </summary>
        public SignatureSignedInfo SignedInfo { get; set; }

        /// <summary>
        /// Resultado de la firma digital que se ha
        /// aplicado sobre el elemento SignedInfo. El resultado de esta firma está codificado y
        /// contiene un atributo que es único con el que se identificará la firma en procesos
        /// posteriores de validación.
        /// </summary>
        public SignatureSignatureValue SignatureValue { get; set; }

        /// <summary>
        /// Elemento opcional que indica la clave que ha
        /// de utilizarse para validar la firma.
        /// </summary>
        public SignatureKeyInfo KeyInfo { get; set; }


        public SignatureObject Object { get; set; }


        [XmlAttribute()]
        public string Id { get; set; }

    }
}
