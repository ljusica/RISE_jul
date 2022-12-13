using System;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
using UnityEngine;
using System.Threading;

public class ConnectionHandler : MonoBehaviour
{
    private AmazonDynamoDBClient client;
    private DynamoDBContext context;

    public void DynamoDBConnector()
    {
        // Create an AWS credentials object that includes your AWS access key and secret key.
        AWSCredentials credentials = new BasicAWSCredentials("my-access-key", "my-secret-key");

        // Create an Amazon DynamoDB client that uses the AWS credentials to authenticate requests.
        client = new AmazonDynamoDBClient(credentials);

        // Use the Amazon DynamoDB client to create a connection to the database.
        context = new DynamoDBContext(client);
    }

    // Other methods for executing operations on the database go here.

    public async void InsertItemAsync(string itemId, string itemName, int itemPrice)
    {
        // Create a new item object with the specified string and integer values.
        var item = new Document
        {
            ["Username"] = "player1",
            ["HighScore1"] = 10.5,
            ["HighScore2"] = 15.3,
            ["HighScore3"] = 20.0
        };

        // Create a DynamoDBOperationConfig object with the specified table name.
        var config = new DynamoDBOperationConfig
        {
            OverrideTableName = "UserProfile"
        };

        // Use the DynamoDBContext to insert the item into the specified table.
        CancellationToken cancellationToken = default(CancellationToken);
        await context.SaveAsync(item, config, cancellationToken);
    }


}

