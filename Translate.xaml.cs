using System;
using System.Text.RegularExpressions;
using Vocabulary_Translator_App.Class;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Vocabulary_Translator_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Translate : Page
    {
        public Translate()
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
                string languagePair = StringOperation.CreatingLangugePair(fromLanguageButton.Content.ToString(), toLanguageButton.Content.ToString());

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

        //adding words to database
        private async void AddToDatabase(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(TranslatedText.Text))
            {
                string languagePair = StringOperation.CreatingLangugePair(fromLanguageButton.Content.ToString(), toLanguageButton.Content.ToString());
                languagePair = languagePair.Replace("|", "");

                DatabaseConnection dbCon = new DatabaseConnection(languagePair);
                dbCon.InsertingValue(ToTranslateText.Text, TranslatedText.Text);

                MessageDialog msg = new MessageDialog(String.Format("Dodano {0} - {1} to {2} database!", ToTranslateText.Text, TranslatedText.Text, languagePair));
                await msg.ShowAsync();

                ToTranslateText.Text = "";
                TranslatedText.Text = "";
            }
            else
            {
                MessageDialog msg = new MessageDialog("You can't add empty translation to database!");
                await msg.ShowAsync();
            }
        }
    }
}
