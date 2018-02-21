using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TicTacToeServeur : MonoBehaviour {

    protected Serveur serveur;

	// Use this for initialization
	void Start () {
        this.serveur = new Serveur();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
