using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using IgdbAPI;
using RestSharp;
using Newtonsoft.Json;
using RestSharp.Serialization.Json;

namespace DirtyRomManager
{
    class IGDBCommunicator : IGDBCommInterface
    {
        private string userKey = "";
        private string bURL = "https://api-v3.igdb.com/";
        string directory = Directory.GetCurrentDirectory();
        private static readonly HttpClient client = new HttpClient();

        private void checkKeyFile()
        {
            if (string.IsNullOrEmpty(userKey))
            {
                try
                {
                    userKey = System.IO.File.ReadAllText(directory + "\\my_key.txt");
                    Console.WriteLine(userKey);
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine("error: " + e);
                }
            }
        }

        public void IgdbCommunicator()
        {
            userKey = null;
        }


        public void setKey(string k)
        {
            userKey = k;
        }

        public string getKey()
        {
            if (userKey != null)
            {
                return userKey;
            }

            return "";
        }

        public List<string> searchGame(string name)
        {
            checkKeyFile();

            //IGDB API Call
            List<string> games = new List<string>();

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(bURL + "games?search="+name+"&fields=name,id");
            //HttpRequestMessage reqMsg = new HttpRequestMessage();
            request.Accept = "application/json";
            request.Headers.Set("user-key", userKey);
            WebResponse response = request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            string responseString = sr.ReadToEnd();
            sr.Close();

            using (StringReader reader = new StringReader(responseString))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("\"name\": \""))
                    {
                        string temp = line.Replace("\"name\": \"", "");
                        temp = temp.Replace("\\u0026", "&");
                        temp = temp.Replace("\\u0027", "'");
                        temp = temp.Remove(0, 4);
                        games.Add(temp.Replace("\"", ""));
                    }
                }
            }
            return games;
        }
    }
}
