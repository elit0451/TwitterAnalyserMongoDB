using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper.Configuration.Attributes;

namespace TwitterDocOrientedDB
{
    public class Tweet
    {
        [Index(0)]
        public int Polarity { get; set; }

        [Index(1)]
        public string Id { get; set; }

        [Index(2)]
        public string Date { get; set; }

        [Index(3)]
        public string Query { get; set; }

        [Index(4)]
        public string User { get; set; }

        [Index(5)]
        public string TweetContent { get; set; }

        public Tweet()
        {

        }

        public Tweet(int polarity, string id, string date, string query, string user, string content)
        {
            Polarity = polarity;
            Id = id;
            Date = date;
            Query = query;
            User = user;
            TweetContent = content;
        }
    }
}
