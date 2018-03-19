using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace Vocabulary_Translator_App.Class
{
    class DatabaseConnection
    {
        private string TableName { get; set; }

        public DatabaseConnection()
        {
        }

        public DatabaseConnection(string languagePair)
        {
            this.TableName = languagePair;
            InitializeDatabase();
        }

        //initialization 
        private void InitializeDatabase()
        {
            using (SqliteConnection db = new SqliteConnection("Filename=./Vocabulary.db"))
            {
                db.Open();

                //create table if not exist
                String tableCommand = String.Format("CREATE TABLE IF NOT EXISTS {0}(Primary_Key INTEGER PRIMARY KEY AUTOINCREMENT, Word VARCHAR(64) NULL, Translation VARCHAR(64) NULL);", this.TableName.Replace("|", ""));
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();

                db.Close();
            }
        }

        //inserting value
        public void InsertingValue(string toTranslateText, string translatedText)
        {
            using (SqliteConnection db = new SqliteConnection("Filename=./Vocabulary.db"))
            {
                db.Open();

                //inserting value
                //used parameterized query to prevent SQL injection attacks
                SqliteCommand insertCommand = new SqliteCommand(String.Format("INSERT INTO {0} VALUES (NULL, @Word, @Translation);", this.TableName), db);
                insertCommand.Parameters.AddWithValue("@Word", toTranslateText);
                insertCommand.Parameters.AddWithValue("@Translation", translatedText);
                insertCommand.ExecuteReader();

                db.Close();
            }
        }

        //geting value from database
        public List<String> GettingTables()
        {
            List<String> tables = new List<string>();

            using (SqliteConnection db = new SqliteConnection("Filename=./Vocabulary.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand("SELECT name FROM sqlite_master WHERE type = 'table' AND name NOT LIKE 'sqlite_sequence';", db);
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    tables.Add(query.GetString(0));
                }

                db.Close();
            }

            return tables;
        }

    }
}
