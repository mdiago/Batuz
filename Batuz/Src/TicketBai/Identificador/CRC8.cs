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

namespace Batuz.TicketBai.Identificador
{

    /// <summary>
    /// Clase para el cálculo del Checsum CRC8.
    /// </summary>
    public static class CRC8
    {

        #region Variables Privadas Estáticas

        /// <summary>
        /// Datos cálculo.
        /// </summary>
        static readonly byte[] _Table =
        {
            0, 94, 188, 226, 97, 63, 221, 131, 194, 156, 126,
            32, 163, 253, 31, 65, 157, 195, 33, 127, 252, 162,
            64, 30, 95, 1, 227, 189, 62, 96, 130, 220, 35,
            125, 159, 193, 66, 28, 254, 160, 225, 191, 93, 3,
            128, 222, 60, 98, 190, 224, 2, 92, 223, 129, 99,
            61, 124, 34, 192, 158, 29, 67, 161, 255, 70, 24,
            250, 164, 39, 121, 155, 197, 132, 218, 56, 102,
            229, 187, 89, 7, 219, 133, 103, 57, 186, 228, 6,
            88, 25, 71, 165, 251, 120, 38, 196, 154, 101, 59,
            217, 135, 4, 90, 184, 230, 167, 249, 27, 69, 198,
            152, 122, 36, 248, 166, 68, 26, 153, 199, 37, 123,
            58, 100, 134, 216, 91, 5, 231, 185, 140, 210, 48,
            110, 237, 179, 81, 15, 78, 16, 242, 172, 47, 113,
            147, 205, 17, 79, 173, 243, 112, 46, 204, 146,
            211, 141, 111, 49, 178, 236, 14, 80, 175, 241, 19,
            77, 206, 144, 114, 44, 109, 51, 209, 143, 12, 82,
            176, 238, 50, 108, 142, 208, 83, 13, 239, 177,
            240, 174, 76, 18, 145, 207, 45, 115, 202, 148, 118,
            40, 171, 245, 23, 73, 8, 86, 180, 234, 105, 55,
            213, 139, 87, 9, 235, 181, 54, 104, 138, 212, 149,
            203, 41, 119, 244, 170, 72, 22, 233, 183, 85, 11,
            136, 214, 52, 106, 43, 117, 151, 201, 74, 20, 246,
            168, 116, 42, 200, 150, 21, 75, 169, 247, 182, 232,
            10, 84, 215, 137, 107, 53
        };

        #endregion

        #region Métodos Públicos Estáticos

        /// <summary>
        /// Calcula el Checksum.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte ComputeChecksum(params byte[] bytes)
        {
            byte crc = 0;
            if (bytes != null && bytes.Length > 0)
                foreach (byte b in bytes)
                    crc = _Table[crc ^ b];

            return crc;

        }

        #endregion

    }
}
