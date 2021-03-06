﻿using MongoDB.Bson;
using System;

namespace TwitterDocOrientedDB
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        private void Run()
        {
            Console.WriteLine("Importing data... Please wait!");
            new TweetsRepo();
            Console.Clear();

            string title = @"
 ____  _  _  __  ____  ____  ____  ____ 
(_  _)/ )( \(  )(_  _)(_  _)(  __)(  _ \
  )(  \ /\ / )(   )(    )(   ) _)  )   /
 (__) (_/\_)(__) (__)  (__) (____)(__\_)
                ";
            Console.WriteLine(title);
            Console.WriteLine("Welcome to the amazing Twitter analytics!");
            Console.WriteLine("\tWhat information would you like to see...");
            Menu();
        }

        private int PickedOption()
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1) Amount of Twitter users");
            Console.WriteLine("2) Which Twitter user links other users the most");
            Console.WriteLine("3) Top 5 of the most popular (mentioned) Twitter users");
            Console.WriteLine("4) Topo 10 of the most active Twitter users");
            Console.WriteLine("5) The 5 most negative tweets");
            Console.WriteLine("6) The 5 most positive tweets");
            Console.WriteLine("7) Exit");
            Console.WriteLine();
            Console.WriteLine("Please choose an option:");

            int inputNum;
            if (Int32.TryParse(Console.ReadLine(), out inputNum) == false)
            {
                inputNum = 0;
            }
            Console.Clear();
            return inputNum;
        }

        private void Menu()
        {
            MongoHelper helper = new MongoHelper();
            bool isRunning = true;
            do
            {
                int input = PickedOption();
                switch (input)
                {
                    case 1:
                        Console.WriteLine("Amount of Twitter users: " + helper.GetNumberOfUsers());
                        break;
                    case 2:
                        Console.WriteLine("Twitter users who linked the most to other Twitter users:");
                        PrintUsersWhoLinkTheMost(helper);
                        break;
                    case 3:
                        Console.WriteLine("Twitter users who are mentioned the most:");
                        PrintMostMentionedUsers(helper);
                        break;
                    case 4:
                        Console.WriteLine("Twitter users who were the most active:");
                        PrintActiveUsers(helper);
                        break;
                    case 5:
                        Console.WriteLine("Twitter users who are the most grumpiest:");
                        PrintNegativeUsers(helper);
                        break;
                    case 6:
                        Console.WriteLine("Twitter users who are the most happiest:");
                        PrintPositiveUsers(helper);
                        break;
                    case 7:
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Wrong input! Please, try again!");
                        break;
                }

                if (isRunning)
                    Console.ReadKey();

                Console.Clear();
            } while (isRunning);
        }


        private void PrintUsersWhoLinkTheMost(MongoHelper helper)
        {
            foreach (var r in helper.GetUsersWhoLinkOthers())
            {
                Console.WriteLine(r.ToJson());
            }
        }
        private void PrintMostMentionedUsers(MongoHelper helper)
        {
            foreach (var r in helper.GetMostMentionedUsers())
            {
                Console.WriteLine(r.ToJson());
            }
        }

        private void PrintActiveUsers(MongoHelper helper)
        {
            foreach (var r in helper.GetMostActiveUsers())
            {
                Console.WriteLine(r.ToJson());
            }
        }
        private void PrintNegativeUsers(MongoHelper helper)
        {
            foreach (var r in helper.GetMostNegativeUsers())
            {
                Console.WriteLine(r.ToJson());
            }
        }

        private void PrintPositiveUsers(MongoHelper helper)
        {
            foreach (var r in helper.GetMostPositiveUsers())
            {
                Console.WriteLine(r.ToJson());
            }
        }
    }
}
