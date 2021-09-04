using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Batuz.TicketBai;
using Batuz.Negocio.Documento;

namespace Batuz.Negocio
{

    /// <summary>
    /// Gestiona la creación de instancias de TicketBai,
    /// </summary>
    public class TicketBaiFactory
    {

        /// <summary>
        /// Devuelve una instancia de TicketBai
        /// a partir de un documento.
        /// </summary>
        /// <param name="documento"></param>
        /// <returns></returns>
        public static TicketBai.TicketBai GetTicketBai(Documento.Documento documento) 
        {

            var result = new TicketBai.TicketBai()
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
                
                }
            };

            return null;
        }

    }
}
