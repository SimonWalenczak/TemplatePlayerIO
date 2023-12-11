using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerIOClient;

public class PlayerIOScript : MonoBehaviour
{
    public static PlayerIOScript Instance;

    private string gameID = "multichess-fkekrt3970yh1d18nu1woq";

    public Connection Pioconnection;
    private List<Message> msgList = new List<Message>(); //  Messsage queue implementation
    private bool joinedroom = false;
    private Dictionary<string, IFunction> _functions = new Dictionary<string, IFunction>();


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Application.runInBackground = true;

        // Create a random userid 
        System.Random random = new System.Random();
        string userid = "Guest" + random.Next(0, 10000);

        Debug.Log("Starting");

        PlayerIO.Authenticate(
            gameID, //Your game id
            "public", //Your connection id
            new Dictionary<string, string>
            {
                //Authentication arguments
                { "userId", userid },
            },
            null, //PlayerInsight segments
            MasterServerJoined,
            delegate(PlayerIOError error) { Debug.Log("Error connecting: " + error.ToString()); }
        );
        
        AddFunctions();
    }

    private void AddFunctions()
    {
        _functions.Add("MOVE", new MoveC2S()); //Reçoit le message du Grid Manager et l'envoie au MoveC2S.
    }
    void MasterServerJoined(Client client)
    {
        Debug.Log("Successfully connected to Player.IO");

        // Comment out the line below to use the live servers instead of your development server
        // Change "localhost" to IPv4 to connect to other people
        //client.Multiplayer.DevelopmentServer = new ServerEndpoint("localhost", 8184);

        Debug.Log("CreateJoinRoom");
        //Create or join the room 
        client.Multiplayer.CreateJoinRoom(
            "FirstRoom", //Room id. If set to null a random roomid is used
            "SimServer", //The room type started on the server
            true, //Should the room be visible in the lobby?
            null,
            null,
            RoomJoined,
            delegate(PlayerIOError error) { Debug.Log("Error Joining Room: " + error.ToString()); }
        );
    }

    void RoomJoined(Connection connection)
    {
        Debug.Log("Joined Room.");
        // We successfully joined a room so set up the message handler
        Pioconnection = connection;
        Pioconnection.OnMessage += HandleMessage;
        joinedroom = true;

        Pioconnection.Send("TEST", 42, "michel");
    }

    void HandleMessage(object sender, Message m)
    {
        msgList.Add(m);
    }
    
    private void ProcessMessageQueue()
    {
        foreach (Message m in msgList)
        {
            if (!_functions.TryGetValue(m.Type, out IFunction func))
            {
                Debug.LogWarning("Message not found + " + m.Type);
                msgList.Remove(m);
                return;
            }

            func.Execute(m);
        }

        // Clear message queue after it's been processed
        msgList.Clear();
    }

    void Update()
    {
        ProcessMessageQueue();
    }
}