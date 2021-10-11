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
using System.IO;
using System.Xml.Serialization;

namespace Batuz.Negocio.Configuracion
{

    /// <summary>
    /// Contiene la información de los parámetros de la 
    /// aplicación.
    /// </summary>
    [Serializable]
    [XmlRoot("Configuracion")]
    public class Parametros
    {

        #region Variables Privadas Estáticas

        /// <summary>
        /// Path separator win="\" and linux ="/".
        /// </summary>
        static char _SeparadorRuta = Path.DirectorySeparatorChar;

        /// <summary>
        /// Ruta al directorio de configuración.
        /// </summary>
        public static string RutaConfiguracion = Environment.GetFolderPath(
            Environment.SpecialFolder.CommonApplicationData) + $"{_SeparadorRuta}Batuz{_SeparadorRuta}";


        /// <summary>
        /// Nombre del fichero de configuración.
        /// </summary>
        internal static string FileName = "Configuracion.xml";

        #endregion

        #region Construtores Estáticos

        /// <summary>
        /// Constructor estático de la clase Settings.
        /// </summary>
        static Parametros()
        {
            Iniciar();
        }

        #endregion

        #region Métodos Privados Estáticos

        /// <summary>
        /// Inicia estáticos.
        /// </summary>
        /// <returns>Devuelve la configuración actual del sistema.</returns>
        internal static Parametros Iniciar()
        {

            string FullPath = $"{RutaConfiguracion}{_SeparadorRuta}{FileName}";


            if (File.Exists(FullPath))
                Actual = DesdeArchivo(FullPath);
            else
                Actual = PorDefecto();

            RevisaDirectorios();
            LimpiaTemporales();

            return Actual;
        }

        /// <summary>
        /// Aseguro existencia de directorios de trabajo.
        /// </summary>
        private static void RevisaDirectorios()
        {
            if (!Directory.Exists(RutaConfiguracion))
                Directory.CreateDirectory(RutaConfiguracion);

            if (!Directory.Exists(Actual.ParametrosAlmacen.RutaAlmacenamiento))
                Directory.CreateDirectory(Actual.ParametrosAlmacen.RutaAlmacenamiento);

            if (!Directory.Exists(Actual.ParametrosAlmacen.RutaArchivosTemporales))
                Directory.CreateDirectory(Actual.ParametrosAlmacen.RutaArchivosTemporales);

        }

        /// <summary>
        /// Vacia el directorio de archivos temporales.
        /// </summary>
        private static void LimpiaTemporales()
        {
            
            foreach (var archivo in Directory.GetFiles((Actual.ParametrosAlmacen.RutaArchivosTemporales))) 
            {
                try
                {                      
                    File.Delete(archivo);
                }
                catch
                {
                }                   
            }
          
        }

        /// <summary>
        /// Carga la configuración en curso desde un archivo.
        /// </summary>
        /// <param name="path">Ruta al fichero xml de configuración.</param>
        /// <returns>Configuración en curso.</returns>
        private static Parametros DesdeArchivo(string path)
        {

            Parametros result = null;

            XmlSerializer serializer = new XmlSerializer(typeof(Parametros));

            using (StreamReader r = new StreamReader(path))
                result = serializer.Deserialize(r) as Parametros;

            return result;
        }

        /// <summary>
        /// Devuelve la configuración por defecto.
        /// </summary>
        /// <returns>Configuración por defecto.</returns>
        private static Parametros PorDefecto()
        {
            return new Parametros()
            {
                ParametrosAlmacen = new ParametrosAlmacen()
                {
                    RutaAlmacenamiento = RutaConfiguracion + $"Data{_SeparadorRuta}",
                    RutaArchivosTemporales = RutaConfiguracion + $"Temp{_SeparadorRuta}",
                },
            };
        }

        #endregion

        #region Propiedades Públicas Estáticas

        /// <summary>
        /// Configuración en curso.
        /// </summary>
        public static Parametros Actual { get; private set; }

        #endregion

        #region Propiedades Públicas de Instancia

        /// <summary>
        /// Paremtros de almacenamiento.
        /// </summary>
        public ParametrosAlmacen ParametrosAlmacen { get; set; }

        #endregion

    }
}
