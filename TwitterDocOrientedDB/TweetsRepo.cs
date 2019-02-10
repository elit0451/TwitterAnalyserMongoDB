using CsvHelper;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace TwitterDocOrientedDB
{
    public class TweetsRepo
    {
        List<Tweet> listOfTweets;
        MongoHelper helper = new MongoHelper();

        public TweetsRepo()
        {
            Init();
        }
        public void Init()
        {

            using (StreamReader reader = new StreamReader(@"C:\DB\test.csv"))
            {
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.HasHeaderRecord = false;
                    listOfTweets = csv.GetRecords<Tweet>().ToList();

                    StoreDataToDB();
                }
            }
        }

        public void StoreDataToDB()
        {

            foreach (Tweet tweet in listOfTweets)
            {
                helper.Create(tweet.Polarity, tweet.Id, tweet.Date, tweet.Query, tweet.User, tweet.TweetContent);
            }
        }

        public void Print()
        {
            //foreach(BsonDocument doc in helper.GetAll())
            //{
            //    Console.WriteLine(doc);
        }
    }
}
