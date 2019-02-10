using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterDocOrientedDB
{
    public class MongoHelper
    {
        private IMongoCollection<BsonDocument> collection;
        public MongoHelper()
        {
            MongoClient client = new MongoClient("mongodb://192.168.0.13:27017");
            IMongoDatabase database = client.GetDatabase("twitter");
            collection = database.GetCollection<BsonDocument>("tweets");
        }

        public void Create(int polarity, string id, string date, string query, string user, string content)
        {
            BsonDocument tweet = new BsonDocument();

            BsonElement tweetPolarity = new BsonElement("polarity", polarity);
            BsonElement tweet_id = new BsonElement("_id", id);
            BsonElement tweetDate = new BsonElement("date", date);
            BsonElement tweetQuery = new BsonElement("query", query);
            BsonElement tweetUser = new BsonElement("user", user);
            BsonElement tweetContent = new BsonElement("content", content);

            tweet.Add(tweetPolarity);
            tweet.Add(tweet_id);
            tweet.Add(tweetDate);
            tweet.Add(tweetQuery);
            tweet.Add(tweetUser);
            tweet.Add(tweetContent);

            collection.InsertOne(tweet);
        }

        public int GetNumberOfUsers()
        {
            var categoriesList = collection.Distinct<string>("user", "{}").ToList();
            return categoriesList.Count;
        }

        public List<BsonDocument> GetUsersWhoLinkOthers()
        {

            var match = new BsonDocument
                {
                    {
                        "$match",
                        new BsonDocument
                            {
                                {"content", new BsonDocument
                                    {
                                    {
                                    "$regex", "@(?:[a-z][a-z0-9_]*)"
                                    }
                                    }
                                }
                            }
                    }
                };

            var group = new BsonDocument
                {
                    { "$group",
                        new BsonDocument
                            {
                                { "_id", "$user"},
                                {
                                    "Count", new BsonDocument
                                                 {
                                                     {
                                                         "$sum", 1
                                                     }
                                                 }
                                }
                            }
                  }
                };

            var sort = new BsonDocument
            {
                {
                    "$sort", new BsonDocument
                    {
                        {
                            "Count", -1
                        }
                    }
                }
            };

            var limit = new BsonDocument
            {
                {
                    "$limit", 10
                }
            };

            var pipeline = new BsonDocument[] {match, group, sort, limit };

            var result = collection.Aggregate<BsonDocument>(pipeline).ToList();


            return result;
        }


        public List<BsonDocument> GetMostActiveUsers()
        {

            var group = new BsonDocument
                {
                    { "$group",
                        new BsonDocument
                            {
                                { "_id", "$user"},
                                {
                                    "Count", new BsonDocument
                                                 {
                                                     {
                                                         "$sum", 1
                                                     }
                                                 }
                                }
                            }
                  }
                };

            var sort = new BsonDocument
            {
                {
                    "$sort", new BsonDocument
                    {
                        {
                            "Count", -1
                        }
                    }
                }
            };

            var limit = new BsonDocument
            {
                {
                    "$limit", 10
                }
            };

            var pipeline = new BsonDocument[] { group, sort, limit };

            var result = collection.Aggregate<BsonDocument>(pipeline).ToList();


            return result;
        }
        public List<BsonDocument> GetMostNegativeUsers()
        {
            var match = new BsonDocument
                {
                    {
                        "$match",
                        new BsonDocument
                            {
                                {"polarity", 0}
                            }
                    }
                };

            var group = new BsonDocument
                {
                    { "$group",
                        new BsonDocument
                            {
                                { "_id", "$user"},
                                {
                                    "Count", new BsonDocument
                                                 {
                                                     {
                                                         "$sum", 1
                                                     }
                                                 }
                                }
                            }
                  }
                };

            var sort = new BsonDocument
            {
                {
                    "$sort", new BsonDocument
                    {
                        {
                            "Count", -1
                        }
                    }
                }
            };

            var limit = new BsonDocument
            {
                {
                    "$limit", 5
                }
            };

            var pipeline = new BsonDocument[] {match, group, sort, limit};

            var result = collection.Aggregate<BsonDocument>(pipeline).ToList();


            return result;
        }

        internal List<BsonDocument> GetMostMentionedUsers()
        {
            var match = new BsonDocument
                {
                    {
                        "$match",
                        new BsonDocument
                            {
                                {"content", new BsonDocument
                                    {
                                    {
                                    "$regex", "@(?:[a-z][a-z0-9_]*)"
                                    }
                                    }
                                }
                            }
                    }
                };

            var group = new BsonDocument
                {
                    { "$group",
                        new BsonDocument
                            {
                                { "_id", new BsonDocument
                                             {
                                                 {
                                                     "$substrCP", new BsonArray
                                                     {
                                                         "$content", new BsonDocument
                                                         {
                                                             {
                                                                 "$indexOfCP", new BsonArray
                                                                 {
                                                                     "$content", "@"
                                                                 }
                                                             }
                                                         }, new BsonDocument
                                                         {
                                                             {
                                                                 "$indexOfCP", new BsonArray
                                                                 {
                                                                     "$content", " "
                                                                 }
                                                             }
                                                         }
}
                                                 }
                                             }
                                },
                                {
                                    "Count", new BsonDocument
                                                 {
                                                     {
                                                         "$sum", 1
                                                     }
                                                 }
                                }
                            }
                  }
                };

            var sort = new BsonDocument
            {
                {
                    "$sort", new BsonDocument
                    {
                        {
                            "Count", -1
                        }
                    }
                }
            };

            var limit = new BsonDocument
            {
                {
                    "$limit", 5
                }
            };

            var pipeline = new BsonDocument[] { match, group, sort, limit };

            var result = collection.Aggregate<BsonDocument>(pipeline).ToList();


            return result;
        }

        public List<BsonDocument> GetMostPositiveUsers()
        {
            var match = new BsonDocument
                {
                    {
                        "$match",
                        new BsonDocument
                            {
                                {"polarity", 4}
                            }
                    }
                };

            var group = new BsonDocument
                {
                    { "$group",
                        new BsonDocument
                            {
                                { "_id", "$user"},
                                {
                                    "Count", new BsonDocument
                                                 {
                                                     {
                                                         "$sum", 1
                                                     }
                                                 }
                                }
                            }
                  }
                };

            var sort = new BsonDocument
            {
                {
                    "$sort", new BsonDocument
                    {
                        {
                            "Count", -1
                        }
                    }
                }
            };

            var limit = new BsonDocument
            {
                {
                    "$limit", 5
                }
            };

            var pipeline = new BsonDocument[] { match, group, sort, limit };

            var result = collection.Aggregate<BsonDocument>(pipeline).ToList();


            return result;
        }
    }
}
