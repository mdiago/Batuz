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

using Batuz.TicketBai.Xades.Hash;
using Batuz.TicketBai.Xades.Xml;
using Batuz.TicketBai.Xades.Xml.Canonicalization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Batuz.TicketBai.Xades.Signer
{

    /// <summary>
    /// Gestor de firma Xades.
    /// </summary>
    public class Signer
    {

        #region Variables Privadas de Instancia

        /// <summary>
        /// Gestor de espacios de nombres por defecto.
        /// </summary>
        protected XmlNamespaceManager _XmlNamespaceManager;

        /// <summary>
        /// XmlDocument creado con el XML oríginal cargado.
        /// </summary>
        protected XmlDocument _XmlDocLoadedSource;

        /// <summary>
        /// String XML de la carga original.
        /// </summary>
        protected string _XmlLoadedSource;

        /// <summary>
        /// String XML de la carga original canonicalizado.
        /// </summary>
        protected string _XmlLoadedCanonical;

        /// <summary>
        /// String XML firmado.
        /// </summary>
        protected string _XmlSigned;

        /// <summary>
        /// XmlDocuments con cada una de las
        /// fases de trabajo de la firma.
        /// </summary>
        protected XmlDocument _XmlDocSignedProperties;

        /// <summary>
        /// XmlDocuments con cada una de las
        /// fases de trabajo de la firma.
        /// Documento generado con el bloque KeyInfo
        /// realizado.
        /// </summary>
        protected XmlDocument _XmlDocKeyInfo;

        /// <summary>
        /// XmlDocuments con cada una de las
        /// fases de trabajo de la firma.
        /// Documento generado con el bloque SignedInfo
        /// realizado.
        /// </summary>
        protected XmlDocument _XmlDocSignedInfo;

        /// <summary>
        /// XmlDocuments con cada una de las
        /// fases de trabajo de la firma.
        /// Documento generado con el bloque SignatureValue
        /// realizado.
        /// </summary>
        protected XmlDocument _XmlDocSignatureValue;

        /// <summary>
        /// XmlNodeList utilizados con cada una de las
        /// fases de trabajo de la firma.
        /// XmlNodeList generado con el bloque SignedProperties
        /// realizado.
        /// </summary>
        protected XmlNodeList _XmlSignedPropertiesXmlNodeList;

        /// <summary>
        /// XmlNodeList utilizados con cada una de las
        /// fases de trabajo de la firma.
        /// XmlNodeList generado con el bloque KeyInfo
        /// realizado.
        /// </summary>
        protected XmlNodeList _XmlKeyInfoXmlNodeList;

        /// <summary>
        /// XmlNodeList utilizados con cada una de las
        /// fases de trabajo de la firma.
        /// XmlNodeList generado con el bloque SignedInfo
        /// realizado.
        /// </summary>
        protected XmlNodeList _XmlSignedInfoXmlNodeList;

        /// <summary>
        /// XmlNodeList utilizados con cada una de las
        /// fases de trabajo de la firma.
        /// XmlNodeList generado con el bloque SignatureValue
        /// realizado.
        /// </summary>
        protected XmlNodeList _XmlSignatureValueXmlNodeList;

        /// <summary>
        /// Guid generado como identificador de la firma.
        /// </summary>
        protected string _IdSignature;

        /// <summary>
        /// Guid generado como identificador de objeto.
        /// </summary>
        protected string _IdObject;

        /// <summary>
        /// XML del objeto cargado para firma
        /// </summary>
        protected string _XmlSignedProperties;

        /// <summary>
        /// XML del objeto cargado para firma
        /// </summary>
        protected string _XmlKeyInfo;

        /// <summary>
        /// XML del objeto cargado para firma
        /// </summary>
        protected string _XmlSignedInfo;

        /// <summary>
        /// XML del objeto cargado para firma
        /// </summary>
        protected string _XmlSignature;

        /// <summary>
        /// XML del objeto cargado para firma
        /// </summary>
        protected string _XmlSignatureValue;


        /// <summary>
        /// XML del objeto cargado para firma
        /// </summary>
        protected string _XmlSignedPropertiesCN14;

        /// <summary>
        /// XML del objeto cargado para firma
        /// </summary>
        protected string _XmlKeyInfoCN14;

        /// <summary>
        /// XML del objeto cargado para firma
        /// </summary>
        protected string _XmlSignedInfoCN14;

        /// <summary>
        /// XML del objeto cargado para firma
        /// </summary>
        protected string _XmlSignatureValueCN14;

        /// <summary>
        /// XML del objeto cargado para firma
        /// </summary>
        protected string _SignedPropertiesHash;

        /// <summary>
        /// XML del objeto cargado para firma
        /// </summary>
        protected string _KeyInfoHash;

        #endregion

        #region Construtores de Instancia

        /// <summary>
        /// Contruye un nuevo gestor de firma Xades.
        /// </summary>
        /// <param name="canonicalizationMethod">Método de canonicalización a utilizar.</param>
        /// <param name="digestMethod">Método de cálculo de hash a utilizar.</param>
        /// <param name="signatureHashAlgorithm">Método de cálculo de hash a utilizar con la firma.</param>
        public Signer(ICanonicalizationMethod canonicalizationMethod, 
            IDigestMethod digestMethod, HashAlgorithm signatureHashAlgorithm)
        {

            CanonicalizationMethod = canonicalizationMethod;
            DigestMethod = digestMethod;
            SignatureHashAlgorithm = signatureHashAlgorithm;

        }

        #endregion

        #region Métodos Privados de Instancia

        /// <summary>
        /// Devuelve un XmlDocument a partir del el XML de entrada.
        /// </summary>
        /// <param name="xmlContent">XML para el XmlDocument.</param>
        /// <param name="preserveWhitespace">Indica si se conservan los especios
        /// en blanco.</param>
        /// <returns>XmlDocument a partir del XML de entrada.</returns>
        protected XmlDocument GetXmlDocument(string xmlContent, bool preserveWhitespace = true)
        {

            XmlDocument xmlDoc = new XmlDocument
            {
                PreserveWhitespace = preserveWhitespace
            };
            xmlDoc.LoadXml(xmlContent);

            return xmlDoc;

        }

        /// <summary>
        /// Devuelve la lista de nodos del XML de entrada que
        /// se corresponde con la expresión XPath.
        /// </summary>
        /// <param name="xmlDoc">XML a canonicalizar.</param>
        /// <param name="xpath">Expresión XPath que devuelve el nodeList a canonicalizar.</param>
        /// <param name="namespaces">Espacios de nombres.</param>
        /// <returns>XmlNodeList con los nodos que devuelve la expresión XPath.</returns>
        protected XmlNodeList GetXmlNodeListByXPath(XmlDocument xmlDoc, string xpath, Dictionary<string, string> namespaces = null)
        {

            XmlNamespaceManager nm = new XmlNamespaceManager(xmlDoc.NameTable);

            var nms = namespaces ?? Namespaces.Items;

            foreach (KeyValuePair<string, string> n in nms)
                nm.AddNamespace(n.Key, n.Value);

            return xmlDoc.SelectNodes(xpath, nm);

        }

        /// <summary>
        /// Devuelve el XML de entrada canonicalizado.
        /// </summary>
        /// <param name="xmlContent">XML a canonicalizar.</param>
        /// <returns>XML de entrada canonicalizado.</returns>
        protected string GetCanonical(string xmlContent)
        {
            return CanonicalizationMethod.GetCanonicalString(xmlContent);
        }

        /// <summary>
        /// Devuelve el XML de entrada canonicalizado.
        /// </summary>
        /// <param name="xmlDoc">Documento XML a canonicalizar.</param>
        /// <returns>XML de entrada canonicalizado.</returns>
        protected string GetCanonical(XmlDocument xmlDoc)
        {
            return CanonicalizationMethod.GetCanonicalString(xmlDoc);
        }

        /// <summary>
        /// Recupera el hash del documento, excluyendo en su caso la
        /// firma.
        /// </summary>
        /// <param name="xmlContent">Texto del documento xm.</param>
        /// <returns></returns>
        protected string GetDigestValue(string xmlContent)
        {

            var canonicalXml = GetCanonical(xmlContent);

            return GetStringUTF8HashToBase64(canonicalXml);

        }

        /// <summary>
        /// Recupera el hash del documento, excluyendo en su caso la
        /// firma.
        /// </summary>
        /// <param name="xmlContent">Texto del documento xm.</param>
        /// <returns></returns>
        protected string GetXmlDocumentDigestValue(string xmlContent)
        {

            XmlDocument xmlDoc = new XmlDocument
            {
                PreserveWhitespace = true
            };
            xmlDoc.LoadXml(xmlContent);

            var nodesSignature = xmlDoc.SelectNodes("//ds:Signature", _XmlNamespaceManager);

            if (nodesSignature.Count > 0)
                if (nodesSignature[0].ParentNode != null)
                    nodesSignature[0].ParentNode.RemoveChild(nodesSignature[0]);

            var canonicalXml = GetCanonical(xmlDoc);

            return GetStringUTF8HashToBase64(canonicalXml);


        }

        /// <summary>
        /// Devuelve el hash de una cadena previamente
        /// codificada en UTF8 como una cadena en base 64.
        /// </summary>
        /// <param name="text">Texto del que obtener el hash.</param>
        /// <returns></returns>
        protected string GetStringUTF8HashToBase64(string text)
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

        #endregion

        #region Propiedades Públicas de Instancia

        /// <summary>
        /// Método de canonicalización a utilizar.
        /// </summary>
        public ICanonicalizationMethod CanonicalizationMethod { get; private set; }

        /// <summary>
        /// Algoritmo de calculo de hash.
        /// </summary>
        public IDigestMethod DigestMethod { get; private set; }

        /// <summary>
        /// Algoritmo de calculo de hash.
        /// </summary>
        public HashAlgorithm SignatureHashAlgorithm { get; private set; }

        /// <summary>
        /// XML obtenido tras la ejecución del método
        /// Sign.
        /// </summary>
        public string XmlSigned
        {
            get
            {
                return _XmlSigned;
            }
        }

        /// <summary>
        /// Devuelve el objeto ticketBai firmado.
        /// </summary>
        /// <returns>Objeto ticketBai firmado.</returns>
        public TicketBai TicketBaiSigned
        {
            get
            {               

                TicketBai result = null;

                if(string.IsNullOrEmpty(XmlSigned))
                    return result;

                XmlSerializer serializer = new XmlSerializer(typeof(TicketBai));

                using (Stream reader = new MemoryStream(Encoding.UTF8.GetBytes(XmlSigned)))
                    result = (TicketBai)serializer.Deserialize(reader);

                return result;

               
            }
        }

        #endregion

    }
}
