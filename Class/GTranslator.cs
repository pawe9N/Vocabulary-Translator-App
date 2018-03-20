using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary_Translator_App.Class
{
    class GTranslator
    {
        //variable which is used to storing translation
        public string translation;
        public GTranslator(string textToTranslate, string languagePair)
        {
            translation = Task.Run(() => TranslateText(textToTranslate, languagePair)).Result;
        }
         
        //translation from translate google
        public async Task<string> TranslateText(string textToTranslate, string languagePair)
        {
            //request for translation
            WebRequest request = WebRequest.Create(String.Format("http://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", textToTranslate, languagePair));
            WebResponse response = await request.GetResponseAsync(); ;
            Stream data = response.GetResponseStream();
            string html = String.Empty;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (StreamReader stream = new StreamReader(data, Encoding.GetEncoding(28592)))
            {
                html = stream.ReadToEnd();
            }

            //taking only translation from html
            html = html.Substring(html.IndexOf("<span title=\"") + "<span title=\"".Length);
            html = html.Substring(html.IndexOf(">") + 1);
            html = html.Substring(0, html.IndexOf("</span>"));
            html = html.Replace("&#39;", "'");

            return html;
        }
    }
}
