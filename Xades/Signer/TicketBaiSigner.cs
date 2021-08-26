using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xades.Hash;
using Xades.Xml.Canonicalization;
using Xades.Xml.Signature;

namespace Xades.Signer
{
    public class TicketBaiSigner: Signer
    {

        string _XmlLoadedSource;
        string _XmlLoadedCanonical;

        string _SignatureMethod = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";

        public TicketBaiSigner(ICanonicalizationMethod canonicalizationMethod, IDigestMethod digestMethod) : base(canonicalizationMethod, digestMethod) 
        {         
        }

        public SignatureSignedInfo GetSignedInfo(string xmlText) 
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
                        Id = "Reference-7e6f3481-4acc-47de-90fd-67878ad15e8e",
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
                                XPath = "not(ancestor-or-self::ds:Signature)"
                            }
                        },
                        DigestMethod = new SignatureSignedInfoReferenceDigestMethod()
                        { 
                            Algorithm = "http://www.w3.org/2001/04/xmlenc#sha512"
                        },
                        DigestValue = GetDigestValue(xmlText)
                    },
                    // SignedProperties
                    new SignatureSignedInfoReference()
                    { 
                        URI = "#Signature-63c35f38-2b5f-4600-b3da-3ddee86d62b3-SignedProperties",
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
                        URI = "#Signature-63c35f38-2b5f-4600-b3da-3ddee86d62b3-KeyInfo",
                        Type = "http://uri.etsi.org/01903#SignedProperties",
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

        public SignatureKeyInfo GetKeyInfo(X509Certificate2 certificate) 
        {

            var certificateModulus = Convert.ToBase64String(((RSACryptoServiceProvider)certificate.PublicKey.Key).ExportParameters(false).Modulus);
            var certificateExponent = Convert.ToBase64String(((RSACryptoServiceProvider)certificate.PublicKey.Key).ExportParameters(false).Exponent);

            var result = new SignatureKeyInfo()
            {
                Id = "#Signature-63c35f38-2b5f-4600-b3da-3ddee86d62b3-KeyInfo",
                X509Data= new SignatureKeyInfoX509Data() 
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


        public QualifyingPropertiesSignedProperties GetSignedProperties() 
        {
            
            var result = new QualifyingPropertiesSignedProperties() 
            { 
                Id = "Signature-63c35f38-2b5f-4600-b3da-3ddee86d62b3-SignedProperties",
                SignedSignatureProperties = new QualifyingPropertiesSignedPropertiesSignedSignatureProperties() 
                { 
                    SigningTime = "2020-10-01T16:49:58+02:00", // MODIFICAR !!!!!!!!!!!!!!!!!!!!!!
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
                                DigestValue = "+iJollIf11D+A9/mAzOUM6SSJvFPOneoOn7NIKf+NqkpcE7VUMx4xiGjw0D8JNrfrexJwxmlMTWd3Eg/d3Bq2Q=="
                            },
                            IssuerSerial = new QualifyingPropertiesSignedPropertiesSignedSignaturePropertiesSigningCertificateCertIssuerSerial() 
                            { 
                                X509IssuerName = "CN=CA AAPP Vascas (2) - DESARROLLO, OU=AZZ Ziurtagiri publikoa - Certificado publico SCA, O=IZENPE S.A., C=ES", // MODIFICAR !!!!!!!!!!!!!!!!!!!!!!
                                X509SerialNumber = "56643058864757982732206463601082748842"// MODIFICAR !!!!!!!!!!!!!!!!!!!!!!
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
                                 Description = null,
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
                        ObjectReference = "#Reference-7e6f3481-4acc-47de-90fd-67878ad15e8e", // MODIFICAR !!!!!!!!!!!!!!!!!!!!!!
                        Description = null,
                        DescriptionSpecified = true,
                        ObjectIdentifier = new QualifyingPropertiesSignedPropertiesSignedDataObjectPropertiesDataObjectFormatObjectIdentifier() 
                        { 
                            Identifier = new QualifyingPropertiesSignedPropertiesSignedDataObjectPropertiesDataObjectFormatObjectIdentifierIdentifier() 
                            { 
                                Qualifier = "OIDAsURN",
                                Value = "urn:oid:1.2.840.10003.5.109.10"
                            },
                            Description = null,
                            DescriptionSpecified = true,
                        },
                        MimeType = "text/xml",
                        Encoding = null,
                        EncodingSpecified = true
                     }
                }
            };

            return result;

        }

        public Signature GetSignature(string xmlText, X509Certificate2 certificate) 
        {

            var result = new Signature()
            {
                Id = "Signature-63c35f38-2b5f-4600-b3da-3ddee86d62b3-Signature",
                SignedInfo = GetSignedInfo(xmlText),
                SignatureValue = new SignatureSignatureValue() 
                { 
                    Id = "Signature-63c35f38-2b5f-4600-b3da-3ddee86d62b3-SignatureValue",
                    Value = null
                },
                KeyInfo = GetKeyInfo(certificate),
                Object = new SignatureObject()
                {
                    QualifyingProperties = new QualifyingProperties() 
                    { 
                        Id = "Signature-63c35f38-2b5f-4600-b3da-3ddee86d62b3-QualifyingProperties",
                        Target = "#Signature-63c35f38-2b5f-4600-b3da-3ddee86d62b3-Signature",
                        SignedProperties = GetSignedProperties()
                    }
                }
            };

            return result;

        }

        public void Load(string xml) 
        {
            _XmlLoadedSource = xml;
            _XmlLoadedCanonical = GetCanonical(_XmlLoadedSource);
        }

        


    }
}
