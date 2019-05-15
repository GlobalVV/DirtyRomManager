using System;
using System.Collections.Generic;
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
        private string bURL = "https://api-v3.igdb.com/games";
        string directory = Directory.GetCurrentDirectory();

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

        public List<string> getGame(string name)
        {
            checkKeyFile();

            //IGDB API Call
            List<string> games = new List<string>();
            var client = new RestClient(bURL);
            var request = new RestRequest(Method.POST);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            request.AddHeader("user-key", userKey);
            //request.AddParameter("undefined", "fields name;\nsearch " + "\""+ name +"\";\nlimit 20;", ParameterType.RequestBody);
            request.AddParameter("undefined", "fields name;\nsearch \"Sonic\";\nlimit 20;", ParameterType.RequestBody);

            GameResponse response = client.Execute<GameResponse>(request).Data;
            for (int i = 0; i < 10; i++)
            {
                //games.Add(response.games[i].name);
                Console.WriteLine(response.games[i].name);
            }
            
            return games;
        }

        public class Game
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class GameResponse
        {
            public List<Game> games { get; set; }
        }
    }
}
