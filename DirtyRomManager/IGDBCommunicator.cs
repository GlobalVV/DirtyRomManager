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
        //k is temporary and should be removed
        private string userKey = "";
        private string bURL = "https://api-v3.igdb.com/games";

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
            List<string> games = new List<string>();
            var client = new RestClient(bURL);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("content-length", "38");
            request.AddHeader("accept-encoding", "gzip, deflate");
            //request.AddHeader("cookie", "__cfduid=d2ea416a09cd63ed63510ce10beff96e91557864505");
            request.AddHeader("Host", "api-v3.igdb.com");
            //request.AddHeader("Postman-Token", "b3aae80a-3783-497b-bab7-d75e3238cf5d,379b8ee0-c400-4ac9-bf75-5c66a94703df");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", "PostmanRuntime/7.11.0");
            request.AddHeader("Content-Type", "text/plain");
            request.AddHeader("user-key", userKey);
            //request.AddParameter("undefined", "fields name;\nsearch " + "\""+ name +"\";\nlimit 20;", ParameterType.RequestBody);
            request.AddParameter("undefined", "fields name;\nsearch \"Sonic\";\nlimit 20;", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            GameResponse temp = JsonConvert.DeserializeObject<GameResponse>(response.Content);
            for (int i = 0; i < temp.games.Count; i++)
            {
                Console.WriteLine(temp.games[i].name);
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
