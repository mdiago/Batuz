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


using Batuz.Negocio.Documento;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Batuz.TicketBai.Pdf
{

    /// <summary>
    /// Actualiza la información del documento en una plantilla html
    /// utilizada para la confección de la factura.
    /// </summary>
    public class RenderizadorHtml
    {

        Documento _Documento;
        string _HtmlSource;
        string _Html;

        MatchCollection _MatchCollection;

        MatchCollection _MatchCollectionTBaiCollection;

        Dictionary<string, string> _TBaiCollectionHtmlTemplates;

        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="documento">Documento a presentas.</param>
        /// <param name="html">HTML de la plantilla.</param>
        public RenderizadorHtml(Documento documento, string html) 
        {
            
            _Documento = documento;
            _Html = html;
            _HtmlSource = html;

        }

        /// <summary>
        /// Recupera las claves a renderizar.
        /// </summary>
        /// <returns>Claves a renderizar</returns>
        private void GetMatchCollectionTBaiCollection() 
        {

            string pattern = @"<[^>]+tbaicollection=" + "('|\")[^('|\")]+('|\")" + @"[^>]*>";

            _MatchCollectionTBaiCollection = Regex.Matches(_HtmlSource, pattern);

        }

        /// <summary>
        /// Recupera el tag name del elemento html cuyo atributo
        /// tbaicollection indica que se trata de un elemento de plantilla
        /// para la colección indicada como valor del atributo.
        /// </summary>
        /// <param name="sTag">Etiqueta de apertura del elemento.</param>
        /// <returns>Tag name de elemento html.</returns>
        private string GetTBaiCollectionTagName(string sTag) 
        {
            return Regex.Match(sTag, @"(?<=<)\w+(?=[^>]+tbaicollection=)").Value;
        }

        /// <summary>
        /// Devuelve el nombre de la propiedad que implementa IList.
        /// </summary>
        /// <param name="sTag">Etiqueta de apertura del elemento.</param>
        /// <returns>Nombre de la propiedad que implementa IList.</returns>
        private string GetTBaiCollectionListName(string sTag)
        {

            string pattern = @"(?<=<[^>]+tbaicollection=('|" + "\"))[^('|\")]+(?=('|\")" + @"[^>]*>)";

            return Regex.Match(sTag, pattern).Value;
        }

        /// <summary>
        /// Recupera una plantilla de colección de documento
        /// que posteriormente será utilizada para renderizar la
        /// misma.
        /// </summary>
        /// <param name="sTag">Etiqueta de apertura del elemento.</param>
        /// <returns></returns>
        private string GetTBaiCollectionHtmlTemplate(string sTag)
        {
            return Regex.Match(sTag, @"(?<=<)\w+(?=[^>]+tbaicollection=)").Value;
        }

        private void LoadTBaiCollectionHtmlTemplates() 
        {

            _TBaiCollectionHtmlTemplates = new Dictionary<string, string>();

            foreach (Match match in _MatchCollectionTBaiCollection)
                LoadTBaiCollectionHtmlTemplate(match.Value);


        }

        /// <summary>
        /// Carga las plantilla de colección y sustituye el fragmanto 
        /// html de plantilla por un marcador.
        /// </summary>
        /// <param name="sTag">Etiqueta de apertura del elemento
        /// html con el atributo tbaicollection.</param>
        private void LoadTBaiCollectionHtmlTemplate(string sTag)
        {

            var tagName = GetTBaiCollectionTagName(sTag);
            var listName = GetTBaiCollectionListName(sTag);

            string pattern = $"[\\s\\t]*<{tagName}[^>]+tbaicollection=('|\"){listName}('|\")[^>]*>[\\s\\S]*?</{tagName}>";

            string htmlTemplate = Regex.Match(_HtmlSource, pattern).Value;
            string tab = Regex.Match(_HtmlSource, $"[\\s\\t]*(?=<{tagName})").Value;

            if (!_TBaiCollectionHtmlTemplates.ContainsKey(listName)) 
                _TBaiCollectionHtmlTemplates.Add(listName, htmlTemplate);

            // Elimino del html de plantilla sustituyendolo por una clave
            _Html = Regex.Replace(_Html, pattern, $"{tab}<!--{listName}-->");


        }

        /// <summary>
        /// Recupera las claves a renderizar.
        /// </summary>
        /// <returns>Claves a renderizar</returns>
        private void GetMatchCollection()
        {

            _MatchCollection = Regex.Matches(_Html, @"(?<=\{)[^\}]+(?=\})");

        }

        /// <summary>
        /// Renderiza las colecciones.
        /// </summary>
        private string RenderizaBaiCollections() 
        {

            string html = "";

            foreach (KeyValuePair<string, string> tBaiCollection in _TBaiCollectionHtmlTemplates)
                html = RenderizaList(tBaiCollection);

            return html;

        }

        /// <summary>
        /// Renderiza en el Html las listas.
        /// </summary>
        /// <param name="tBaiCollection">Nombre de la propiedad a renderizar.</param>
        private string RenderizaList(KeyValuePair<string, string> tBaiCollection) 
        {

            var propInfo = _Documento.GetType().GetProperty(tBaiCollection.Key);

            if (propInfo == null)
                throw new InvalidCastException($"No hay ninguna colección de elementos en el documento con el nombre {tBaiCollection.Key}.");

            IList list = propInfo.GetValue(_Documento) as IList;

            var htmlTemplate = tBaiCollection.Value;

            var listMatchCollection = Regex.Matches(htmlTemplate, @"(?<=\{)[^\}]+(?=\})");

            var html = "";

            if (list != null)
            {
                foreach (var item in list)
                {

                    var htmlItem = htmlTemplate;

                    foreach (Match match in listMatchCollection)
                        htmlItem = RenderizaItemList(match.Value, item, htmlItem);

                    html += $"{htmlItem}\n";

                }
            }

            _Html = _Html.Replace($"<!--{tBaiCollection.Key}-->", html);

            return _Html;

        }

        /// <summary>
        /// Renderiza en el Html las listas.
        /// </summary>
        /// <param name="key">Nombre de la propiedad a renderizar.</param>
        private string RenderizaItemList(string key, object item, string html)
        {

            var values = key.Split(':');
            var path = values[0];
            var format = values.Length == 1 ? null : values[1];

            var steps = path.Split('.');
            object currentObj = item;

            for (int s = 0; s < steps.Length; s++)
            {

                var pInf = currentObj.GetType().GetProperty(steps[s]);
                var pValue = pInf.GetValue(currentObj);

                currentObj = pValue;

                if (currentObj == null)
                    break;

            }

            string rendered = $"{currentObj}";

            if (format != null)
            {

                if (currentObj.GetType().IsAssignableFrom(typeof(DateTime)))
                    rendered = (currentObj as DateTime?)?.ToString(format);
                else if (currentObj.GetType().IsAssignableFrom(typeof(decimal)))
                    rendered = (currentObj as decimal?)?.ToString(format);

            }

            return html.Replace($"{{{key}}}", rendered);

        }

        /// <summary>
        /// Devuelve el Html resultado de la renderización.
        /// </summary>
        /// <returns>Html resultado de la renderización.</returns>
        public string Renderiza() 
        {

            GetMatchCollectionTBaiCollection();

            LoadTBaiCollectionHtmlTemplates();

            var html =  RenderizaBaiCollections();

            _Html = html;

            GetMatchCollection();

            foreach (Match match in _MatchCollection)
                Renderiza(match);

            return _Html;

        }


        /// <summary>
        /// Renderiza en el Html una clave concreta.
        /// </summary>
        /// <param name="match">Clave enontrada.</param>
        private void Renderiza(Match match) 
        {

            var values = match.Value.Split(':');
            var path = values[0];
            var format = values.Length == 1 ? null : values[1];

            var steps = path.Split('.');
            object currentObj = _Documento;

            for (int s = 0; s < steps.Length; s++) 
            {
                
                var pInf = currentObj.GetType().GetProperty(steps[s]);
                var pValue = pInf.GetValue(currentObj);

                currentObj = pValue;

                if (currentObj == null)
                    break;

            }

            string rendered = $"{currentObj}";

            if (format != null) 
            {

                if (currentObj.GetType().IsAssignableFrom(typeof(DateTime))) 
                    rendered = (currentObj as DateTime?)?.ToString(format);
                else if (currentObj.GetType().IsAssignableFrom(typeof(decimal)))
                    rendered = (currentObj as decimal?)?.ToString(format);

            }

            _Html = _Html.Replace($"{{{match.Value}}}", rendered);

        }

      

        /// <summary>
        /// Devuelve el Html resultado de la renderización.
        /// </summary>
        /// <returns>Html resultado de la renderización.</returns>
        //public string Rederiza() 
        //{

        //    foreach (Match match in _MatchCollection)
        //        Renderiza(match);

        //    return _Html;

        //}

    }
}
