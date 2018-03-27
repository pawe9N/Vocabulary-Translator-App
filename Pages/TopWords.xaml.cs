using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Vocabulary_Translator_App.Class;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Vocabulary_Translator_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TopWords : Page
    {
        public TopWords()
        {
            this.InitializeComponent();
            this.SettingDictionaries();
        }

        //setting dictionary names in menuflyoutitem
        private void SettingDictionaries()
        {
            DatabaseConnection conDB = new DatabaseConnection();
            List<string> dictionaries = conDB.GettingTables();

            foreach (string dictionary in dictionaries)
            {
                CreatingMenuFlyoutItem(DictionariesMenu, StringOperation.TableToLanguages(dictionary));
            }
        }

        //creating single menuflyoutitem
        private void CreatingMenuFlyoutItem(MenuFlyout DictionariesMenu, string text)
        {
            MenuFlyoutItem item = new MenuFlyoutItem()
            {
                Text = text
            };
            item.Click += ChoosingDictionary;
            DictionariesMenu.Items.Add(item);
        }

        //setting words from database with counts
        public void SettingRows()
        {
            Words.Items.Clear();
            Translation.Items.Clear();

            DatabaseConnection conDB = new DatabaseConnection();
            List<string> rows = conDB.SearchningWordsWithHighestCount(StringOperation.LanguagesToTable(DictionariesButton.Content.ToString()));

            short iterator = 2;
            foreach (string item in rows)
            {
                if(iterator%2 == 0)
                {
                    CreatingListItems(Words, iterator/2+". "+item);
                }
                else
                {
                    CreatingListItems(Translation, item);
                }
                iterator++;
            }

        }

        //creating single listitem
        public static void CreatingListItems(ListView list, string content)
        {
            list.Items.Add(new ListViewItem().Content = content);
        }

        //choosing dictionary name from menu event
        private void ChoosingDictionary(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = (sender as MenuFlyoutItem);
            DictionariesButton.Content = item.Text;
            SettingRows();

        }
    }
}
