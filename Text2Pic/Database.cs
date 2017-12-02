using System;
using System.Collections.Generic;
using System.Data.SQLite; //Install System.Data.SQLite.Core 1.0.106
using System.IO;

namespace Text2Pic
{
  public class Database
    {
        private SQLiteConnection _dbConn;
        
        //Opens the connection and creates the table if it does not exist
        public Database(string name)
        {
            if (!File.Exists(name))
            {
                Connect(name);
                string sql = "CREATE TABLE WordsNdImages (wordphrase VARCHAR(200), pos VARCHAR(10), url VARCHAR(1000))";
                SQLiteCommand command = new SQLiteCommand(sql, _dbConn);
                command.ExecuteNonQuery();
            }
            else
            {
                Connect(name);
            }
        }

        private void Connect(string name)
        {
            _dbConn = new SQLiteConnection("Data Source=" + name + ";Version=3;");
            _dbConn.Open();
        }

        //For a wordphrase with a pos tag, inserts its url into the database, if it already exists it updates the url
        public void InsertPhrase(string wordphrase, string pos, string url)
        {
            if (GetUrl(wordphrase,pos)!="")
            {
                string sql = "UPDATE WordsNdImages SET url=\"" + url + "\" WHERE wordphrase=\"" + wordphrase +
                             "\" AND pos=\"" + pos + "\";";
                SQLiteCommand command = new SQLiteCommand(sql, _dbConn);
                command.ExecuteNonQuery();
            }
            else
            {
                string sql = "INSERT INTO WordsNdImages (wordphrase, pos, url) VALUES (\'" + wordphrase + "\',\'" +
                             pos +
                             "\',\'" + url + "\')";
                SQLiteCommand command = new SQLiteCommand(sql, _dbConn);
                command.ExecuteNonQuery();
            }
        }

        //Returns empty if there is no wordphrase with the correspodning pos in the table
        public string GetUrl(string wordPhrase, string pos)
        {
            string sql = "SELECT url\n" +
                         "FROM WordsNdImages\n" +
                         "WHERE wordphrase=\"" + wordPhrase + "\"\n" +
                         "AND pos=\"" + pos + "\";";
            SQLiteCommand command = new SQLiteCommand(sql,_dbConn);
            SQLiteDataReader reader = command.ExecuteReader();
            String url;
            if (reader.Read())
            {
                url = reader["url"].ToString();
            }
            else
            {
                url = "";
            }
            return url;
        }

        //Probably don't need, may return repeated wordphrases if it is given multiple pos tags
        public List<string> GetWordPhrases()
        {
            List<string> wordphrases = new List<string>();
            string sql = "SELECT wordphrase FROM WordsNdImages";
            SQLiteCommand command = new SQLiteCommand(sql,_dbConn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                wordphrases.Add(reader["wordphrase"].ToString());   
            }
            return wordphrases;
        }
    }
}