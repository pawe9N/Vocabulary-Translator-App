using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Vocabulary_Translator_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //initialize mainpage with translate page frame
        public MainPage()
        {
            this.InitializeComponent();
            MyFrame.Navigate(typeof(Translate));
        }
 
        //navigate to Translate page
        private void NavigateToTranslatePage(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(Translate));
        }

        //navigate to MyDictionaries page
        private void NavigateToMyDictionaries(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(MyDictionaries));
        }

        //navigate to TopWords page
        private void NavigateToTopWords(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(TopWords));
        }

        //navigate to ExportToCSV page
        private void NavigateToExportToCSV(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(ExportToCSV));
        }
    }
}
