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
        //this.vue.afficherCoupX(3, 3);
        //this.vue.afficherCoupO(2, 2);
        //this.vue.afficherCoupX(1, 1);
    }

    static protected ControleurGrille instance = null;
    static public ControleurGrille getInstance()
    {
        return ControleurGrille.instance;
    }

    public void reagirClicCase(int colonne, int rangee)
    {
        Debug.Log("colonne " + colonne + " rangee " + rangee);
        if(this.symbole == 'x') this.vue.afficherCoupX(colonne, rangee);
        if (this.symbole == 'o') this.vue.afficherCoupO(colonne, rangee);
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
        if (symboleTour == symbole) this.tourActif = false;

    }
}
