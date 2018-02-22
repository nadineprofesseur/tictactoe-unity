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
        this.contact.RegisterHandler(MsgType.Scene, recevoirMessage);
        this.contact.Connect(HOTE, PORT);
        Client.instance = this;
    }

    System.Text.RegularExpressions.Regex coquilleCoup = new System.Text.RegularExpressions.Regex("^{\"coup\":(.*)}$");

    public virtual void recevoirMessage(NetworkMessage message)
    {
        string json = message.ReadMessage<StringMessage>().value;
        Debug.Log("recevoirMessage " + json);

        // version temporaire // TODO interpreteur
        if (json.CompareTo("{\"symbole\":\"x\"}") == 0) ControleurGrille.getInstance().recevoirSymbole('x');
        if (json.CompareTo("{\"symbole\":\"o\"}") == 0) ControleurGrille.getInstance().recevoirSymbole('o');
        if (json.CompareTo("{\"tour\":\"x\"}") == 0) ControleurGrille.getInstance().recevoirTour('x');
        if (json.CompareTo("{\"tour\":\"o\"}") == 0) ControleurGrille.getInstance().recevoirTour('o');

        // https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity5.html
        if (json.Contains("{\"coup\":"))
        {
            string jsonCoup = coquilleCoup.Match(json).Groups[1].Value;
            Debug.Log("Coup deballe=" + jsonCoup);
            Protocole.Coup coup = JsonUtility.FromJson<Protocole.Coup>(jsonCoup);
            ControleurGrille.getInstance().recevoirCoup(coup);
        }

        //contact.Send(MsgType.Scene, new StringMessage("Client parle au serveur"));
    }

    public virtual void envoyerMessage(string message)
    {
        contact.Send(MsgType.Scene, new StringMessage(message));
    }

    static public Client getInstance()
    {
        return Client.instance;
    }
}
