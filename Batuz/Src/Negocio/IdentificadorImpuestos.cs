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

namespace Batuz.Negocio
{

    /// <summary>
    /// Códigos de tipos de impuesto.
    /// </summary>
    public enum IdentificadorImpuestos
    {

        /// <summary>
        /// SIN ASIGNAR NINGÚN VALOR.
        /// </summary>
        SINVALOR,

        /// <summary>
        /// IVA REPERCUTIDO REGIMEN GENERAL 4%
        /// </summary>
        IRAC0400,
        
        /// <summary>
        /// IVA REPERCUTIDO REGIMEN GENERAL 10%
        /// </summary>
        IRAC1000,

        /// <summary>
        /// IVA REPERCUTIDO REGIMEN GENERAL 21%
        /// </summary>
        IRAC2100,

        /// <summary>
        /// IVA REPERCUTIDO RECARGO DE EQUIVALENCIA 0,50%
        /// </summary>
        IRAD0050,

        /// <summary>
        /// IVA REPERCUTIDO RECARGO DE EQUIVALENCIA 1,40%
        /// </summary>
        IRAD0140,

        /// <summary>
        /// IVA REPERCUTIDO RECARGO DE EQUIVALENCIA 1,75%
        /// </summary>
        IRAD0175,

        /// <summary>
        /// IVA REPERCUTIDO RECARGO DE EQUIVALENCIA 5,20%
        /// </summary>
        IRAD0520,

        /// <summary>
        /// IVA REPERCUTIDO ADQUISICIONES INTRACOMUNITARIAS DE BIENES 4%
        /// </summary>
        IRAE0400,

        /// <summary>
        /// IVA REPERCUTIDO ADQUISICIONES INTRACOMUNITARIAS DE BIENES 10%
        /// </summary>
        IRAE1000,

        /// <summary>
        /// IVA REPERCUTIDO ADQUISICIONES INTRACOMUNITARIAS DE BIENES 21%
        /// </summary>
        IRAE2100,

        /// <summary>
        /// IVA REPERCUTIDO ADQUISICIONES INTRACOMUNITARIAS DE SERVICIOS 4%
        /// </summary>
        IRAF0400,

        /// <summary>
        /// IVA REPERCUTIDO ADQUISICIONES INTRACOMUNITARIAS DE SERVICIOS 10%
        /// </summary>
        IRAF1000,

        /// <summary>
        /// IVA REPERCUTIDO ADQUISICIONES INTRACOMUNITARIAS DE SERVICIOS 21%
        /// </summary>
        IRAF2100,

        /// <summary>
        /// IVA REPERCUTIDO ADQUISICIONES INTRACOMUNITARIAS INVERSION 4%
        /// </summary>
        IRAG0400,

        /// <summary>
        /// IVA REPERCUTIDO ADQUISICIONES INTRACOMUNITARIAS INVERSION 10%
        /// </summary>
        IRAG1000,

        /// <summary>
        /// IVA REPERCUTIDO ADQUISICIONES INTRACOMUNITARIAS INVERSION 21%
        /// </summary>
        IRAG2100,

        /// <summary>
        /// IVA REPERCUTIDO OSS E IOSS AUSTRIA 20 %
        /// </summary>
        IRAHAT20,

        /// <summary>
        /// IVA REPERCUTIDO OSS E IOSS BELGICA 21 %
        /// </summary>
        IRAHBE21,

        /// <summary>
        /// IVA REPERCUTIDO OSS E IOSS BULGARIA 20 %
        /// </summary>
        IRAHBG20,

        /// <summary>
        /// IVA REPERCUTIDO OSS E IOSS CHIPRE 19 %
        /// </summary>
        IRAHCY19,

        /// <summary>
        /// IVA REPERCUTIDO OSS E IOSS REP.CHECA 21 %
        /// </summary>
        IRAHCZ21,

        /// <summary>
        /// IVA REPERCUTIDO OSS E IOSS ALEMANIA 19 %
        /// </summary>
        IRAHDE19,

        /// <summary>
        /// IVA REPERCUTIDO OSS E IOSS DINAMARCA 25 %
        /// </summary>
        IRAHDK25,

        /// <summary>
        /// IVA REPERCUTIDO OSS E IOSS ESTONIA 20 %
        /// </summary>
        IRAHEE20,

        /// <summary>
        /// IVA REPERCUTIDO OSS E IOSS FINLANDIA 24 %
        /// </summary>
        IRAHFI24,

        /// <summary>
        /// IVA REPERCUTIDO OSS E IOSS FRANCIA 20 %
        /// </summary>
        IRAHFR20,

        /// <summary>
        /// IVA REPERCUTIDO OSS E IOSS GRECIA 24 %
        /// </summary>
        IRAHGR24,

        /// <summary>
        /// IVA REPERCUTIDO OSS E IOSS CROACIA 25 %
        /// </summary>
        IRAHHR25,

        /// <summary>
        /// IVA REPERCUTIDO OSS E IOSS HUNGRIA 27 %
        /// </summary>
        IRAHHU27,

        /// <summary>
        /// IVA REPERCUTIDO OSS E IOSS IRLANDA 23 %
        /// </summary>
        IRAHIE23,

        /// <summary>
        /// IVA REPERCUTIDO OSS E IOSS ITALIA 22 %
        /// </summary>
        IRAHIT22,

        /// <summary>
        /// IVA REPERCUTIDO OSS E IOSS LITUANIA 21 %
        /// </summary>
        IRAHLT21,

        /// <summary>
        /// IVA REPERCUTIDO OSS E IOSS LUXEMBURGO 17 %
        /// </summary>
        IRAHLU17,

        /// <summary>
        /// IVA REPERCUTIDO OSS E IOSS LETONIA 21 %
        /// </summary>
        IRAHLV21,

        /// <summary>
        /// IVA REPERCUTIDO OSS E IOSS MALTA 18 %
        /// </summary>
        IRAHMT18,

        /// <summary>
        /// IVA REPERCUTIDO OSS E IOSS PAISES BAJOS 21 %
        /// </summary>
        IRAHNL21,

        /// <summary>
        /// IVA REPERCUTIDO OSS E IOSS POLONIA 23 %
        /// </summary>
        IRAHPL23,

        /// <summary>
        /// IVA REPERCUTIDO OSS E IOSS PORTUGAL 23 %
        /// </summary>
        IRAHPT23,

        /// <summary>
        /// IVA REPERCUTIDO OSS E IOSS RUMANIA 19 %
        /// </summary>
        IRAHRO19,

        /// <summary>
        /// IVA REPERCUTIDO OSS E IOSS SUECIA 25 %
        /// </summary>
        IRAHSE25,

        /// <summary>
        /// IVA REPERCUTIDO OSS E IOSS ESLOVENIA 22 %
        /// </summary>
        IRAHSI22,

        /// <summary>
        /// IVA REPERCUTIDO OSS E IOSS REP.ESLOVACA 20 %
        /// </summary>
        IRAHSK20,

        /// <summary>
        /// IVA REPERCUTIDO REGIMEN ESPECIAL DE CRITERIO DE CAJA 4%
        /// </summary>
        IRAK0400,

        /// <summary>
        /// IVA REPERCUTIDO REGIMEN ESPECIAL DE CRITERIO DE CAJA 10%
        /// </summary>
        IRAK1000,

        /// <summary>
        /// IVA REPERCUTIDO REGIMEN ESPECIAL DE CRITERIO DE CAJA 21%
        /// </summary>
        IRAK2100,

        /// <summary>
        /// IVA REPERCUTIDO REGIMEN ESPECIAL DE OBJETOS USADOS 4%
        /// </summary>
        IRAL0400,

        /// <summary>
        /// IVA REPERCUTIDO REGIMEN ESPECIAL DE OBJETOS USADOS 10%
        /// </summary>
        IRAL1000,

        /// <summary>
        /// IVA REPERCUTIDO REGIMEN ESPECIAL DE OBJETOS USADOS 21%
        /// </summary>
        IRAL2100,

        /// <summary>
        /// IVA REPERCUTIDO REGIMEN ESPECIAL AGENCIAS DE VIAJES 21%
        /// </summary>
        IRAO2100,

        /// <summary>
        /// IVA REPERCUTIDO INVERSION SUJETO PASIVO 4%
        /// </summary>
        IRAP0400,

        /// <summary>
        /// IVA REPERCUTIDO INVERSION SUJETO PASIVO 10%
        /// </summary>
        IRAP1000,

        /// <summary>
        /// IVA REPERCUTIDO INVERSION SUJETO PASIVO 21%
        /// </summary>
        IRAP2100,

        /// <summary>
        /// IVA REPERCUTIDO REGIMEN ESPECIAL AGRICULTURA, GANADERIA Y PESCA
        /// </summary>
        IRAQ0000,

        /// <summary>
        /// IVA REPERCUTIDO EXENCIONES EN OPERACIONES INTERIORES (ART. 20/E1)
        /// </summary>
        IRAS0000,

        /// <summary>
        /// IVA REPERCUTIDO EXPORTACIONES (ART. 21/E2)
        /// </summary>
        IRAT0000,

        /// <summary>
        /// IVA REPERCUTIDO OPERAC. ASIMILADAS A LAS EXPORTACIONES (ART. 22/E3)
        /// </summary>
        IRAU0000,

        /// <summary>
        /// IIVA REPERCUTIDO EXENCIONES REG. ADUANEROS Y FISCALES (ART. 24/E4)
        /// </summary>
        IRAV0000,

        /// <summary>
        /// IVA REPERCUTIDO ENTREGAS INTRACOMUNITARIAS (ART. 25/E5) ENTREGAS
        /// </summary>
        IRAW0000,

        /// <summary>
        /// IVA REPERCUTIDO ENTREGAS INTRACOMUNITARIAS (ART. 25/E5) SERVICIOS
        /// </summary>
        IRAW00A0,

        /// <summary>
        /// IVA REPERCUTIDO EXENCIONES OTROS
        /// </summary>
        IRAX0000,

        /// <summary>
        /// IVA REPERCUTIDO OPERACIONES INTRAGRUPO 4%
        /// </summary>
        IRGD0400,

        /// <summary>
        /// IVA REPERCUTIDO OPERACIONES INTRAGRUPO 10%
        /// </summary>
        IRGD1000,

        /// <summary>
        /// IVA REPERCUTIDO OPERACIONES INTRAGRUPO 21%
        /// </summary>
        IRGD2100,

        /// <summary>
        /// RETENCIONES SOPORTADAS TRANSPORTES/MUDANZAS EO 1%
        /// </summary>
        RSAF0100,

        /// <summary>
        /// RETENCIONES SOPORTADAS ACTIVIDADES PROFESIONALES 7%
        /// </summary>
        RSAF0700,

        /// <summary>
        /// RETENCIONES SOPORTADAS ACTIVIDADES PROFESIONALES 15%
        /// </summary>
        RSAF1500,

        /// <summary>
        /// RETENCIONES SOPORTADAS ARRENDAMIENTOS INM. URBANOS 19%
        /// </summary>
        RSAJ1900,

        /// <summary>
        /// RETENCIONES SOPORTADAS CAPITAL MOBILIARIO 19%
        /// </summary>
        RSAO1900

    }

}
