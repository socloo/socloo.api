using MongoDB.Bson;
namespace SoclooAPI.Models
{
    public class Question
    {
        public ObjectId Id { get; set; }
        public string Text { get; set; }
    }
}
