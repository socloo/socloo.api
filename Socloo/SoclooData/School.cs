using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Socloo.Data
{
    public class School
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("Schools")]

        public string Name { get; set; }
        public string Grade { get; set; }
        public string SchoolBranch { get; set; }
        public string Address { get; set; }
        public List<ObjectId> AdministratorsId { get; set; }
        public List<ObjectId> StudentsId { get; set; }
        public List<ObjectId> TeachersId { get; set; }
        public List<ObjectId> GroupsId { get; set; }
        public List<ObjectId> CoursesId { get; set; }
    }
}
