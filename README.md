# TwitterAnalyser - implemented with MongoDB

## Features

- Extracting Twitter data from a CSV file
- Storing those records after in a MongoDB collection
- Analysing the data can be done through selecting a specific option from a _Menu_
- Results are being provided based on the following criteria:

  1. How many Twitter users are in the database?
  2. Which Twitter users link the most to other Twitter users? (The top ten.)
  3. Who is are the most mentioned Twitter users? (The top five.)
  4. Who are the most active Twitter users (The top ten)?
  5. Who are the five most grumpy (most negative tweets) and the most happy (most positive tweets)?

## Requirements

* Docker ✅

## How to run
1. Connect to your Docker container running the MongoDB DBMS through:
`docker run --rm -v $(pwd)/data:/data/db --publish=27017:27017 --name dbms -d mongo 88385afac5fe88a5ba47cd60c084bc1855cae6089a7e7d95ba24f0ba6fea1404`
	
  NB! If you have *Windows*, please replace **$(pwd)** with the path to the directory of the repository

1. Run the following command in a new shell
`docker run -it --rm elit0452/dotnet`
	-  In case you don't have the image downloaded, it will be downloaded from Docker hub 🐳. 

1. Download the sample Twitter data with: 
`wget http://cs.stanford.edu/people/alecmgo/trainingandtestdata.zip`
1. Uncompress the Twitter dataset to your current directory with:
`unzip trainingandtestdata.zip`

1. Clone the project using the  following command **or** download the repository zip file
`git clone https://github.com/elit0451/TwitterAnalyserMongoDB.git`
1. Using a shell navigate to the folder where the repository is located
1. Use your favourite text editor and change the csv file location in *TweetsRepo.cs* file to the place where the `training.1600000.processed.noemoticon.csv` is being stored 
1. Similarly, edit the *MongoHelper.cs* file with your local machine's IP address (the port stays the same)

1. The next step is to execute the following command and afterwards you can interact with the aplication 🏗
`dotnet run`
1. In order to see the queried results in the file you can start picking up the options from the menu 👍🏻
