using System;
using UnityEngine;

public class ControleurGrille
{
    protected VueGrille vue;
    public ControleurGrille(VueGrille vue)
    {
        this.vue = vue;
    }

    public void tester()
    {
        this.vue.afficherNomO("Jojo");
        this.vue.afficherNomX("Tutu");
        this.vue.afficherCoupX(3, 3);
        this.vue.afficherCoupO(2, 2);
        this.vue.afficherCoupX(1, 1);
    }

    public void reagirClicCase(int colonne, int rangee)
    {
        Debug.Log("colonne " + colonne + " rangee " + rangee);
        this.vue.afficherCoupX(colonne, rangee);

    }
}
