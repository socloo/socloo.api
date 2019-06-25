using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Socloo.Data
{
    public class School
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Schools")]

        public string Name { get; set; }
        public string Grade { get; set; }
        public string SchoolBranch { get; set; }
        public string Address { get; set; }
        public List<string> AdministratorsId { get; set; }
        public List<string> StudentsId { get; set; }
        public List<string> TeachersId { get; set; }
        public List<string> GroupsId { get; set; }
        public List<string> CoursesId { get; set; }
    }
}
