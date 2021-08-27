using Batuz.TicketBai.Xades.Hash;
using Batuz.TicketBai.Xades.Xml;
using Batuz.TicketBai.Xades.Xml.Canonicalization;
using Batuz.TicketBai.Xades.Xml.Signature;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace Batuz.TicketBai.Xades.Signer
{

    /// <summary>
    /// Gestor de firma para TicketBAI.
    /// </summary>
    public class TicketBaiSigner: Signer
    {

        #region Variables Privadas de Instancia

        /// <summary>
        /// Serialización del texto XML de entrada
        /// para trabajar con los datos.
        /// </summary>
        TicketBai _TicketBaiTmp;


        string _SignatureMethod = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";

        #endregion

        #region Construtores de Instancia

        /// <summary>
        /// Contruye un nuevo gestor de firma Xades para TicketBai.
        /// </summary>
        /// <param name="canonicalizationMethod">Método de canonicalización a utilizar.</param>
        /// <param name="digestMethod">Método de cálculo de hash a utilizar.</param>
        /// <param name="signatureHashAlgorithm">Método de cálculo de hash a utilizar con la firma.</param>
        public TicketBaiSigner(ICanonicalizationMethod canonicalizationMethod, 
            IDigestMethod digestMethod, HashAlgorithm signatureHashAlgorithm) : 
            base(canonicalizationMethod, digestMethod, signatureHashAlgorithm)
        {
        }

        #endregion

        #region Métodos Privados de Instancia

        /// <summary>
        /// Contruye el bloque xml 'SignedInfo'.
        /// </summary>
        /// <param name="xmlText">Texto xml del TicketBai sin firmar.</param>
        /// <returns>Elemento 'SignedInfo' de 'Signature'.</returns>
        private SignatureSignedInfo GetSignedInfo(string xmlText)
        {

            var result = new SignatureSignedInfo()
            {
                CanonicalizationMethod = new SignatureSignedInfoCanonicalizationMethod()
                {
                    Algorithm = CanonicalizationMethod.TransformAlgorithmUrl
                },
                SignatureMethod = new SignatureSignedInfoSignatureMethod()
                {
                    Algorithm = _SignatureMethod
                },
                Reference = new SignatureSignedInfoReference[3]
                {
                    // Object
                    new SignatureSignedInfoReference()
                    {
                        Id = $"Reference-{_IdObject}",
                        URI = "",
                        Type = "http://www.w3.org/2000/09/xmldsig#Object",
                        Transforms = new SignatureSignedInfoReferenceTransform[3]
                        {
                            new SignatureSignedInfoReferenceTransform()
                            {
                                Algorithm = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315"
                            },
                            new SignatureSignedInfoReferenceTransform()
                            {
                                Algorithm = "http://www.w3.org/2000/09/xmldsig#enveloped-signature"
                            },
                            new SignatureSignedInfoReferenceTransform()
                            {
                                Algorithm = "http://www.w3.org/TR/1999/REC-xpath-19991116",
                                XPath = "not(ancestor-or-self::ds:Signature)",
                            }
                        },
                        DigestMethod = new SignatureSignedInfoReferenceDigestMethod()
                        {
                            Algorithm = "http://www.w3.org/2001/04/xmlenc#sha512"
                        },
                        DigestValue = GetXmlDocumentDigestValue(xmlText)
                    },
                    // SignedProperties
                    new SignatureSignedInfoReference()
                    {
                        URI = $"#Signature-{_IdSignature}-SignedProperties",
                        Type = "http://uri.etsi.org/01903#SignedProperties",
                        DigestMethod = new SignatureSignedInfoReferenceDigestMethod()
                        {
                            Algorithm = "http://www.w3.org/2001/04/xmlenc#sha512"
                        },
                        DigestValue = null
                    },
                    // KeyInfo
                    new SignatureSignedInfoReference()
                    {
                        URI = $"#Signature-{_IdSignature}-KeyInfo",
                        DigestMethod = new SignatureSignedInfoReferenceDigestMethod()
                        {
                            Algorithm = "http://www.w3.org/2001/04/xmlenc#sha512"
                        },
                        DigestValue = null
                    }
                },
            };

            return result;

        }

        /// <summary>
        /// Contruye el bloque xml 'KeyInfo'. Este bloque
        /// opcional contiene información del certificado
        /// y su clave pública. Es necesario para poder
        /// validar la firma.
        /// </summary>
        /// <param name="certificate">Certificado con el que se realiza la
        /// operación de firma.</param>
        /// <returns>Elemento 'SignedInfo' de 'Signature'.</returns>
        private SignatureKeyInfo GetKeyInfo(X509Certificate2 certificate)
        {

            var certificateModulus = Convert.ToBase64String(((RSACryptoServiceProvider)certificate.PublicKey.Key).ExportParameters(false).Modulus);
            var certificateExponent = Convert.ToBase64String(((RSACryptoServiceProvider)certificate.PublicKey.Key).ExportParameters(false).Exponent);

            var result = new SignatureKeyInfo()
            {
                Id = $"Signature-{_IdSignature}-KeyInfo",
                X509Data = new SignatureKeyInfoX509Data()
                {
                    X509Certificate = Convert.ToBase64String(certificate.GetRawCertData())
                },
                KeyValue = new SignatureKeyInfoKeyValue()
                {
                    RSAKeyValue = new SignatureKeyInfoKeyValueRSAKeyValue()
                    {
                        Modulus = certificateModulus,
                        Exponent = certificateExponent
                    }
                }
            };

            return result;

        }

        /// <summary>
        /// Construye el bloque 'SignedProperties' el cual contiene
        /// información sobre el certificado utilizado para la firma,
        /// la política de firma utilizada e información sobre el objeto
        /// firmado.
        /// </summary>
        /// <param name="certificate">Certificado con el que se realiza
        /// la operación de firma.</param>
        /// <returns>Elemento 'SignedProperties' de 'Signature'.</returns>
        private QualifyingPropertiesSignedProperties GetSignedProperties(X509Certificate2 certificate)
        {

            var result = new QualifyingPropertiesSignedProperties()
            {
                Id = $"Signature-{_IdSignature}-SignedProperties",
                SignedSignatureProperties = new QualifyingPropertiesSignedPropertiesSignedSignatureProperties()
                {
                    SigningTime = $"{DateTime.Now:yyyy-MM-ddThh:mm:sszzz}",
                    SigningCertificate = new QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSigningCertificate()
                    {
                        Cert = new QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSigningCertificateCert()
                        {
                            CertDigest = new QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSigningCertificateCertCertDigest()
                            {
                                DigestMethod = new DigestMethod()
                                {
                                    Algorithm = "http://www.w3.org/2001/04/xmlenc#sha512"
                                },
                                DigestValue = Convert.ToBase64String(certificate.GetCertHash())
                            },
                            IssuerSerial = new QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSigningCertificateCertIssuerSerial()
                            {
                                X509IssuerName = certificate.IssuerName.Name,
                                X509SerialNumber = certificate.SerialNumber
                            }
                        }
                    },
                    SignaturePolicyIdentifier = new QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSignaturePolicyIdentifier()
                    {
                        SignaturePolicyId = new QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSignaturePolicyIdentifierSignaturePolicyId()
                        {
                            SigPolicyId = new QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSignaturePolicyIdentifierSignaturePolicyIdSigPolicyId()
                            {
                                Identifier = "https://www.batuz.eus/fitxategiak/batuz/ticketbai/sinadura_elektronikoaren_zehaztapenak_especificaciones_de_la_firma_electronica_v1_0.pdf",
                                Description = "",
                                DescriptionSpecified = true
                            },
                            SigPolicyHash = new QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSignaturePolicyIdentifierSignaturePolicyIdSigPolicyHash()
                            {
                                DigestMethod = new DigestMethod()
                                {
                                    Algorithm = "http://www.w3.org/2001/04/xmlenc#sha256"
                                },
                                DigestValue = "Quzn98x3PMbSHwbUzaj5f5KOpiH0u8bvmwbbbNkO9Es="
                            },
                            SigPolicyQualifiers = new QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSignaturePolicyIdentifierSignaturePolicyIdSigPolicyQualifiers()
                            {
                                SigPolicyQualifier = new QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSignaturePolicyIdentifierSignaturePolicyIdSigPolicyQualifiersSigPolicyQualifier()
                                {
                                    SPURI = "https://www.batuz.eus/fitxategiak/batuz/ticketbai/sinadura_elektronikoaren_zehaztapenak_especificaciones_de_la_firma_electronica_v1_0.pdf"
                                }
                            }
                        }
                    }
                },
                SignedDataObjectProperties = new QualifyingPropertiesSignedPropertiesSignedDataObjectProperties()
                {
                    DataObjectFormat = new QualifyingPropertiesSignedPropertiesSignedDataObjectPropertiesDataObjectFormat()
                    {
                        ObjectReference = $"#Reference-{_IdObject}",
                        Description = "",
                        DescriptionSpecified = true,
                        ObjectIdentifier = new QualifyingPropertiesSignedPropertiesSignedDataObjectPropertiesDataObjectFormatObjectIdentifier()
                        {
                            Identifier = new QualifyingPropertiesSignedPropertiesSignedDataObjectPropertiesDataObjectFormatObjectIdentifierIdentifier()
                            {
                                Qualifier = "OIDAsURN",
                                Value = "urn:oid:1.2.840.10003.5.109.10"
                            },
                            Description = "",
                            DescriptionSpecified = true,
                        },
                        MimeType = "text/xml",
                        Encoding = "",
                        EncodingSpecified = true
                    }
                }
            };

            return result;

        }

        /// <summary>
        /// Devuelve el objeto 'Signature' el cual debemos incluir
        /// dentro del cuerpo xml del TicketBai asociado (enveloped).
        /// Falta el valor del hash.
        /// </summary>
        /// <param name="xmlText">Texto del TicketBai a firmar.</param>
        /// <param name="certificate">Certificado a utilziar en la firma.</param>
        /// <returns>Elemento 'Signature' de 'TicketBai'. </returns>
        private Xml.Signature.Signature GetEmptySignature(string xmlText, X509Certificate2 certificate)
        {

            var result = new Xml.Signature.Signature()
            {
                Id = $"Signature-{_IdSignature}-Signature",
                SignedInfo = GetSignedInfo(xmlText),
                SignatureValue = new SignatureSignatureValue()
                {
                    Id = $"Signature-{_IdSignature}-SignatureValue",
                    Value = null
                },
                KeyInfo = GetKeyInfo(certificate),
                Object = new SignatureObject()
                {
                    QualifyingProperties = new QualifyingProperties()
                    {
                        Id = $"Signature-{_IdSignature}-QualifyingProperties",
                        Target = $"#Signature-{_IdSignature}-Signature",
                        SignedProperties = GetSignedProperties(certificate)
                    }
                }
            };

            return result;

        }

        /// <summary>
        /// Soluciona el problema con las Stag con autocierre de Microsoft.
        /// (sustituye ' />' por '/>') eliminando el espacio 
        /// </summary>
        /// <param name="xml">XML a limpiar.</param>
        /// <returns>XML limpio.</returns>
        private string ClearAutoSelfClosedTagSpaces(string xml)
        {

            return Regex.Replace(xml,
                @"(?<=<(\w+:){0,1}[^>]*)\s/>", "/>");

        }

        /// <summary>
        /// Prepara SignedProperties para la firma.
        /// </summary>
        private void PrepareSignedProperties()
        {

            var parser = new XmlParser();

            var xml = parser.GetString(_TicketBaiTmp, Namespaces.Items);

            _XmlDocSignedProperties = GetXmlDocument(xml);
            _XmlSignedPropertiesXmlNodeList = GetXmlNodeListByXPath(_XmlDocSignedProperties,
                "//xades:SignedProperties/descendant-or-self::node()|//xades:SignedProperties//@*");

            _XmlSignedProperties = Regex.Match(xml,
                @"<(\w+:){0,1}SignedProperties[^>]*>[\S\s]+</(\w+:){0,1}SignedProperties>").Value;

            _XmlSignedProperties = ClearAutoSelfClosedTagSpaces(_XmlSignedProperties);

            _XmlSignedPropertiesCN14 = CanonicalizationMethod.GetCanonicalString(_XmlSignedPropertiesXmlNodeList);
            _SignedPropertiesHash = GetDigestValue(_XmlSignedPropertiesCN14);
            _TicketBaiTmp.Signature.SignedInfo.Reference[1].DigestValue = _SignedPropertiesHash;

        }

        /// <summary>
        /// Prepara KeyInfo para la firma.
        /// </summary>
        private void PrepareKeyInfo()
        {

            var parser = new XmlParser();

            var namespaces = new Dictionary<string, string>()
            {
                { "T",          "urn:ticketbai:emision"},
                { "ds",         "http://www.w3.org/2000/09/xmldsig#"},
            };

            var xml = parser.GetString(_TicketBaiTmp, namespaces);

            _XmlDocKeyInfo = GetXmlDocument(xml);
            _XmlKeyInfoXmlNodeList = GetXmlNodeListByXPath(_XmlDocKeyInfo,
                "//ds:KeyInfo/descendant-or-self::node()|//ds:KeyInfo//@*");

            _XmlKeyInfo = Regex.Match(xml,
                @"<(\w+:){0,1}KeyInfo[^>]*>[\S\s]+</(\w+:){0,1}KeyInfo>").Value;

            _XmlKeyInfo = ClearAutoSelfClosedTagSpaces(_XmlKeyInfo);

            _XmlKeyInfoCN14 = CanonicalizationMethod.GetCanonicalString(_XmlKeyInfoXmlNodeList);
            _KeyInfoHash = GetDigestValue(_XmlKeyInfoCN14);
            _TicketBaiTmp.Signature.SignedInfo.Reference[2].DigestValue = _KeyInfoHash;

        }

        /// <summary>
        /// Cálcula firma y actualiza SignatureValue.
        /// </summary>
        /// <param name="certificate">Certificado con el que realizar la firma.</param>
        private void ComputeSignature(X509Certificate2 certificate)
        {

            var parser = new XmlParser();

            var namespaces = new Dictionary<string, string>()
            {
                { "T",          "urn:ticketbai:emision"},
                { "ds",         "http://www.w3.org/2000/09/xmldsig#"},
            };

            byte[] DataToSign = CanonicalizationMethod.Encoding.GetBytes(_XmlSignedInfoCN14);

            try
            {

                RSACryptoServiceProvider pk = (RSACryptoServiceProvider)certificate.PrivateKey;
                byte[] Sign = pk.SignData(DataToSign, SignatureHashAlgorithm);

                _TicketBaiTmp.Signature.SignatureValue = new SignatureSignatureValue()
                {
                    Id = $"Signature-{_IdSignature}-SignatureValue",
                    Value = Convert.ToBase64String(Sign)
                };

                var xml = parser.GetString(_TicketBaiTmp, namespaces);

                _XmlDocSignatureValue = GetXmlDocument(xml);

                _XmlSignatureValueXmlNodeList = GetXmlNodeListByXPath(_XmlDocSignedInfo,
                    "//ds:SignatureValue/descendant-or-self::node()|//ds:SignatureValue//@*");

                _XmlSignatureValue = Regex.Match(xml,
                    @"<(\w+:){0,1}SignatureValue(\s[^>]*>|>)[\S\s]+</(\w+:){0,1}SignatureValue>").Value;

                _XmlSignatureValue = ClearAutoSelfClosedTagSpaces(_XmlSignatureValue);

                _XmlSignatureValueCN14 = CanonicalizationMethod.GetCanonicalString(_XmlSignedInfoXmlNodeList);

            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}.\nUpdate CSP for SHA256 in your certificate:\n" +
                    $"certutil -importPFX -csp \"Microsoft Enhanced RSA and AES Cryptographic Provider\" -v C:\\Path\\cert.pfx");
            }

        }

        /// <summary>
        /// Prepara SignedInfo para la firma.
        /// </summary>
        /// <param name="certificate">Certificado con el que realizar la firma.</param>
        private void PrepareSignedInfo(X509Certificate2 certificate)
        {

            var parser = new XmlParser();

            var namespaces = new Dictionary<string, string>()
            {
                { "T",          "urn:ticketbai:emision"},
                { "ds",         "http://www.w3.org/2000/09/xmldsig#"},
            };

            var xml = parser.GetString(_TicketBaiTmp, namespaces);

            _XmlDocSignedInfo = GetXmlDocument(xml);
            _XmlSignedInfoXmlNodeList = GetXmlNodeListByXPath(_XmlDocSignedInfo,
                "//ds:SignedInfo/descendant-or-self::node()|//ds:SignedInfo//@*");

            _XmlSignedInfo = Regex.Match(xml,
                @"<(\w+:){0,1}SignedInfo[^>]*>[\S\s]+</(\w+:){0,1}SignedInfo>").Value;

            _XmlSignedInfo = ClearAutoSelfClosedTagSpaces(_XmlSignedInfo);

            _XmlSignedInfoCN14 = CanonicalizationMethod.GetCanonicalString(_XmlSignedInfoXmlNodeList);

            ComputeSignature(certificate);

        }

        /// <summary>
        /// Devuelve el XmlNamespaceManager para un documento
        /// firmado.
        /// </summary>
        /// <param name="xmlDoc">XmlDocument para el que se va a crear.</param>
        /// <returns>XmlNamespaceManager para un documento.</returns>
        private XmlNamespaceManager GetDefaultXmlNamespaceManager(XmlDocument xmlDoc)
        {

            XmlNamespaceManager result = new XmlNamespaceManager(xmlDoc.NameTable);

            foreach (KeyValuePair<string, string> n in Namespaces.Items)
                result.AddNamespace(n.Key, n.Value);

            return result;

        }

        /// <summary>
        /// Obtiene el texto XML de un XmlDocument de entrada.
        /// </summary>
        /// <param name="xmlDoc">XmlDocument del que obtener el texto XML.</param>
        /// <returns>Texto XML del documento.</returns>
        private string GetXmlText(XmlDocument xmlDoc)
        {

            string result = null;

            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
            {
                xmlDoc.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                result = stringWriter.GetStringBuilder().ToString();
            }

            return result;

        }

        /// <summary>
        /// Devuelve el XmlNode del TicketBai orígen para la firma.
        /// </summary>
        /// <returns>XmlNode del TicketBai orígen para la firma.</returns>
        private XmlNode GetSourceTicketBai()
        {

            if (_XmlDocLoadedSource == null)
                throw new Exception("No XmlDocLoadedSource found.");

            if (_XmlNamespaceManager == null)
                throw new Exception("No XmlNamespaceManager found.");


            var ticketBais = _XmlDocLoadedSource.SelectNodes(@"//T:TicketBai", _XmlNamespaceManager);

            if (ticketBais.Count == 0)
                throw new Exception("No T:TicketBai found.");

            return _XmlDocLoadedSource.SelectNodes(@"//T:TicketBai", _XmlNamespaceManager)[0];

        }

        /// <summary>
        /// Borra el elmento Sginature del nodo TicketBai.
        /// </summary>
        /// <param name="ticketBai">Nodo TicketBai.</param>
        private void DeleteSignature(XmlNode ticketBai)
        {

            if (_XmlNamespaceManager == null)
                throw new Exception("No XmlNamespaceManager found.");

            var signatures = _XmlDocLoadedSource.SelectNodes(
                @"//ds:Signature/descendant-or-self::node()|//ds:Signature//@*", _XmlNamespaceManager);

            if (signatures.Count > 0)
                ticketBai.RemoveChild(signatures[0]);
        }

        /// <summary>
        /// Devuelve el texto XML de la firma generada.
        /// </summary>
        /// <returns>Texto XML de la firma generada.</returns>
        private string GetNewSignatureXmlText()
        {

            if (_TicketBaiTmp == null)
                throw new Exception("No TicketBai found.");

            if (_XmlNamespaceManager == null)
                throw new Exception("No XmlNamespaceManager found.");

            var parser = new XmlParser();

            var xml = parser.GetString(_TicketBaiTmp, Namespaces.Items);
            XmlDocument xmlDocPreSigned = new XmlDocument();
            xmlDocPreSigned.PreserveWhitespace = true;
            xmlDocPreSigned.LoadXml(_XmlLoadedSource);

            var signatures = xmlDocPreSigned.SelectNodes(@"//ds:Signature/descendant-or-self::node()|//ds:Signature//@*", _XmlNamespaceManager);
            XmlNode newSignature = null;

            if (signatures.Count > 0)
                newSignature = signatures[0];


            return newSignature.OuterXml;

        }

        /// <summary>
        /// Inserta una nueva firma en el documento XML orígen.
        /// </summary>
        private void InsertSignatureInXmlDocLoadedSource()
        {

            var ticketBai = GetSourceTicketBai();

            if (_TicketBaiTmp == null)
                throw new Exception("No TicketBai found.");

            DeleteSignature(ticketBai);

            _XmlSignature = GetNewSignatureXmlText();

            XmlDocumentFragment signature = _XmlDocLoadedSource.CreateDocumentFragment();
            signature.InnerXml = _XmlSignature;

            ticketBai.AppendChild(signature);

        }

        /// <summary>
        /// Actualiza los bloques de la firma canonicalizados para el digest
        /// con el xml utilizado antes de la canonicalización.
        /// </summary>
        private void UpdateSignatureXml()
        {

            // Incluyo los xml orígen
            var signedProperties = _XmlDocLoadedSource.SelectNodes(@"//xades:SignedProperties", _XmlNamespaceManager)[0];
            signedProperties.InnerXml = Regex.Match(_XmlSignedProperties,
                @"(?<=<(\w+:){0,1}SignedProperties[^>]*>)[\S\s]+(?=</(\w+:){0,1}SignedProperties>)").Value;

            var keyInfo = _XmlDocLoadedSource.SelectNodes(@"//ds:KeyInfo", _XmlNamespaceManager)[0];
            keyInfo.InnerXml = Regex.Match(_XmlKeyInfo,
                @"(?<=<(\w+:){0,1}KeyInfo[^>]*>)[\S\s]+(?=</(\w+:){0,1}KeyInfo>)").Value;

            var signedInfo = _XmlDocLoadedSource.SelectNodes(@"//ds:SignedInfo", _XmlNamespaceManager)[0];
            signedInfo.InnerXml = Regex.Match(_XmlSignedInfo,
                @"(?<=<(\w+:){0,1}SignedInfo[^>]*>)[\S\s]+(?=</(\w+:){0,1}SignedInfo>)").Value;

            var signatureValue = _XmlDocLoadedSource.SelectNodes(@"//ds:SignatureValue", _XmlNamespaceManager)[0];
            signatureValue.InnerXml = Regex.Match(_XmlSignatureValue,
                @"(?<=<(\w+:){0,1}SignatureValue[^>]*>)[\S\s]+(?=</(\w+:){0,1}SignatureValue>)").Value;

            // Convierto el XmlDocument en texto XML
            _XmlSigned = GetXmlText(_XmlDocLoadedSource);

            // Limpio las etiquetas con autocierre de espaciós al final.
            _XmlSigned = ClearAutoSelfClosedTagSpaces(_XmlSigned);

        }

        /// <summary>
        /// Valida la firma del xml cargado.
        /// </summary>
        /// <returns>Mensaje informando del resultado de la validación.</returns>
        private bool ValidateSignature()
        {

            if (_TicketBaiTmp.Signature == null)
                throw new InvalidOperationException("No Signature found");

            var signature = Convert.FromBase64String(_TicketBaiTmp.Signature.SignatureValue.Value);
            var modulus = Convert.FromBase64String(_TicketBaiTmp.Signature.KeyInfo.KeyValue.RSAKeyValue.Modulus);
            var exponent = Convert.FromBase64String(_TicketBaiTmp.Signature.KeyInfo.KeyValue.RSAKeyValue.Exponent);

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            RSAParameters RSAKeyInfo = new RSAParameters();

            RSAKeyInfo.Modulus = modulus;
            RSAKeyInfo.Exponent = exponent;

            rsa.ImportParameters(RSAKeyInfo);

            var xmlSignedInfoXmlNodeList = GetXmlNodeListByXPath(_XmlDocLoadedSource,
                "//ds:SignedInfo/descendant-or-self::node()|//ds:SignedInfo//@*");

            var xmlSignedInfo = Regex.Match(_XmlLoadedSource,
                @"<(\w+:){0,1}SignedInfo[^>]*>[\S\s]+</(\w+:){0,1}SignedInfo>").Value;

            xmlSignedInfo = ClearAutoSelfClosedTagSpaces(xmlSignedInfo);

            var xmlSignedInfoCN14 = CanonicalizationMethod.GetCanonicalString(xmlSignedInfoXmlNodeList);

            byte[] signedData = CanonicalizationMethod.Encoding.GetBytes(xmlSignedInfoCN14);

            return rsa.VerifyData(signedData, new SHA256Managed(), signature);

        }

        /// <summary>
        /// Valida la firma del xml cargado.
        /// </summary>
        /// <returns>Mensaje informando del resultado de la validación.</returns>
        private bool ValidateSignaturePropertiesRef()
        {

            if (_TicketBaiTmp.Signature == null)
                throw new InvalidOperationException("No Signature found");

            var xmlSignedPropertiesXmlNodeList = GetXmlNodeListByXPath(_XmlDocLoadedSource,
                "//xades:SignedProperties/descendant-or-self::node()|//xades:SignedProperties//@*");

            var xmlSignedProperties = Regex.Match(_XmlLoadedSource,
                @"<(\w+:){0,1}SignedProperties[^>]*>[\S\s]+</(\w+:){0,1}SignedProperties>").Value;

            xmlSignedProperties = ClearAutoSelfClosedTagSpaces(xmlSignedProperties);

            var xmlSignedPropertiesCN14 = CanonicalizationMethod.GetCanonicalString(xmlSignedPropertiesXmlNodeList);
            var signedPropertiesHash = GetDigestValue(xmlSignedPropertiesCN14);

            SignatureSignedInfoReference reference = null;

            foreach (var currentRef in _TicketBaiTmp.Signature.SignedInfo.Reference)
                if (currentRef.Type == "http://uri.etsi.org/01903#SignedProperties")
                    reference = currentRef;

            if (reference == null)
                throw new InvalidOperationException("No 'KeyInfo' found.");

            return reference.DigestValue == signedPropertiesHash;


        }

        /// <summary>
        /// Valida la firma del xml cargado.
        /// </summary>
        /// <returns>Mensaje informando del resultado de la validación.</returns>
        private bool ValidateKeyInfoRef()
        {

            if (_TicketBaiTmp.Signature == null)
                throw new InvalidOperationException("No Signature found");

            var xmlKeyInfoXmlNodeList = GetXmlNodeListByXPath(_XmlDocLoadedSource,
                "//ds:KeyInfo/descendant-or-self::node()|//ds:KeyInfo//@*");

            var xmlKeyInfo = Regex.Match(_XmlLoadedSource,
                @"<(\w+:){0,1}KeyInfo[^>]*>[\S\s]+</(\w+:){0,1}KeyInfo>").Value;

            xmlKeyInfo = ClearAutoSelfClosedTagSpaces(xmlKeyInfo);

            var xmlKeyInfoCN14 = CanonicalizationMethod.GetCanonicalString(xmlKeyInfoXmlNodeList);
            var keyInfoHash = GetDigestValue(xmlKeyInfoCN14);

            SignatureSignedInfoReference reference = null;

            foreach (var currentRef in _TicketBaiTmp.Signature.SignedInfo.Reference)
                if (currentRef.URI.EndsWith("KeyInfo"))
                    reference = currentRef;

            if (reference == null)
                throw new InvalidOperationException("No 'http://uri.etsi.org/01903#SignedProperties' found.");

            return reference.DigestValue == keyInfoHash;

        }

        /// <summary>
        /// Valida la firma del xml cargado.
        /// </summary>
        /// <returns>Mensaje informando del resultado de la validación.</returns>
        private bool ValidateTicketBaiRef()
        {

            if (_TicketBaiTmp.Signature == null)
                throw new InvalidOperationException("No Signature found");

            SignatureSignedInfoReference reference = null;

            foreach (var currentRef in _TicketBaiTmp.Signature.SignedInfo.Reference)
                if (currentRef.Type == "http://www.w3.org/2000/09/xmldsig#Object")
                    reference = currentRef;

            if (reference == null)
                throw new InvalidOperationException("No 'http://www.w3.org/2000/09/xmldsig#Object' found.");

            var docHash = GetXmlDocumentDigestValue(_XmlLoadedCanonical);

            return reference.DigestValue == docHash;


        }

        #endregion

        #region Métodos Públicos de Instancia

        /// <summary>
        /// Carga el texto xml de un TicketBai
        /// </summary>
        /// <param name="xml"></param>
        public void Load(string xml)
        {

            _XmlLoadedSource = xml;
            _XmlLoadedCanonical = GetCanonical(_XmlLoadedSource);

            XmlSerializer serializer = new XmlSerializer(typeof(TicketBai));

            using (Stream reader = new MemoryStream(Encoding.UTF8.GetBytes(_XmlLoadedCanonical)))
                _TicketBaiTmp = (TicketBai)serializer.Deserialize(reader);

            _XmlDocLoadedSource = GetXmlDocument(_XmlLoadedSource);

            _XmlNamespaceManager = GetDefaultXmlNamespaceManager(_XmlDocLoadedSource);

        }

        /// <summary>
        /// Realiza las operaciones de preparación de la firma.
        /// </summary>
        /// <param name="certificate">Certificado con el que realizar la firma.</param>
        public void Sign(X509Certificate2 certificate)
        {

            _IdSignature = $"{Guid.NewGuid()}";
            _IdObject = $"{Guid.NewGuid()}";

            _TicketBaiTmp.Signature = GetEmptySignature(_XmlLoadedCanonical, certificate);

            PrepareSignedProperties();
            PrepareKeyInfo();
            PrepareSignedInfo(certificate);
            UpdateSignatureXml();

        }

        /// <summary>
        /// Devuelve un mensaje con la validación de la firma.
        /// </summary>
        /// <returns>Devuelve un mensaje con la validación de la firma.</returns>
        public bool Validate()
        {

            return ValidateTicketBaiRef() &&
                ValidateSignaturePropertiesRef() &&
                ValidateKeyInfoRef() &&
                ValidateSignature();

        }

        /// <summary>
        /// Devuelve un mensaje con la validación de la firma.
        /// </summary>
        /// <returns>Devuelve un mensaje con la validación de la firma.</returns>
        public string GetValidateInfo()
        {

            string result = "";

            result += $"DIGEST DE LA REFERENCIA OBJETO " + (ValidateTicketBaiRef() ? "VALIDO" : "INVALIDO");

            result += $"\nDIGEST DE LA REFERENCIA SIGNED PROPERTIES " + (ValidateSignaturePropertiesRef() ? "VALIDO" : "INVALIDO");

            result += $"\nDIGEST DE LA REFERENCIA KEY INFO " + (ValidateKeyInfoRef() ? "VALIDO" : "INVALIDO");

            result += $"\nFIRMA " + (ValidateSignature() ? "VALIDA" : "INVALIDA");

            return result;

        }

        #endregion

    }
}
