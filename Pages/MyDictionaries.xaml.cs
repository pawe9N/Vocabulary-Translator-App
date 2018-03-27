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
    public sealed partial class MyDictionaries : Page
    {
        public MyDictionaries()
        {
            try
            {
                this.InitializeComponent();
                this.SettingTablesList();
                this.ClearingRows(Words);
                this.SettingRows();
            }   
            catch
            { }
        }

        public static void CreatingListItems(ListView list, string content)
        {
            list.Items.Add(new ListViewItem().Content = content);
        }

        public void SettingTablesList()
        {
            DatabaseConnection conDB = new DatabaseConnection();
            List<string> tables = conDB.GettingTables();

            foreach (string table in tables)
            {
                CreatingListItems(Tables, StringOperation.TableToLanguages(table));
            }

            if (Tables.Items.Count > 0)
            {
                Tables.SelectedItem = Tables.Items[0];
            }
        }

        public void SettingRows()
        {
            DatabaseConnection conDB = new DatabaseConnection();
            List<string> rows = conDB.GettingRows(StringOperation.LanguagesToTable(Tables.SelectedItem.ToString()));

            foreach (string item in rows)
            {
                 CreatingListItems(Words, item);
            }

        }

        public void ClearingRows(ListView List)
        {
            List.Items.Clear();
        }

        private void Tables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearingRows(Words);
            SettingRows();
        }
    }
}
