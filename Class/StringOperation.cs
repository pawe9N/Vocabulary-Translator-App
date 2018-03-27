using System;

namespace Vocabulary_Translator_App.Class
{
    class StringOperation
    {
        public StringOperation()
        { }

        //taking two language codes and creating language pair from them
        public static string CreatingLangugePair(string fromLanguageCode, string toLanguageCode)
        {
            string languagePair = LanguageToCode(fromLanguageCode) + "|" + LanguageToCode(toLanguageCode);

            return languagePair;
        }

        public static string LanguagesToTable(string languages)
        {
            string[] words = languages.Split(" ");

            return String.Concat(LanguageToCode(words[0]), LanguageToCode(words[2]));
        }

        public static string TableToLanguages(string table)
        {
            string fromL = CodeToLanguage(table.Substring(0, 2));
            string toL = CodeToLanguage(table.Substring(2, 2));
            string languages = String.Concat(fromL, " ➤ ", toL);

            return languages;
        }

        //choosing codes for signle language
        private static string LanguageToCode(string language)
        {
            if (language == "Polish") { return "pl"; }
            else if (language == "English") { return "en"; }
            else if (language == "Spanish") { return "es"; }
            else if (language == "French") { return "fr"; }
            else if (language == "German") { return "de"; }
            else { return null; }
        }

        private static string CodeToLanguage(string code)
        {
            if (code == "pl") { return "Polish"; }
            else if (code == "en") { return "English"; }
            else if (code == "es") { return "Spanish"; }
            else if (code == "fr") { return "French"; }
            else if (code == "de") { return "German"; }
            else { return null; }
        }

    }
}
