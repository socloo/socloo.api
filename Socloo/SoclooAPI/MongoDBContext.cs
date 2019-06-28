using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SoclooAPI
{
    public class MongoDBContext
    {
        public MongoClient client = new MongoClient("mongodb+srv://Admin:admin@socloocluster-xfypo.mongodb.net/test?retryWrites=true&w=majority");
        public IMongoDatabase database;
        public MongoDBContext()
        {


            database = client.GetDatabase("SoclooDevDb");

            try
            {
                List<string> list = database.ListCollectionNames().ToList<string>();
       
                if (!list.Contains("Answer"))
                {
                    database.CreateCollection("Answer");

                }
                if (!!list.Contains("AnswersMCs"))
                {
                    database.CreateCollection("AnswerMCs");
                }
                if (!list.Contains("AnswerSAs"))
                {
                    database.CreateCollection("AnswerSAs");
                }
                if (!list.Contains("AnswerTFs"))
                {
                    database.CreateCollection("AnswerTFs");
                }
                if (!list.Contains("Assignments"))
                {
                    database.CreateCollection("Assignments");
                }
                if (!list.Contains("Calendars"))
                {
                    database.CreateCollection("Calendars");
                }
                if (!list.Contains("Chats"))
                {
                    database.CreateCollection("Chats");
                }
                if (!list.Contains("Courses"))
                {
                    database.CreateCollection("Courses");
                }
                if (!list.Contains("Dashboards"))
                {
                    database.CreateCollection("Dashboards");
                }
                if (!list.Contains("Documents"))
                {
                    database.CreateCollection("Documents");
                }
                if (!list.Contains("Groups"))
                {
                    database.CreateCollection("Groups");
                }
                if (!list.Contains("Messages"))
                {
                    database.CreateCollection("Messages");
                }
                if (!list.Contains("Occurrences"))
                {
                    database.CreateCollection("Occurrences");
                }
                if (!list.Contains("Porfolios"))
                {
                    database.CreateCollection("Porfolios");
                }
                if (!list.Contains("Posts"))
                {
                    database.CreateCollection("Posts");
                }
                if (!list.Contains("Questions"))
                {
                    database.CreateCollection("Questions");
                }
                if (!list.Contains("Schools"))
                {
                    database.CreateCollection("Schools");
                }
                if (!list.Contains("SchoolAdmins"))
                {
                    database.CreateCollection("SchoolAdmins");
                }
                if (!list.Contains("Students"))
                {
                    database.CreateCollection("Students");
                }
                if (!list.Contains("SuperAdmins"))
                {
                    database.CreateCollection("SuperAdmins");
                }
                if (!list.Contains("Teachers"))
                {
                    database.CreateCollection("Teachers");
                }
                if (!list.Contains("Tests"))
                {
                    database.CreateCollection("Tests");
                }
                if (!list.Contains("Users"))
                {
                    //  MongoDB.Bson.Serialization.BsonSerializer.RegisterSerializer(typeof(User), new UserSerializer());
                    database.CreateCollection("Users");

                    // IMongoCollection<User> collection = database.GetCollection<User>("User");
                    //    collection.Indexes.CreateMany(new User);


                    //   collection.DocumentSerializer.Serialize(MongoDB.Bson.Serialization.BsonSerializer.RegisterSerializer(typeof(User), new UserSerializer()));


                }


            }
            catch (Exception ex)
            {

            }






        }



    }
}
