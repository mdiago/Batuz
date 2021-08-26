using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Xades.Xml.Canonicalization
{
    public class CanonicalizationMethodCN14 : ICanonicalizationMethod
    {
        /// <summary>
        /// Url a incluir en el atributo 'Transform Algorithm'
        /// del elemento signature.
        /// </summary>
        string _TransformAlgorithmUrl = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";

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

        /// <summary>
        /// Constructor.
        /// </summary>
        public CanonicalizationMethodCN14()
        {

            Encoding = Encoding.GetEncoding("UTF-8");

        }

        /// <summary>
        /// Devuelve el XML de entrada canonicalizado.
        /// </summary>
        /// <param name="xmlContent">XML a canonicalizar.</param>
        /// <returns>XML de entrada canonicalizado.</returns>
        public string GetCanonicalString(string xmlContent)
        {

            // 1. Remove the xml declaration (<?xml...).
            var canonical = Regex.Replace(xmlContent, @"<\?[^>]*[^<]", "");

            // 2. Start at precisely the "<" character opening the root element and end at the ">" character that closes this element.
            canonical = Regex.Replace(canonical, @"[^>]+$", "");

            // 3. Replace all CR-LF line endings with the newline character 0x0A.
            canonical = Regex.Replace(canonical, @"\r", "");

            // 4. Remove the Signature element but leave any surrounding whitespace intact.
            canonical = Regex.Replace(canonical, @"<(\w+:){0,1}Signature(\s|>)[\s\S]+</(\w+:){0,1}Signature>", "");

            return canonical;

        }

    }
}
