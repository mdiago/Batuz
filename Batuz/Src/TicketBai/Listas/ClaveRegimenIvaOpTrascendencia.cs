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

using System.Xml.Serialization;

namespace Batuz.TicketBai.Listas
{

    /// <summary>
    /// Clave que identificará el tipo de régimen del IVA
    /// o una operación con trascendencia tributaria.
    /// Alfanumérico (2) L9.
    /// </summary>
    public enum ClaveRegimenIvaOpTrascendencia
    {

        /// <summary>
        /// Operación de régimen general y cualquier otro supuesto que no esté recogido en los
        /// siguientes valores.
        /// </summary>
        [XmlEnum(Name = "01")]
        RegimenGeneral,

        /// <summary>
        /// Exportación.
        /// </summary>
        [XmlEnum(Name = "02")]
        Exportacion,

        /// <summary>
        /// Operaciones a las que se aplique el régimen especial de bienes usados, objetos de arte,
        /// antigüedades y objetos de colección.
        /// </summary>
        [XmlEnum(Name = "03")]
        Rebu,

        /// <summary>
        /// Régimen especial del oro de inversión.
        /// </summary>
        [XmlEnum(Name = "04")]
        ReOroInversion,

        /// <summary>
        /// Régimen especial de las agencias de viajes.
        /// </summary>
        [XmlEnum(Name = "05")]
        ReAgenciasViaje,

        /// <summary>
        /// Régimen especial grupo de entidades en IVA (Nivel Avanzado).
        /// </summary>
        [XmlEnum(Name = "06")]
        ReGrupoEntidades,

        /// <summary>
        /// Régimen especial del criterio de caja.
        /// </summary>
        [XmlEnum(Name = "07")]
        ReCriterioCaja,

        /// <summary>
        /// Operaciones sujetas al IPSI/IGIC (Impuesto sobre la Producción, los Servicios y la
        /// Importación / Impuesto General Indirecto Canario).
        /// </summary>
        [XmlEnum(Name = "08")]
        IpsiIgic,

        /// <summary>
        /// Facturación de las prestaciones de servicios de agencias de viaje que actúan como
        /// mediadoras en nombre y por cuenta ajena (disposición adicional 3ª del Reglamento de
        /// Facturación).
        /// </summary>
        [XmlEnum(Name = "09")]
        AgenciasViajesMediadores,

        /// <summary>
        /// Cobros por cuenta de terceros o terceras de honorarios profesionales o de derechos
        /// derivados de la propiedad industrial, de autor u otros por cuenta de sus socios, socias,
        /// asociados, asociadas, colegiados o colegiadas efectuados por sociedades, asociaciones,
        /// colegios profesionales u otras entidades que realicen estas funciones de cobro.
        /// </summary>
        [XmlEnum(Name = "10")]
        DchosPropiedadIndustrial,

        /// <summary>
        /// Operaciones de arrendamiento de local de negocio sujetos a retención.
        /// </summary>
        [XmlEnum(Name = "11")]
        ArrendamientosConRetencion,

        /// <summary>
        /// Operaciones de arrendamiento de local de negocio no sujetos a retención.
        /// </summary>
        [XmlEnum(Name = "12")]
        ArrendamientosSinRetencion,


        /// <summary>
        /// Operaciones de arrendamiento de local de negocio sujetas y no sujetas a retención.
        /// </summary>
        [XmlEnum(Name = "13")]
        ArrendamientosConYSinRetencion,

        /// <summary>
        /// Factura con IVA pendiente de devengo en certificaciones de obra cuya destinataria sea una
        /// Administración Pública.
        /// </summary>
        [XmlEnum(Name = "14")]
        CertificacionesAdmonPublica,

        /// <summary>
        /// Factura con IVA pendiente de devengo en operaciones de tracto sucesivo.
        /// </summary>
        [XmlEnum(Name = "15")]
        PteDevengoTractoSucesivo,

        /// <summary>
        /// Operaciones en recargo de equivalencia.
        /// </summary>
        [XmlEnum(Name = "51")]
        ReRecargoEquivalencia,

        /// <summary>
        /// Operaciones en régimen simplificado
        /// </summary>
        [XmlEnum(Name = "52")]
        RegimenSimplificado,


        /// <summary>
        /// Operaciones realizadas por personas o entidades que no tengan la consideración de
        /// empresarios, empresarias o profesionales a efectos del IVA
        /// </summary>
        [XmlEnum(Name = "53")]
        NoEmpresariosNiProfesionales

    }
}
