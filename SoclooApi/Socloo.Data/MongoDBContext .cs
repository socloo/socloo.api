using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Socloo.Data
{
    class MongoDBContext
    {
      
        public MongoDBContext()
        {


            MongoClient client = new MongoClient("mongodb+srv://Admin:admin@socloocluster-xfypo.mongodb.net/test?retryWrites=true&w=majority");
            var database = client.GetDatabase("SoclooDevDb");
            try
            {
                database.CreateCollection("Users");
                database.CreateCollection("Tests");
                database.CreateCollection("Teachers");
                database.CreateCollection("SuperAdmins");
                database.CreateCollection("Students");
                database.CreateCollection("SchoolAdmins");
                database.CreateCollection("Schools");
                database.CreateCollection("Questions");
                database.CreateCollection("Posts");
                database.CreateCollection("Porfolios");
                database.CreateCollection("Occurrences");
                database.CreateCollection("Messages");
                database.CreateCollection("Groups");
                database.CreateCollection("Documents");
                database.CreateCollection("Dashboards");
                database.CreateCollection("Courses");
                database.CreateCollection("Chats");
                database.CreateCollection("Calendars");
                database.CreateCollection("Assignments");
                database.CreateCollection("AnswerTFs");
                database.CreateCollection("AnswerSAs");
                database.CreateCollection("AnswerMCs");
                database.CreateCollection("Answers");
            }catch(Exception ex)
            {
                throw new Exception(" " +ex);
            }
      

        }
       





    }
}
