using MongoDB.Bson;
namespace SoclooAPI.Models
{
    public class QuestionViewModel
    {
        public ObjectId Id { get; set; }
        public string Text { get; set; }
    }
}
