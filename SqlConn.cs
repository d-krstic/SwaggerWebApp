using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;
using System.Runtime.Caching;

namespace SwaggerWebApp
{
    public class SqlConn
    {
        /*
        ta class bi moral skrbeti za povezavo z SQL baza ampak sem imel probleme pri povezavi zato sem zacasno naredil da bere iz data.json za "simuliranje" delovanja baze
        */
        public static List<Email> data = new List<Email>();
        public static ObjectCache cache = MemoryCache.Default;

        public static void open() {
            using (StreamReader r = new StreamReader("data.json"))
            {
                string json = r.ReadToEnd();
                data = Deserialize<List<Email>>(json);
            }

            //napolni cache z emaili ki so bili prebrani z baze
            fillCache();
        }

        public static void save() {
            string json = System.Text.Json.JsonSerializer.Serialize(data);
            File.WriteAllText("data.json", json);
        }

        public static Email checkCache(string query) {
            return cache[query] as Email;
        }

        public static void removeFromCache(Email e) {
            cache.Remove(e.Mail);

            //ce je cache prazen (je uporabnik query-al vse emaile v njem) bo cache napolnilo z email s baze
            fillCache();
        }

        private static void fillCache() { //vzame 50% random emailov iz data in jih kopira v cache (v dejasnkem primeru bi bil % velikosti drugacen)
            Random rand = new Random();
            int n = (int)Math.Floor((double)data.Count * 0.5);
            List<int> usedIndexes = new List<int>();
            CacheItemPolicy policy = new CacheItemPolicy();

            for (int i = 0; i < n; i++) {
                int x = rand.Next(data.Count);
                if (usedIndexes.Contains(x)) {
                    i--;
                    continue;
                }
                else {
                    usedIndexes.Add(x);
                    Email toCache = data[x];
                    cache.Set(toCache.Mail, toCache, policy);
                }
            }
        }

        //JSON convert methods
        private static string Serialize<T>(T obj) {
            var mysettings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Newtonsoft.Json.Formatting.Indented
            };

            return JsonConvert.SerializeObject(obj, mysettings);
        }


        private static T Deserialize<T>(string obj) {
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };

            return JsonConvert.DeserializeObject<T>(obj, settings);
        }
    }
}
