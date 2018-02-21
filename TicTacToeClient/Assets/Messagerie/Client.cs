using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class Client : NetworkManager
{
    protected int PORT = 4444;
    protected string HOTE = "127.0.0.1";
    protected NetworkClient contact;
    protected NetworkMessage reseau;
    protected static Client instance = null;

    public Client()
    {
        this.contact = new NetworkClient();
        this.contact.RegisterHandler(MsgType.Scene, gererMessages);
        this.contact.Connect(HOTE, PORT);
        Client.instance = this;
    }
    public virtual void gererMessages(NetworkMessage message)
    {
        string json = message.ReadMessage<StringMessage>().value;
        Debug.Log("gererMessages " + json);

        // version temporaire // TODO interpreteur
        if (json.CompareTo("{symbole:x}") == 0) ControleurGrille.getInstance().recevoirSymbole('x');
        if (json.CompareTo("{symbole:o}") == 0) ControleurGrille.getInstance().recevoirSymbole('o');
        if (json.CompareTo("{tour:x}") == 0) ControleurGrille.getInstance().recevoirTour('x');


    }
    static public Client getInstance()
    {
        return Client.instance;
    }

/*
    public void Init(NetworkClient client)
    {

    }
*/
}
