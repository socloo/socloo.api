using MongoDB.Driver;
using System;
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

                if (!list.Contains("answers"))
                {
                    database.CreateCollection("answers");

                }
                if (!list.Contains("answersmcs"))
                {
                    database.CreateCollection("answermcs");
                }
                if (!list.Contains("answersas"))
                {
                    database.CreateCollection("answersas");
                }
                if (!list.Contains("answertfs"))
                {
                    database.CreateCollection("answertfs");
                }
                if (!list.Contains("assignments"))
                {
                    database.CreateCollection("assignments");
                }
                if (!list.Contains("calendars"))
                {
                    database.CreateCollection("calendars");
                }
                if (!list.Contains("chats"))
                {
                    database.CreateCollection("chats");
                }
                if (!list.Contains("courses"))
                {
                    database.CreateCollection("courses");
                }
                if (!list.Contains("dashboards"))
                {
                    database.CreateCollection("dashboards");
                }
                if (!list.Contains("documents"))
                {
                    database.CreateCollection("documents");
                }
                if (!list.Contains("groups"))
                {
                    database.CreateCollection("groups");
                }
                if (!list.Contains("messages"))
                {
                    database.CreateCollection("messages");
                }
                if (!list.Contains("occurrences"))
                {
                    database.CreateCollection("occurrences");
                }
                if (!list.Contains("portfolios"))
                {
                    database.CreateCollection("porfolios");
                }
                if (!list.Contains("posts"))
                {
                    database.CreateCollection("posts");
                }
                if (!list.Contains("questions"))
                {
                    database.CreateCollection("questions");
                }
                if (!list.Contains("schools"))
                {
                    database.CreateCollection("schools");
                }
                if (!list.Contains("schooladmins"))
                {
                    database.CreateCollection("schooladmins");
                }
                if (!list.Contains("students"))
                {
                    database.CreateCollection("students");
                }
                if (!list.Contains("superadmins"))
                {
                    database.CreateCollection("superadmins");
                }
                if (!list.Contains("teachers"))
                {
                    database.CreateCollection("teachers");
                }
                if (!list.Contains("tests"))
                {
                    database.CreateCollection("tests");
                }
                if (!list.Contains("users"))
                {
                    database.CreateCollection("users");


                }


            }
            catch (Exception ex)
            {

            }

    




        }



    }
}
