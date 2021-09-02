using Batuz.Negocio.Documento;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
                html += RenderizaList(tBaiCollection);

            return html;

        }

        /// <summary>
        /// Renderiza en el Html las listas.
        /// </summary>
        /// <param name="tBaiCollection">Nombre de la propiedad a renderizar.</param>
        private string RenderizaList(KeyValuePair<string, string> tBaiCollection) 
        {
            
            IList list = _Documento.GetType().GetProperty(tBaiCollection.Key).GetValue(_Documento) as IList;

            if(list == null)
                throw new InvalidCastException($"No hay ninguna colección de elementos en el documento con el nombre {tBaiCollection.Key}.");

            var htmlTemplate = tBaiCollection.Value;

            var listMatchCollection = Regex.Matches(htmlTemplate, @"(?<=\{)[^\}]+(?=\})");

            var html = "";

            foreach (var item in list)
            {

                var htmlItem = htmlTemplate;

                foreach (Match match in listMatchCollection)
                    htmlItem = RenderizaItemList(match.Value, item, htmlItem);

                html += $"{htmlItem}\n";

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
