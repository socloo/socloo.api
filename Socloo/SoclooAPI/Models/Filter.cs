using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace SoclooAPI.Models
{
    public class Filter
    {
        public MongoDBContext mongoDB;
        public string RemoveBadWord(string Text)
        {
            mongoDB = new MongoDBContext();
            List<FilterViewModel> it = mongoDB.database.GetCollection<FilterViewModel>("Italian Bad Words").Find(new BsonDocument()).ToList();
            List<FilterViewModel> fr = mongoDB.database.GetCollection<FilterViewModel>("French Bad Words").Find(new BsonDocument()).ToList();
            List<FilterViewModel> de = mongoDB.database.GetCollection<FilterViewModel>("German Bad Words").Find(new BsonDocument()).ToList();
            List<FilterViewModel> es = mongoDB.database.GetCollection<FilterViewModel>("Spanish Bad Words").Find(new BsonDocument()).ToList();
            List<FilterViewModel> en = mongoDB.database.GetCollection<FilterViewModel>("English Bad Words").Find(new BsonDocument()).ToList();
            Text = Text.ToLower();
            foreach (var word in it)
            {
                if (Text.ToLower().Contains(word.Text))
                {
                    string newValue = "";
                    for (int i = 0; i < word.Text.Length; i++)
                    {
                        newValue = newValue + "*";
                    }
                    Text = Text.Replace(word.Text, newValue);
                }
            }
            foreach (var word in fr)
            {
                if (Text.ToLower().Contains(word.Text))
                {
                    string newValue = "";
                    for (int i = 0; i < word.Text.Length; i++)
                    {
                        newValue = newValue + "*";
                    }
                    Text = Text.Replace(word.Text, newValue);
                }
            }
            foreach (var word in de)
            {
                if (Text.ToLower().Contains(word.Text))
                {
                    string newValue = "";
                    for (int i = 0; i < word.Text.Length; i++)
                    {
                        newValue = newValue + "*";
                    }
                    Text = Text.Replace(word.Text, newValue);
                }
            }
            foreach (var word in es)
            {
                if (Text.ToLower().Contains(word.Text))
                {
                    string newValue = "";
                    for (int i = 0; i < word.Text.Length; i++)
                    {
                        newValue = newValue + "*";
                    }
                    Text = Text.Replace(word.Text, newValue);
                }
            }
            foreach (var word in en)
            {
                if (Text.ToLower().Contains(word.Text))
                {
                    string newValue = "";
                    for (int i = 0; i < word.Text.Length; i++)
                    {
                        newValue = newValue + "*";
                    }
                    Text = Text.Replace(word.Text, newValue);
                }
            }
            return Text;
        }
    }
}
