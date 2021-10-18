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
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

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

                var signer = new Batuz.TicketBai.Xades.Signer.TicketBaiSigner(new CanonicalizationMethodDsigC14N(),
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
            return new Documento()
            {
                DocumentoTipo = DocumentoTipo.Factura,
                SerieFactura = "2021",
                NumFactura = "0000034",
                Moneda = "EUR",
                FechaExpedicionFactura = DateTime.Now,
                Emisor = new DocumentoSujeto()
                {
                    Nombre = "WEFINZ SOLUTIONS SL",
                    IdentficadorFiscal = "B44531218",
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
        }

        /// <summary>
        /// Ejemplo de creación de un pdf de factura TicketBai.
        /// </summary>
        private void CrearFacturaPdfTicketBai()
        {

            // Creamos y preparamos un documento de ejemplo
            Documento documento = CrearDocumentoEjemplo();
            documento.CalcularImpuestos();

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
            return store.Certificates[23];

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


    }
}
