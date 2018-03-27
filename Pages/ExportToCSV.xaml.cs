using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Vocabulary_Translator_App.Class;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Vocabulary_Translator_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ExportToCSV : Page
    {
        //folder where to export csv
        StorageFolder folder;
        public ExportToCSV()
        {
            this.InitializeComponent();
            this.SettingDictionaries();
        }

        //show folderpicker dialog
        private async void ShowFolderPickerDialog(object sender, RoutedEventArgs e)
        {
            FolderPicker fp = new FolderPicker()
            {
                SuggestedStartLocation = PickerLocationId.Desktop,     
            };

            fp.FileTypeFilter.Add("*");

            folder = await fp.PickSingleFolderAsync();
            if(folder != null)
                ChosenPathText.Text = folder.Path;

            EnableExport(ChosenDictionary.Text, ChosenPathText.Text);
        }

        //enable button to make export to csv
        private void EnableExport(string directory, string path)
        {
            if(directory != "You choose ... dictionary" && path != "You choose ... path")
            {
                ExportButton.IsEnabled = true;
            }
        }

        //setting dictionaries flyout
        private void SettingDictionaries()
        {
            DatabaseConnection conDB = new DatabaseConnection();
            List<string> dictionaries = conDB.GettingTables();

            foreach (string dictionary in dictionaries)
            {
                CreatingMenuFlyoutItem(DictionariesMenu, StringOperation.TableToLanguages(dictionary));
            }
        }

        //creating single menu item
        private void CreatingMenuFlyoutItem(MenuFlyout DictionariesMenu, string text)
        {
            MenuFlyoutItem item = new MenuFlyoutItem()
            {
                Text = text
            };
            item.Click += ChoosingDictionary;
            DictionariesMenu.Items.Add(item);
        }

        //choosing dictionary
        private void ChoosingDictionary(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = (sender as MenuFlyoutItem);
            ChosenDictionary.Text = item.Text;

            EnableExport(ChosenDictionary.Text, ChosenPathText.Text);
        }

        //making export from database to csv file
        private async void ExportToCSVFile(object sender, RoutedEventArgs e)
        {
            string dictionary = StringOperation.LanguagesToTable(ChosenDictionary.Text);
            string path = Path.Combine(ChosenPathText.Text, dictionary+".csv");
            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Word, Translation");

            DatabaseConnection dbCon = new DatabaseConnection();
            List<string> words = dbCon.GettingRows(dictionary);

            foreach(string word in words)
            {
                sb.AppendLine(word.Replace(" ➤", ","));
            }

            try
            {
                await folder.GetFileAsync(dictionary + ".csv");
            }
            catch
            {
                await folder.CreateFileAsync(dictionary + ".csv");
            }

            StorageFile csvFile = await folder.GetFileAsync(dictionary + ".csv");
            await FileIO.WriteTextAsync(csvFile,  sb.ToString(), Windows.Storage.Streams.UnicodeEncoding.Utf8);
        }
    }
}
