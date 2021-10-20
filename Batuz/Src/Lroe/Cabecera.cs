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

using System;
using System.Xml.Serialization;

namespace Batuz.Lroe
{

    /// <summary>
    /// Datos del cabecera del alta ingresos con facturas emitidas con software garante.
    /// </summary>
    [Serializable()]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class Cabecera
    {

        #region Propiedades Públicas de Instancia

        /// <summary>
        /// Modelo de la declaración. Deberá informarse con valor “140” (Libro
        /// Registro de Operaciones Económicas, personas físicas).
        /// Alfanumérico (3).
        /// </summary>
        public string Modelo { get; set; }

        /// <summary>
        /// Capítulo del Libro Registro de Operaciones Económicas. 
        /// Deberá informarse con valor “1” (Ingresos y facturas emitidas).
        /// Alfanumérico (2).
        /// </summary>
        public string Capitulo { get; set; }

        /// <summary>
        /// Subcapítulo del Libro Registro de Operaciones Económicas. Deberá 
        /// informarse con valor “1.1” (Ingresos con facturas emitidas con software
        /// garante).
        /// Alfanumérico (3).
        /// </summary>
        public string Subcapitulo { get; set; }

        /// <summary>
        /// Tipo de operación. Deberá informarse con valor “A00” (Alta).
        /// Alfanumérico (3).
        /// </summary>
        public string Operacion { get; set; }

        /// <summary>
        /// Identificación de la versión del esquema utilizado.
        /// Alfanumérico (5) LC0.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Ejercicio. Numérico (4).
        /// </summary>
        public string Ejercicio { get; set; }

        /// <summary>
        /// Obligado tributario.
        /// </summary>
        public ObligadoTributario ObligadoTributario { get; set; }

        #endregion

    }
}
