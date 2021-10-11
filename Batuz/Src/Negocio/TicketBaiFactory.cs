﻿/*
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

using Batuz.Info;
using Batuz.TicketBai;
using System.Collections.Generic;

namespace Batuz.Negocio
{

    /// <summary>
    /// Gestiona la creación de instancias de TicketBai.
    /// </summary>
    public class TicketBaiFactory
    {

        /// <summary>
        /// Devuelve una instancia de TicketBai
        /// a partir de un documento.
        /// </summary>
        /// <param name="documento">Factura o justificante del cual se
        /// va a generar el TicketBai.</param>
        /// <returns>TicketBai que representa la factura o jusficante facilitados
        /// como argumento.</returns>
        public static TicketBai.TicketBai GetTicketBai(Documento.Documento documento) 
        {

            documento.CalcularImpuestos();

            TicketBai.TicketBai result = new TicketBai.TicketBai()
            {
                Cabecera = new Cabecera() 
                { 
                    IDVersionTBAI = TicketBai.Listas.IDVersionTBAI.Version_1_2
                },
                Sujetos = new Sujetos() 
                { 
                    Emisor = new SujetosEmisor() 
                    { 
                        NIF = documento.Emisor.IdentficadorFiscal,
                        ApellidosNombreRazonSocial = documento.Emisor.Nombre
                    },
                    Destinatarios = new List<SujetosDestinatarios>() 
                    {
                        { 
                            new SujetosDestinatarios()
                            { 
                                IDDestinatario = new SujetosDestinatariosIDDestinatario()
                                { 
                                    NIF = documento.Destinatario.IdentficadorFiscal,
                                    ApellidosNombreRazonSocial = documento.Destinatario.Nombre
                                }
                            }
                        }
                    },
                },
                Factura = new Factura() 
                { 
                    CabeceraFactura = new FacturaCabeceraFactura() 
                    { 
                        SerieFactura = documento.SerieFactura,
                        NumFactura = documento.NumFactura,
                        FechaExpedicionFactura = $"{documento.FechaExpedicionFactura:dd-MM-yyyy}",
                        HoraExpedicionFactura = $"{documento.FechaExpedicionFactura:HH:mm:ss}",
                    },
                    DatosFactura = new FacturaDatosFactura() 
                    { 
                        DescripcionFactura = documento.DescripcionFactura,
                        ImporteTotalFactura = documento.TotalFactura,
                        Claves = new FacturaDatosFacturaClaves() 
                        { 
                            IDClave = new FacturaDatosFacturaClavesIDClave[1] 
                            { 
                                new FacturaDatosFacturaClavesIDClave()
                                { 
                                     ClaveRegimenIvaOpTrascendencia = TicketBai.Listas.ClaveRegimenIvaOpTrascendencia.RegimenGeneral
                                }
                            }
                        }
                    },
                    TipoDesglose = new FacturaTipoDesglose() 
                    {
                        DesgloseFactura = new Desglose() 
                        { 
                             Sujeta = new DesgloseSujeta() 
                             {                                 
                             }
                        }
                    }
                },
                HuellaTBAI = new HuellaTBAI() 
                { 
                     EncadenamientoFacturaAnterior = new HuellaTBAIEncadenamientoFacturaAnterior() 
                     { 
                         SerieFacturaAnterior = null,
                         NumFacturaAnterior = null,
                         FechaExpedicionFacturaAnterior = null,
                         SignatureValueFirmaFacturaAnterior = null
                     },
                     Software = new HuellaTBAISoftware() 
                     { 
                        LicenciaTBAI = "TBAIPRUEBA",
                        EntidadDesarrolladora = new Sujeto() 
                        { 
                            NIF = VerificacionPresencial.EmpresaDesarrolladoraNif
                        },
                        Nombre  = VerificacionPresencial.EmpresaDesarrolladoraNombre,
                        Version = VerificacionPresencial.SoftwareGaranteVersion
                     }
                }
            };

            if (documento.CuotaImpuestosRetenidos != 0)
                result.Factura.DatosFactura.RetencionSoportada = documento.CuotaImpuestosRetenidos;

            foreach (var iva in documento.DocumentoImpuestosSoportados) 
            {
                if (iva.TipoImpuestos == 0) 
                {                    

                    if (result.Factura.TipoDesglose.DesgloseFactura.Sujeta.Exenta == null)
                        result.Factura.TipoDesglose.DesgloseFactura.Sujeta.Exenta = new DesgloseSujetaExenta();

                    var exenta = result.Factura.TipoDesglose.DesgloseFactura.Sujeta.Exenta;

                    exenta.BaseImponible = iva.BaseImpuestos;
                    exenta.CausaExencion = TicketBai.Listas.CausaExencion.Articulo20NormaForalIva;

                } 
                else 
                {
                    
                    if (result.Factura.TipoDesglose.DesgloseFactura.Sujeta.NoExenta == null)
                        result.Factura.TipoDesglose.DesgloseFactura.Sujeta.NoExenta = new DesgloseSujetaNoExenta() {
                            DetalleNoExenta = new DesgloseSujetaNoExentaDetalleNoExenta()
                            {
                                TipoNoExenta = TicketBai.Listas.TipoNoExenta.SinInversionSujetoPasivo,
                                DesgloseIVA = new List<DesgloseSujetaNoExentaDetalleNoExentaDetalleIVA>()
                            }
                };

                    var desgloseIVA = result.Factura.TipoDesglose.DesgloseFactura.Sujeta.NoExenta.DetalleNoExenta.DesgloseIVA;

                    desgloseIVA.Add(new DesgloseSujetaNoExentaDetalleNoExentaDetalleIVA() {
                        BaseImponible = iva.BaseImpuestos,
                        TipoImpositivo = iva.TipoImpuestos,
                        CuotaImpuesto = iva.CuotaImpuestos
                    });

                }
            }

            return result;

        }

    }
}
