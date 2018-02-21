﻿using System;
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
        this.enDemarrage = false;
        Debug.Log("Listen " + PORT);
    }
    public virtual void gererConnexion(NetworkMessage message)
    {
        Debug.Log("gererConnexion()");
        if (null == connexionX)
        {
            connexionX = message.conn;
            Debug.Log("Joueur X connecté");
            connexionX.Send(MsgType.Scene, new StringMessage("Vous etes le joueur X"));
        }
        else if (null == connexionO)
        {
            connexionO = message.conn;
            Debug.Log("Joueur O connecté");
            NetworkServer.SendToAll(MsgType.Scene, new StringMessage("Le jeu commence"));
        }
        else
        {
            Debug.Log("Nouveau joueur ignoré");
        }

    }
}