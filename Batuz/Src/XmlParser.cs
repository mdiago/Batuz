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
using System.IO;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Batuz
{

    /// <summary>
    /// Serializador xml.
    /// </summary>
    public class XmlParser
    {

        #region Construtores de Instancia

        /// <summary>
        /// Constructor.
        /// </summary>
        public XmlParser()
        {

            Encoding = Encoding.GetEncoding("UTF-8");

        }

        #endregion

        #region Métodos Privados de Instancia

        /// <summary>
        /// Codificación de texto a utilizar. UTF8 por defecto.
        /// </summary>
        public Encoding Encoding { get; set; }

        #endregion

        #region Métodos Públicos de Instancia

        /// <summary>
        /// Serializa el objeto como xml y lo devuelve
        /// como archivo xml en una cadena.
        /// </summary>
        /// <param name="instance">Instancia de objeto a serializar.</param>
        /// <param name="namespaces">Espacios de nombres.</param> 
        /// <param name="indent">Indica si se debe utilizar indentación.</param>
        /// <param name="omitXmlDeclaration">Indica si se se omite la delcaración xml.</param>
        /// <param name="omitRootNamespace">Indica si se se omite la delcaración del espacio de nombres raiz.</param> 
        /// <returns>string con el archivo xml.</returns>
        public string GetString(object instance, Dictionary<string, string> namespaces, bool indent = false, bool omitXmlDeclaration = true, bool omitRootNamespace = false)
        {

            return Encoding.GetString(GetBytes(instance, namespaces, indent, omitXmlDeclaration, omitRootNamespace));

        }

        /// <summary>
        /// Serializa el objeto como xml y lo devuelve
        /// como archivo xml canonicalizado en una cadena.
        /// </summary>
        /// <param name="instance">Instancia de objeto a serializar.</param>
        /// <param name="namespaces">Espacios de nombres.</param> 
        /// <param name="indent">Indica si se debe utilizar indentación.</param>
        /// <param name="omitXmlDeclaration">Indica si se se omite la delcaración xml.</param>
        /// <returns>string con el archivo xml.</returns>
        public string GetCanonicalString(object instance, Dictionary<string, string> namespaces, bool indent = false, bool omitXmlDeclaration = true)
        {

            var xmlContent = Encoding.GetString(GetBytes(instance, namespaces, indent, omitXmlDeclaration));

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlContent);

            XmlDsigC14NTransform xmlTransform = new XmlDsigC14NTransform();
            xmlTransform.LoadInput(xmlDoc);
            MemoryStream ms = (MemoryStream)xmlTransform.GetOutput(typeof(MemoryStream));

            return Encoding.GetString(ms.ToArray());

        }

        /// <summary>
        /// Serializa el objeto como xml y lo devuelve
        /// como archivo xml como cadena de bytes.
        /// </summary>
        /// <param name="instance">Instancia de objeto a serializar.</param>
        /// <param name="namespaces">Espacios de nombres.</param> 
        /// <param name="indent">Indica si se debe utilizar indentación.</param>
        /// <param name="omitXmlDeclaration">Indica si se se omite la delcaración xml.</param>
        /// <param name="omitRootNamespace">Indica si se se omite la delcaración del espacio de nombres raiz.</param> 
        /// <returns>string con el archivo xml.</returns>
        public byte[] GetBytes(object instance, Dictionary<string, string> namespaces, 
            bool indent = false, bool omitXmlDeclaration = true, bool omitRootNamespace = false)
        {

            XmlSerializer serializer = new XmlSerializer(instance.GetType());

            var xmlSerializerNamespaces = new XmlSerializerNamespaces();

            if (!omitRootNamespace)
                xmlSerializerNamespaces.Add("T", "urn:ticketbai:emision");

            foreach (KeyValuePair<string, string> ns in namespaces)
                xmlSerializerNamespaces.Add(ns.Key, ns.Value);

            var ms = new MemoryStream();
            byte[] result = null;

            var settings = new XmlWriterSettings
            {
                Indent = indent,
                IndentChars = "",
                Encoding = Encoding,
                OmitXmlDeclaration = omitXmlDeclaration
            };

            using (var writer = new StreamWriter(ms))
            {
                using (var xmlWriter = XmlWriter.Create(writer, settings))
                {

                    serializer.Serialize(xmlWriter, instance, xmlSerializerNamespaces);
                    result = ms.ToArray();

                }
            }

            return result;

        }

        /// <summary>
        /// Obtiene una intancia de un tipo determinado
        /// a partir de un string con un xml válido para 
        /// la representación del tipo.
        /// </summary>
        /// <typeparam name="T">Tipo a deserializar.</typeparam>
        /// <param name="xml">XNL de una instancia del tipo.</param>
        /// <returns>Objeto del tipo obtenido del texto XML.</returns>
        public T GetInstance<T>(string xml)
        {

            T result = default(T);

            XmlSerializer serializer =
                new XmlSerializer(typeof(T));

            var instance = new XmlSerializer(typeof(T));

            using (TextReader reader = new StringReader(xml))
                result = (T)serializer.Deserialize(reader);

            return result;

        }

        #endregion

    }
}
