using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Vocabulary_Translator_App.Class;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
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

        //translate two string with translate google app
        public void GTranslateText()
        {
            if (!String.IsNullOrEmpty(ToTranslateText.Text))
            {
                //erasing white spaces from string thats appear more then one time
                Regex regex = new Regex("[ ]{2,}");
                string toTranslateText = regex.Replace(ToTranslateText.Text.Trim(), " ");

                //creating language pair to translation
                string languagePair = GTranslator.CreatingLangugePair(fromLanguageButton.Content.ToString(), toLanguageButton.Content.ToString());
                
                //translation
                GTranslator translator = new GTranslator(toTranslateText, languagePair);

                //added translation to textblock
                TranslatedText.Text = translator.translation;
                ToTranslateText.Text = toTranslateText;
            }
        }

        //translation will start after button's click 
        private void Translate_Click(object sender, RoutedEventArgs e)
        {
            GTranslateText();
        }

        //translation will start after pressing Enter key 
        private void Translate_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                GTranslateText();
            }
        }
        
        //changing name for fromLanguageButton from menu 
        private void ChangingFromLanguageName(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = (sender as MenuFlyoutItem);
            fromLanguageButton.Content = item.Text;
        }

        //changing name for toLanguageButton from menu 
        private void ChangingToLanguageName(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = (sender as MenuFlyoutItem);
            toLanguageButton.Content = item.Text;
        }

        //swaping language pair
        private void SwapingLanguages(object sender, RoutedEventArgs e)
        {
            string temp = toLanguageButton.Content.ToString();
            toLanguageButton.Content = fromLanguageButton.Content.ToString();
            fromLanguageButton.Content = temp;
        }
    }
}
