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

using System.Reflection;

namespace Batuz.Info
{

    /// <summary>
    /// Información requerida por la ORDEN FORAL 1482/2020, de 9 de septiembre 
    /// en su artículo 9.
    /// </summary>
    public static class VerificacionPresencial
    {

        #region Construtores Estáticos

        /// <summary>
        /// Constructor estático.
        /// </summary>
        static VerificacionPresencial()
        {
            SoftwareGaranteVersion = $"{Assembly.GetExecutingAssembly().GetName().Version}";
        }

        #endregion

        #region Propiedades Públicas Estáticas

        /// <summary>
        /// Número de identificación fiscal 
        /// de la persona o entidad desarrolladora del software garante utilizado desde
        /// el dispositivo.
        /// </summary>
        public static readonly string EmpresaDesarrolladoraNif = "B12959755";

        /// <summary>
        /// apellidos y nombre o razón social 
        /// de la persona o entidad desarrolladora del software garante utilizado desde
        /// el dispositivo.
        /// </summary>
        public static readonly string EmpresaDesarrolladoraNombre = "IRENE SOLUTIONS SLU";

        /// <summary>
        /// Nombre del software garante utilizado desde el dispositivo.
        /// </summary>
        public static readonly string SoftwareGaranteNombre = "Irene.Solutions.Batuz";

        /// <summary>
        /// Versión del software garante utilizado desde el dispositivo.
        /// </summary>
        public static string SoftwareGaranteVersion { get; private set; }

        #endregion

        #region Métodos Públicos Estáticos

        /// <summary>
        /// Devuelve la información de verificación que requiere
        /// la norma.
        /// </summary>
        /// <returns></returns>
        private static string GetVerificacionPresencial() 
        {
            return  $"Empresa Desarrolladora (NIF): {EmpresaDesarrolladoraNif}\n" +
                    $"EmpresaDesarrolladora (Nombre): {EmpresaDesarrolladoraNif}\n" +
                    $"Software Garante (Nombre): {EmpresaDesarrolladoraNif}\n" +
                    $"Software Garante (Version): {EmpresaDesarrolladoraNif}\n";
        }

        #endregion

    }
}
