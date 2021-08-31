/*
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

using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;

namespace Batuz.TicketBai.Xades.Xml.Canonicalization
{

    /// <summary>
    /// Canonicaliza utilizando la transformación definida en
    /// http://www.w3.org/TR/2001/REC-xml-c14n-20010315
    /// </summary>
    public class CanonicalizationMethodDsigC14N : ICanonicalizationMethod
    {

        #region Variables Privadas de Instancia

        /// <summary>
        /// Url a incluir en el atributo 'Transform Algorithm'
        /// del elemento signature.
        /// </summary>
        readonly string _TransformAlgorithmUrl = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";

        #endregion

        #region Construtores de Instancia

        /// <summary>
        /// Constructor.
        /// </summary>
        public CanonicalizationMethodDsigC14N()
        {

            Encoding = Encoding.GetEncoding("UTF-8");

        }


        #endregion

        #region Propiedades Públicas de Instancia

        /// <summary>
        /// Url a incluir en el atributo 'Transform Algorithm'
        /// del elemento signature.
        /// </summary>
        public string TransformAlgorithmUrl
        {
            get
            {
                return _TransformAlgorithmUrl;
            }
        }

        /// <summary>
        /// Codificación de texto a utilizar. UTF8 por defecto.
        /// </summary>
        public Encoding Encoding { get; set; }

        #endregion

        #region Métodos Públicos de Instancia

        /// <summary>
        /// Devuelve el XML de entrada canonicalizado.
        /// </summary>
        /// <param name="xmlContent">XML a canonicalizar.</param>
        /// <returns>XML de entrada canonicalizado.</returns>
        public string GetCanonicalString(string xmlContent)
        {

            XmlDocument xmlDoc = new XmlDocument
            {
                PreserveWhitespace = true
            };
            xmlDoc.LoadXml(xmlContent);

            XmlDsigC14NTransform xmlTransform = new XmlDsigC14NTransform();
            xmlTransform.LoadInput(xmlDoc);
            MemoryStream ms = (MemoryStream)xmlTransform.GetOutput(typeof(MemoryStream));

            return Encoding.GetString(ms.ToArray());

        }

        /// <summary>
        /// Devuelve el XML de entrada canonicalizado.
        /// </summary>
        /// <param name="xmlDoc">Documento XML a canonicalizar.</param>
        /// <returns>XML de entrada canonicalizado.</returns>
        public string GetCanonicalString(XmlDocument xmlDoc)
        {

            XmlDsigC14NTransform xmlTransform = new XmlDsigC14NTransform();
            xmlTransform.LoadInput(xmlDoc);
            MemoryStream ms = (MemoryStream)xmlTransform.GetOutput(typeof(MemoryStream));

            return Encoding.UTF8.GetString(ms.ToArray());

        }

        /// <summary>
        /// Devuelve el XML de entrada canonicalizado.
        /// </summary>
        /// <param name="xmlNodeList">Lista de nodos a canonicalizar.</param>
        /// <returns>XML de entrada canonicalizado.</returns>
        public string GetCanonicalString(XmlNodeList xmlNodeList)
        {

            XmlDsigC14NTransform xmlTransform = new XmlDsigC14NTransform();
            xmlTransform.LoadInput(xmlNodeList);
            MemoryStream ms = (MemoryStream)xmlTransform.GetOutput(typeof(MemoryStream));

            return Encoding.UTF8.GetString(ms.ToArray());

        }

        /// <summary>
        /// Devuelve el XML de entrada canonicalizado.
        /// </summary>
        /// <param name="xmlContent">XML a canonicalizar.</param>
        /// <param name="xpath">Expresión XPath que devuelve el nodeList a canonicalizar.</param>
        /// <param name="namespaces">Espacios de nombres.</param>
        /// <returns>XML de entrada canonicalizado.</returns>
        public string GetCanonicalString(string xmlContent, string xpath, Dictionary<string, string> namespaces = null)
        {

            XmlDocument xmlDoc = new XmlDocument
            {
                PreserveWhitespace = true
            };
            xmlDoc.LoadXml(xmlContent);

            XmlNamespaceManager nm = new XmlNamespaceManager(xmlDoc.NameTable);

            var nms = namespaces ?? Namespaces.Items;

            foreach (KeyValuePair<string, string> n in nms)
                nm.AddNamespace(n.Key, n.Value);

            XmlNodeList xmlSignedProperties = xmlDoc.SelectNodes(xpath, nm);

            XmlDsigC14NTransform xmlTransform = new XmlDsigC14NTransform();
            xmlTransform.LoadInput(xmlSignedProperties);
            MemoryStream ms = (MemoryStream)xmlTransform.GetOutput(typeof(MemoryStream));

            return Encoding.UTF8.GetString(ms.ToArray());

        }

        #endregion


    }

}
