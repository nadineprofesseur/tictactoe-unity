using System;
using UnityEngine;

public class ControleurGrille
{
    protected VueGrille vue;
    public ControleurGrille(VueGrille vue)
    {
        this.vue = vue;
        ControleurGrille.instance = this;
    }

    public void tester()
    {
        this.vue.afficherNomO("Jojo");
        this.vue.afficherNomX("Tutu");
    }

    static protected ControleurGrille instance = null;
    static public ControleurGrille getInstance()
    {
        return ControleurGrille.instance;
    }

    public void reagirClicCase(int colonne, int rangee)
    {
        if(this.tourActif)
        { 
            Debug.Log("colonne " + colonne + " rangee " + rangee);
            if(this.symbole == 'x') this.vue.afficherCoupX(colonne, rangee);
            if (this.symbole == 'o') this.vue.afficherCoupO(colonne, rangee);
            Client.getInstance().envoyerMessage("{\"coup\":{\"symbole\":\"" + this.symbole+ "\",\"colonne\":" + colonne+ ",\"rangee\":" + rangee+"}}");
            this.tourActif = false; // TODO faire controler par le serveur ?
        }
    }

    protected char symbole = ' ';
    protected Boolean tourActif = false;

    public void recevoirSymbole(char symbole)
    {
        this.symbole = symbole;
        Debug.Log("J'ai le symbole " + symbole);
    }
    public void recevoirTour(char symboleTour)
    {
        Debug.Log("Recevoir tour " + symboleTour);
        if (symboleTour == this.symbole) this.tourActif = true;
        Debug.Log("Est-ce mon tour ? " + this.tourActif);
    }
}
