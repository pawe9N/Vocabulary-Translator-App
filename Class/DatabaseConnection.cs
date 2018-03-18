using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace Vocabulary_Translator_App.Class
{
    class DatabaseConnection
    {
        
        public DatabaseConnection()
        {
            InitializeDatabase();
        }

        public static void InitializeDatabase()
        {
            using (SqliteConnection db = new SqliteConnection("Filename=./Vocabulary.db"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT EXISTS EnglishPolish(Primary_Key INTEGER PRIMARY KEY AUTOINCREMENT, Word VARCHAR(64) NULL, Translation VARCHAR(64) NULL);";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();

                db.Close();
            }
        }

    }
}
