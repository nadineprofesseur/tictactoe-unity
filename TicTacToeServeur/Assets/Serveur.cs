using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class Serveur : NetworkManager
{
    protected int PORT = 4444;
    protected bool enDemarrage = true;
    protected NetworkClient serveur;
    protected NetworkClient client1;
    protected StringMessage test;
    protected NetworkConnection connexionX = null;
    protected NetworkConnection connexionO = null;
    protected Boolean pret = false;

    public void Init(NetworkClient client)
    {
        this.client1 = client;
        Debug.Log("Init()");
    }

    public Serveur()
	{
        NetworkServer.Listen(PORT);
        NetworkServer.RegisterHandler(MsgType.Connect, gererConnexion);
        NetworkServer.RegisterHandler(MsgType.Scene, recevoirMessage);
        this.enDemarrage = false;
        Debug.Log("Listen " + PORT);
    }

    // https://docs.unity3d.com/Manual/JSONSerialization.html
    [System.Serializable]
    public class Coup
    {
        public char symbole;
        public int colonne;
        public int rangee;
    };
    System.Text.RegularExpressions.Regex coquilleCoup = new System.Text.RegularExpressions.Regex("^{\"coup\":(.*)}$");

    public virtual void recevoirMessage(NetworkMessage message)
    {
        string json = message.ReadMessage<StringMessage>().value;
        Debug.Log("Message recu de Scene " + json);

        // https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity5.html
        if (json.Contains("{\"coup\":"))
        {
            json = coquilleCoup.Match(json).Groups[1].Value;
            Debug.Log("Coup deballe=" + json);
            Coup coup = JsonUtility.FromJson<Coup>(json);
        }
    }
    public virtual void gererConnexion(NetworkMessage message)
    {
        Debug.Log("gererConnexion()");
        if (null == connexionX)
        {
            connexionX = message.conn;
            Debug.Log("Joueur X connecté");
            connexionX.Send(MsgType.Scene, new StringMessage("{\"symbole\":\"x\"}"));
        }
        else if (null == connexionO)
        {
            connexionO = message.conn;
            Debug.Log("Joueur O connecté");
            connexionO.Send(MsgType.Scene, new StringMessage("{\"symbole\":\"o\"}"));
            this.pret = true;
            connexionX.Send(MsgType.Scene, new StringMessage("{\"tour\":\"x\"}"));
            connexionO.Send(MsgType.Scene, new StringMessage("{\"tour\":\"x\"}"));
            //NetworkServer.SendToAll(MsgType.Scene, new StringMessage("{tour:x}"));
        }
        else
        {
            Debug.Log("Nouveau joueur ignoré");
        }

    }
}
