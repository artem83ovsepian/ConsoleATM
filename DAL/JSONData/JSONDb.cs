using System.Text.Json;
using DAL.Entities;

namespace DAL.JSONData
{
    public class JSONDb
    {
        private readonly string fileName = "JSONData\\ATMdb.json";

        public Root DbRoot { get; set; }

        public JSONDb()
        {
            DbRoot = JsonSerializer.Deserialize<Root>(File.ReadAllText(fileName));
        }

        public void Save()
        {
            var jsonText = JsonSerializer.Serialize(DbRoot);
            File.WriteAllText(fileName, jsonText);
        }
    }
}
