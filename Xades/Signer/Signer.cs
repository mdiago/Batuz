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
using Xades.Hash;
using Xades.Xml.Canonicalization;

namespace Xades.Signer
{
    public class Signer
    {

        /// <summary>
        /// Método de canonicalización a utilizar.
        /// </summary>
        public ICanonicalizationMethod CanonicalizationMethod { get; private set; }

        /// <summary>
        /// Algoritmo de calculo de hash.
        /// </summary>
        public IDigestMethod DigestMethod { get; private set; }

        /// <summary>
        /// Representa un creador de firma Xades.
        /// </summary>
        /// <param name="canonicalizationMethod">Método de canonicalización a utilizar.</param>
        /// <param name="digestMethod">Método de cálculo de hash a utilizar.</param>
        public Signer(ICanonicalizationMethod canonicalizationMethod, IDigestMethod digestMethod) 
        {

            CanonicalizationMethod = canonicalizationMethod;
            DigestMethod = digestMethod;

        }

        public string GetCanonical(string xmlContent) 
        { 
            return CanonicalizationMethod.GetCanonicalString(xmlContent);
        }

        public string GetDigestValue(string xmlContent) 
        {

            var canonicalXml = GetCanonical(xmlContent);

            return GetStringUTF8HashToBase64(canonicalXml);


        }

        /// <summary>
        /// Devuelve el hash de una cadena previamente
        /// codificada en UTF8 como una cadena en base 64.
        /// </summary>
        /// <param name="text">Texto del que obtener el hash.</param>
        /// <returns></returns>
        public string GetStringUTF8HashToBase64(string text)
        {
            return Convert.ToBase64String(GetStringUTF8Hash(text));
        }

        /// <summary>
        /// Devuelve hash de una cadena tras codificarla
        /// en UTF8.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private byte[] GetStringUTF8Hash(string text)
        {
            return DigestMethod.ComputeHash(CanonicalizationMethod.Encoding.GetBytes(text));
        }



    }
}
