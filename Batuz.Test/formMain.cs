using Batuz.TicketBai;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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

        private void formMain_Load(object sender, EventArgs e)
        {


            // Factura de ejemplo
            var ticketBai = new TicketBai.TicketBai() 
            {
                Cabecera = new Cabecera() 
                { 
                    IDVersionTBAI = "1.2"
                },
                Sujetos = new Sujetos() 
                { 
                    Emisor = new SujetosEmisor() 
                    { 
                        NIF = "B00000034",
                        ApellidosNombreRazonSocial = "HOTEL ADIBIDEZ"
                    },
                    Destinatarios = new List<SujetosDestinatarios>() 
                    { 
                        new SujetosDestinatarios()
                        { 
                            IDDestinatario = new SujetosDestinatariosIDDestinatario()
                            { 
                                NIF = "B26248146",
                                ApellidosNombreRazonSocial = "EMPRESA LANTEGIA"
                            }
                        }
                    }
                },
                Factura = new Factura() 
                { 
                    CabeceraFactura = new FacturaCabeceraFactura() 
                    { 
                        SerieFactura = "B2022",
                        NumFactura = "0100",
                        FechaExpedicionFactura = "30-01-2022",
                        HoraExpedicionFactura = "18:00:17"
                    },
                    DatosFactura = new FacturaDatosFactura() 
                    {
                        DescripcionFactura = "Servicios Hotel",
                        ImporteTotalFactura = 2343.00m,
                        Claves = new FacturaDatosFacturaClaves() 
                        { 
                            IDClave = new FacturaDatosFacturaClavesIDClave() 
                            { 
                                 ClaveRegimenIvaOpTrascendencia = ClaveRegimenIvaOpTrascendencia.RegimenGeneral
                            }
                        }
                    },
                    TipoDesglose = new FacturaTipoDesglose() 
                    { 
                        DesgloseFactura = new FacturaTipoDesgloseDesgloseFactura() 
                        { 
                            Sujeta = new FacturaTipoDesgloseDesgloseFacturaSujeta() 
                            { 
                                NoExenta = new FacturaTipoDesgloseDesgloseFacturaSujetaNoExenta() 
                                { 
                                    DetalleNoExenta = new FacturaTipoDesgloseDesgloseFacturaSujetaNoExentaDetalleNoExenta() 
                                    { 
                                        TipoNoExenta= TipoNoExenta.S1,
                                        DesgloseIVA = new FacturaTipoDesgloseDesgloseFacturaSujetaNoExentaDetalleNoExentaDetalleIVA[2] 
                                        {
                                            new FacturaTipoDesgloseDesgloseFacturaSujetaNoExentaDetalleNoExentaDetalleIVA()
                                            { 
                                                BaseImponible = 300.00m,
                                                TipoImpositivo = 21.00m,
                                                CuotaImpuesto = 63.00m
                                            },
                                            new FacturaTipoDesgloseDesgloseFacturaSujetaNoExentaDetalleNoExentaDetalleIVA()
                                            {
                                                BaseImponible = 1800.00m,
                                                TipoImpositivo = 10.00m,
                                                CuotaImpuesto = 180.00m
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }                    
                },
                HuellaTBAI = new HuellaTBAI() 
                {
                    EncadenamientoFacturaAnterior = new HuellaTBAIEncadenamientoFacturaAnterior()
                    {
                        SerieFacturaAnterior = "B2022",
                        NumFacturaAnterior = "0099",
                        FechaExpedicionFacturaAnterior = "29-01-2022",
                        SignatureValueFirmaFacturaAnterior = "BeMkKwXaFsxHQec65SKpVP7EU9o4nUXOx7SAftIToFsxH+2j2tXPXhpBUnS26dhdSpiMl2DlTuqRsFdZfWyYazaGHgSRQHZZAnFt",
                    },
                    Software = new HuellaTBAISoftware() 
                    {
                        LicenciaTBAI = "TBAIPRUEBA",
                        EntidadDesarrolladora = new HuellaTBAISoftwareEntidadDesarrolladora() 
                        { 
                            NIF = "A48119820"
                        },
                        Nombre = "DFBTBAI",
                        Version = "1.04.00"
                    },
                    NumSerieDispositivo = "GP4FC5J"
                }
            };

            var xmlParser = new XmlParser();

            var ns = new Dictionary<string, string>() {
            };

            var xml = xmlParser.GetCanonicalString(ticketBai, ns);

            var hash = new SHA512Managed().ComputeHash(Encoding.UTF8.GetBytes(xml));
            var b64 = Convert.ToBase64String(hash);

            var xmlPath = @"C:\Users\usuario\Downloads\TicketBAI\test.xml";

            File.WriteAllText(xmlPath, xml);

            //XmlDocument xmlDocument = new XmlDocument();

            //XmlSerializer xmlSerializer = new XmlSerializer(ticketBai.GetType());

            //using (StreamWriter sw = new StreamWriter(xmlPath, false, Encoding.GetEncoding("UTF-8")))
            //{
            //    xmlSerializer.Serialize(sw, ticketBai);
            //}

        }
    }
}
