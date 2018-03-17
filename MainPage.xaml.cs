using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Vocabulary_Translator_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        public async Task<string> TranslateText(string input, string languagePair)
        {
            WebRequest request = WebRequest.Create(String.Format("http://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", input, languagePair));
            WebResponse response = await request.GetResponseAsync(); ;
            Stream data = response.GetResponseStream();
            string html = String.Empty;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (StreamReader stream = new StreamReader(data, Encoding.GetEncoding(28592)))
            {
                html = stream.ReadToEnd();
            }
            html = html.Substring(html.IndexOf("<span title=\"") + "<span title=\"".Length);
            html = html.Substring(html.IndexOf(">") + 1);
            html = html.Substring(0, html.IndexOf("</span>"));

            return html;

        }

        private void Translate_Click(object sender, RoutedEventArgs e)
        {
            string toTranslateText = ToTranslateText.Text;

            var translatedText = Task.Run(() => TranslateText(toTranslateText, "en|pl")).Result;

            TranslatedText.Text = translatedText;
        }
    }
}
