using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Socloo.Data
{
    class School
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Schools")]

        public string Name { get; set; }
        public string Grade { get; set; }
        public string SchoolBranch { get; set; }
        public string Address { get; set; }
        public List<SchoolAdmin> Administrators { get; set; }
        public List<Student> Students{ get; set; }
        public List<Teacher> Teachers{ get; set; }
        public List<Group> Groups{ get; set; }
        public List<Course> Courses { get; set; }
}
}
