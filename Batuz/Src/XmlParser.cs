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

        /// <summary>
        /// Codificación de texto a utilizar. UTF8 por defecto.
        /// </summary>
        public Encoding Encoding { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public XmlParser() 
        {

            Encoding = Encoding.GetEncoding("UTF-8");

        }

        /// <summary>
        /// Serializa el objeto como xml y lo devuelve
        /// como archivo xml en una cadena.
        /// </summary>
        /// <param name="instance">Instancia de objeto a serializar.</param>
        /// <param name="namespaces">Espacios de nombres.</param> 
        /// <param name="indent">Indica si se debe utilizar indentación.</param>
        /// <param name="omitXmlDeclaration">Indica si se se omite la delcaración xml.</param>
        /// <returns>string con el archivo xml.</returns>
        public string GetString(object instance, Dictionary<string, string> namespaces, bool indent = false, bool omitXmlDeclaration = true)
        {

            return Encoding.GetString(GetBytes(instance, namespaces, indent, omitXmlDeclaration));

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
        /// <returns>string con el archivo xml.</returns>
        public byte[] GetBytes(object instance, Dictionary<string, string> namespaces, bool indent = false, bool omitXmlDeclaration = true)
        {
            
            XmlSerializer serializer = new XmlSerializer(instance.GetType());

            var xmlSerializerNamespaces = new XmlSerializerNamespaces(new XmlQualifiedName[] {
                new XmlQualifiedName("T", "urn:ticketbai:emision") 
                }
            );

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




    }
}
