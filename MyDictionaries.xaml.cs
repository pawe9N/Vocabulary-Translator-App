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
            this.InitializeComponent();
            this.SettingTablesList();
        }

        public static void CreatingListItems(ListView list, string content)
        {
            list.Items.Add(new ListViewItem().Content = content);
        }

        public void SettingTablesList()
        {
            DatabaseConnection conDB = new DatabaseConnection();
            List<string> tables = conDB.GettingTables();

            foreach(string table in tables)
            {
                CreatingListItems(Tables, table);
            }

            if(Tables.Items.Count > 0)
            {
                Tables.SelectedItem = Tables.Items[0];
            }
        }
    }
}
