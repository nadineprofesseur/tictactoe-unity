using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class Client : NetworkManager
{
    protected int PORT = 4444;
    protected string HOTE = "127.0.0.1";
    protected NetworkClient contact;
    protected NetworkMessage reseau;

    public Client()
    {
        this.contact = new NetworkClient();
        this.contact.RegisterHandler(MsgType.Scene, gererMessages);
        this.contact.Connect(HOTE, PORT);
    }
    public virtual void gererMessages(NetworkMessage message)
    {
        string json = message.ReadMessage<StringMessage>().value;
        Debug.Log("gererMessages " + json);
    }

}
