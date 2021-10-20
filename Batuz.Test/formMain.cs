using Batuz.Envios;
using Batuz.Envios.Json;
using Batuz.Lroe;
using Batuz.Negocio;
using Batuz.Negocio.Configuracion;
using Batuz.Negocio.Documento;
using Batuz.Test.Properties;
using Batuz.TicketBai.Pdf;
using Batuz.TicketBai.Xades.Hash;
using Batuz.TicketBai.Xades.Xml;
using Batuz.TicketBai.Xades.Xml.Canonicalization;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Batuz.Test
{
    public partial class formMain : Form
    {
        public formMain()
        {
            InitializeComponent();
        }

        private void btCrearPdfTicketBai_Click(object sender, EventArgs e)
        {
            CrearFacturaPdfTicketBai();
        }

        private void btCrearTicketBai_Click(object sender, EventArgs e)
        {
            CrearTicketBai();
        }

        private void btCrearTicketBaiFirmado_Click(object sender, EventArgs e)
        {
            CrearTicketBaiFirmado();
        }

        private void btValidar_Click(object sender, EventArgs e)
        {


            var xmlPath = $"{Parametros.Actual.ParametrosAlmacen.RutaArchivosTemporales}TicketBai.Firmado.xml";
            var txtPath = $"{Parametros.Actual.ParametrosAlmacen.RutaArchivosTemporales}TicketBai.Firmado.txt";

            if (File.Exists(xmlPath))
            {

                var xmlTicketBai = File.ReadAllText(xmlPath);

                var signer = new TicketBai.Xades.Signer.TicketBaiSigner(new CanonicalizationMethodDsigC14N(),
                    new DigestMethodSHA512(), new SHA256Managed());


                signer.Load(xmlTicketBai);

                var valid = signer.Validate();                

                File.WriteAllText(txtPath, signer.GetValidateInfo());

            }
            else 
            {
                File.WriteAllText(txtPath, "No existe xml que validar.");
            }

            wBr.Navigate(txtPath);

        }

        /// <summary>
        /// Crea un documento de ejemplo.
        /// </summary>
        /// <returns>Documento de ejemplo</returns>
        private Documento CrearDocumentoEjemplo()
        {
            var documento = new Documento()
            {
                DocumentoTipo = DocumentoTipo.Factura,
                SerieFactura = "2021",
                NumFactura = "0000034",
                Moneda = "EUR",
                FechaExpedicionFactura = DateTime.Now,
                DescripcionFactura = "MANTENIMIENTO SISTEMAS",
                Emisor = new DocumentoSujeto()
                {
                    Nombre = "MANUEL DIAGO GARCIA",
                    IdentficadorFiscal = "19006851L",
                    Pais = "ES",
                    CorreoElectronico = "info@wefinz.com",
                    Telefono = "964679395",
                    Domicilio = new DocumentoDomicilio()
                    {
                        Direccion = "AV CAMI DONDA 25",
                        CodigoPostal = "12530",
                        Provincia = "CASTELLON",
                        Municipio = "BURRIANA"
                    }
                },
                Destinatario = new DocumentoSujeto()
                {
                    Nombre = "MAC ORGANIZACION SL",
                    IdentficadorFiscal = "B12756474",
                    Pais = "ES",
                    CorreoElectronico = "mac@arteroconsultores.com",
                    Telefono = "964256545",
                    Domicilio = new DocumentoDomicilio()
                    {
                        Direccion = "CL POETA GUIMERA, 7 2ºA",
                        CodigoPostal = "12001",
                        Provincia = "CASTELLON",
                        Municipio = "CASTELLON"
                    }
                },
                DocumentoLineas = new List<DocumentoLinea>()
                {
                    {
                        new DocumentoLinea()
                        {
                            ProductoIdentificador = "P00001",
                            ProductoDescripcion = "MANTENIMIENTO SISTEMAS",
                            Cantidad = 1,
                            Precio = 183.25m,
                            TotalSinImpuestos = 183.25m,
                            IdentificadorImpuestos = IdentificadorImpuestos.IRAC2100,
                            TipoImpuestos = 10m,
                            CuotaImpuestos = 18.33m
                        }
                    },
                    {
                        new DocumentoLinea()
                        {
                            ProductoIdentificador = "P00002",
                            ProductoDescripcion = "SOFTWARE GESTIÓN DOCUMENTAL",
                            Cantidad = 1,
                            Precio = 183.25m,
                            TotalSinImpuestos = 2135.18m,
                            IdentificadorImpuestos = IdentificadorImpuestos.IRAC2100,
                            TipoImpuestos = 21m,
                            CuotaImpuestos = 448.38M
                        }
                    }
                }

            };

            documento.CalcularImpuestos();

            return documento;

        }

        /// <summary>
        /// Ejemplo de creación de un pdf de factura TicketBai.
        /// </summary>
        private void CrearFacturaPdfTicketBai()
        {

            // Creamos y preparamos un documento de ejemplo
            Documento documento = CrearDocumentoEjemplo();

            // Creo un objeto TicketBai para obtener el código indentificativo y el CRC8
            var ticketBai = TicketBaiFactory.GetTicketBai(documento);


            // Firmo el ticketBai
            var xmlParser = new XmlParser();

            var signer = new TicketBai.Xades.Signer.TicketBaiSigner(new CanonicalizationMethodDsigC14N(),
                new DigestMethodSHA512(), new SHA256Managed());

            var xml = xmlParser.GetString(ticketBai, new Dictionary<string, string>()
            {
                    { "T",          "urn:ticketbai:emision"},
            });

            signer.Load(xml);

            var certificado = CargaCertificado();

            signer.Sign(certificado);

            // obtener el código indentificativo y el CRC8
            documento.CodigoIdentificativo = $"{signer.TicketBaiSigned.CodigoIdentificativo}";
            documento.CodigoDetecionErrores = signer.TicketBaiSigned.CodigoIdentificativo.ControlCRC8;

            // Texto html de plantilla de factura
            var plantillaFacturaHtml = Resources.factura;            
            RenderizadorHtml renderizadorHtml = new RenderizadorHtml(documento, 
                plantillaFacturaHtml);
            // Texto html completado con los datos del documento
            var facturaHtml = renderizadorHtml.Renderiza();

            // Mediante el texto html obtenemos el pdf de factura
            var pdfManager = new PdfManager();
            var facturaPdf = pdfManager.GetPdfFormHtml(facturaHtml, "", 
                (byte[])Resources.seguiemj);

            // Guardamos el pdf
            var pdfPath = $"{Parametros.Actual.ParametrosAlmacen.RutaArchivosTemporales}TicketBai.pdf";
            File.WriteAllBytes(pdfPath, facturaPdf);

            wBr.Navigate(pdfPath);

        }

        /// <summary>
        /// Ejemplo de creación de un archivo TicketBai.
        /// </summary>
        private void CrearTicketBai()
        {

            Documento doc = CrearDocumentoEjemplo();
            var ticketBai = TicketBaiFactory.GetTicketBai(doc);
            var xmlParser = new XmlParser();

            var xmlPath = $"{Parametros.Actual.ParametrosAlmacen.RutaArchivosTemporales}TicketBai.xml";
            File.WriteAllText(xmlPath, xmlParser.GetString(ticketBai, Namespaces.Items));

            wBr.Navigate(xmlPath);

        }

        /// <summary>
        /// Carga un certificado para la firma.
        /// </summary>
        /// <returns>Certificado para la firma.</returns>
        private X509Certificate2 CargaCertificado()
        {

            var store = new X509Store(StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);
            return store.Certificates[19];

        }

        /// <summary>
        /// Crea un TicketBai firmado.
        /// </summary>
        private void CrearTicketBaiFirmado()
        {

            Documento doc = CrearDocumentoEjemplo();
            var ticketBai = TicketBaiFactory.GetTicketBai(doc);
            var xmlParser = new XmlParser();

            var signer = new TicketBai.Xades.Signer.TicketBaiSigner(new CanonicalizationMethodDsigC14N(),
                new DigestMethodSHA512(), new SHA256Managed());

            var xml = xmlParser.GetString(ticketBai, new Dictionary<string, string>()
            {
                    { "T",          "urn:ticketbai:emision"},
            });

            signer.Load(xml);

            var certificado = CargaCertificado();

            signer.Sign(certificado);

            var xmlPath = $"{Parametros.Actual.ParametrosAlmacen.RutaArchivosTemporales}TicketBai.Firmado.xml";

            File.WriteAllText(xmlPath, signer.XmlSigned);

            wBr.Navigate(xmlPath);

        }

        private void btSend_Click(object sender, EventArgs e)
        {

            Documento doc = CrearDocumentoEjemplo();
            var ticketBai = TicketBaiFactory.GetTicketBai(doc);
            var xmlParser = new XmlParser();

            var signer = new TicketBai.Xades.Signer.TicketBaiSigner(new CanonicalizationMethodDsigC14N(),
                new DigestMethodSHA512(), new SHA256Managed());

            var xml = xmlParser.GetString(ticketBai, new Dictionary<string, string>()
            {
                    { "T",          "urn:ticketbai:emision"},
            });

            signer.Load(xml);

            var certificado = CargaCertificado();

            signer.Sign(certificado);

            var peticion = new PeticionHttp(Url.UrlProduccionEnvio);

            var tBaiSigned = $"<?xml version=\"1.0\" encoding=\"UTF-8\"?>{signer.XmlSigned}";

            File.WriteAllText(@"C:\Users\usuario\Downloads\TEST ALTA\TBAI.xml", tBaiSigned);

            LROEPF140IngresosConFacturaConSGAltaPeticion alta = new LROEPF140IngresosConFacturaConSGAltaPeticion() 
            { 
                Cabecera = new Cabecera() 
                { 
                    Modelo = "140",
                    Capitulo = "1",
                    Subcapitulo = "1.1",
                    Operacion = "A00",
                    Version = "1.0",
                    Ejercicio = "2021",
                    ObligadoTributario = new ObligadoTributario() 
                    { 
                        NIF = "19006851L",
                        ApellidosNombreRazonSocial = "DIAGO GARCIA MANUEL"
                    }
                },
                Ingresos = new List<Ingreso>() 
                { 
                    new Ingreso()
                    { 
                        TicketBai = Encoding.UTF8.GetBytes(tBaiSigned),
                        Renta = new List<DetalleRenta>()
                        {
                            new DetalleRenta()
                            {
                                Epigrafe = "1845"
                            }
                        }
                    }
                }
            };

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("lrpficfcsgap", "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/LROE_PF_140_1_1_Ingresos_ConfacturaConSG_AltaPeticion_V1_0_2.xsd");

            XmlSerializer serializer = new XmlSerializer(alta.GetType());

            //LROEPF140IngresosConFacturaConSGAltaPeticion ejemplo = null;

            //using (Stream reader = new FileStream(@"C:\Users\usuario\Downloads\TEST ALTA\EJEMPLO.xml", FileMode.Open))
            //    ejemplo = (LROEPF140IngresosConFacturaConSGAltaPeticion)serializer.Deserialize(reader);

            //File.WriteAllText(@"C:\Users\usuario\Downloads\TEST ALTA\EJEMPLO_TBAI.xml", Encoding.UTF8.GetString(ejemplo.Ingresos[0].TicketBai));

            // Create an XmlTextWriter using a FileStream.
            MemoryStream ms = new MemoryStream();
            XmlWriter writer = new XmlTextWriter(ms, Encoding.UTF8);
            // Serialize using the XmlTextWriter.
            serializer.Serialize(writer, alta, ns);
            writer.Close();

            var bytesPeticion = ms.ToArray();

            //var ticketBaiFirmado = $"<?xml version=\"1.0\" encoding=\"UTF-8\"?>{signer.XmlSigned}";
            //var ticketBaiFirmado = File.ReadAllText(@"C:\Users\usuario\Downloads\Ejemplos\Ejemplo_TicketBAI_79732487C_A2022_0399.xml");
            //var bytesPeticion = peticion.Encoding.GetBytes(ticketBaiFirmado);

            //bytesPeticion = Encoding.UTF8.GetBytes(File.ReadAllText(@"C:\Users\usuario\Downloads\Ejemplos\Ejemplo_1_LROE_PF_140_IngresosConFacturaConSG_79732487C.xml"));


            byte[] bytesPeticionComprimida;

            using (var outStream = new MemoryStream())
            {
                using (var tinyStream = new GZipStream(outStream, CompressionMode.Compress))
                using (var mStream = new MemoryStream(bytesPeticion))
                    mStream.CopyTo(tinyStream);

                bytesPeticionComprimida = outStream.ToArray();
            }




            var dat = new data("1.1", new inte() {nif="19006851L", nrs="MANUEL", ap1="DIAGO", ap2="GARCIA" }, "140", "2021");
            var json = $"{dat}";

            peticion.Peticion.Headers["eus-bizkaia-n3-data"] = json;
            peticion.Peticion.ContentLength = bytesPeticionComprimida.Length;

            peticion.Peticion.ClientCertificates.Add(certificado);

            using (Stream stream = peticion.Peticion.GetRequestStream())
                stream.Write(bytesPeticionComprimida, 0, bytesPeticionComprimida.Length);

            HttpWebResponse response = (HttpWebResponse)peticion.Peticion.GetResponse();

            string statusDescription = response.StatusDescription;

            Stream dataStream = response.GetResponseStream();

            string responseFromServer;

            using (StreamReader reader = new StreamReader(dataStream))
            {
                responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
            }


        }
    }
}
