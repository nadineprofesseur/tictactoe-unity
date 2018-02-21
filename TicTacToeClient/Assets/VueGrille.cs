using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class VueGrille : MonoBehaviour, IPointerClickHandler
{

    protected ControleurGrille controleur;
    protected IPanel grillage;
    //protected Text prototypeSymboleX;
    //protected Text prototypeSymboleO;
    protected Text[,] coupsDejaJoues;
    UnityEngine.UI.Button case11;
    UnityEngine.UI.Button[,] cases;

    /*public class VueCase : UnityEngine.UI.Button
    {
    }*/

    // Use this for initialization
    void Start()
    {
        this.controleur = new ControleurGrille(this);

        this.grillage = GameObject.Find("grillage").GetComponent<IPanel>();
        //this.prototypeSymboleX = GameObject.Find("symbole-x").GetComponent<Text>();
        //this.prototypeSymboleO = GameObject.Find("symbole-o").GetComponent<Text>();
        this.coupsDejaJoues = new Text[3, 3];

        this.cases = new UnityEngine.UI.Button[3, 3];
        this.cases[0, 0] = GameObject.Find("case-1-1").GetComponent<UnityEngine.UI.Button>();
        this.cases[0, 0].onClick.AddListener(() => { clicCase(1, 1); });
        this.cases[1, 0] = GameObject.Find("case-2-1").GetComponent<UnityEngine.UI.Button>();
        this.cases[1, 0].onClick.AddListener(() => { clicCase(2, 1); });
        this.cases[2, 0] = GameObject.Find("case-3-1").GetComponent<UnityEngine.UI.Button>();
        this.cases[2, 0].onClick.AddListener(() => { clicCase(3, 1); });

        this.cases[0, 1] = GameObject.Find("case-1-2").GetComponent<UnityEngine.UI.Button>();
        this.cases[0, 1].onClick.AddListener(() => { clicCase(1, 2); });
        this.cases[1, 1] = GameObject.Find("case-2-2").GetComponent<UnityEngine.UI.Button>();
        this.cases[1, 1].onClick.AddListener(() => { clicCase(2, 2); });
        this.cases[2, 1] = GameObject.Find("case-3-2").GetComponent<UnityEngine.UI.Button>();
        this.cases[2, 1].onClick.AddListener(() => { clicCase(3, 2); });

        this.cases[0, 2] = GameObject.Find("case-1-3").GetComponent<UnityEngine.UI.Button>();
        this.cases[0, 2].onClick.AddListener(() => { clicCase(1, 3); });
        this.cases[1, 2] = GameObject.Find("case-2-3").GetComponent<UnityEngine.UI.Button>();
        this.cases[1, 2].onClick.AddListener(() => { clicCase(2, 3); });
        this.cases[2, 2] = GameObject.Find("case-3-3").GetComponent<UnityEngine.UI.Button>();
        this.cases[2, 2].onClick.AddListener(() => { clicCase(3, 3); });


        this.controleur.tester();
    }

    void clicCase(int colonne, int rangee)
    {
        this.controleur.reagirClicCase(colonne, rangee);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void afficherNomO(string nom)
    {
        Text nomO = GameObject.Find("nom-o").GetComponent<Text>();
        nomO.text = nom;
    }

    public void afficherNomX(string nom)
    {
        Text nomX = GameObject.Find("nom-x").GetComponent<Text>();
        nomX.text = nom;
    }

    protected int DECALAGE_X = 450;
    protected int DECALAGE_Y = 500;
    protected int LARGEUR = 150;
    protected int HAUTEUR = 200;

    protected void afficherTexteDansCase(int rangee, int colonne, string texte)
    {
        if (rangee < 1 || rangee > 3) return;
        if (colonne < 1 || colonne > 3) return;
        rangee = rangee - 1;
        colonne = colonne - 1;
        this.cases[rangee, colonne].GetComponentsInChildren<Text>()[0].text = texte;
    }

    public void afficherCoupX(int rangee, int colonne)
    {
        this.afficherTexteDansCase(rangee, colonne, "x");

        //this.coupsDejaJoues[rangee, colonne] = symboleX;
    }

    public void afficherCoupO(int rangee, int colonne)
    {
        this.afficherTexteDansCase(rangee, colonne, "o");

        //this.coupsDejaJoues[rangee, colonne] = symboleO;
    }
    public void OnPointerClick(PointerEventData eventData) { }
    /*
     public void afficherCoupX(int rangee, int colonne)
    {
        if (rangee < 1 || rangee > 3) return;
        if (colonne < 1 || colonne > 3) return;
        rangee = rangee - 1;
        colonne = colonne - 1;

        //this.prototypeSymboleX.MemberwiseClone(); // Il faut etre dans l'objet
        Text symboleX = Instantiate(this.prototypeSymboleX);
        symboleX.rectTransform.parent = this.transform;
        symboleX.transform.position = new Vector2(DECALAGE_X + LARGEUR * colonne, DECALAGE_Y - HAUTEUR * colonne);
        this.coupsDejaJoues[rangee, colonne] = symboleX;
    }

    public void afficherCoupO(int rangee, int colonne)
    {
        if (rangee < 1 || rangee > 3) return;
        if (colonne < 1 || colonne > 3) return;
        rangee = rangee - 1;
        colonne = colonne - 1;

        //Text symboleO = Instantiate(this.prototypeSymboleO, this.transform) as Text;
        Text symboleO = Instantiate(this.prototypeSymboleO);
        symboleO.rectTransform.parent = this.transform;
        symboleO.transform.position = new Vector2(DECALAGE_X + LARGEUR * colonne, DECALAGE_Y - HAUTEUR * colonne);
        this.coupsDejaJoues[rangee, colonne] = symboleO;
    } 
    public Vector2 convertirClicEnCase(Vector2 positionClic)
    {
        Debug.Log("Position " + positionClic.x + " position " + positionClic.y);
        positionClic.x = positionClic.x - DECALAGE_X;
        positionClic.y = -(positionClic.y - DECALAGE_Y);
        positionClic.x = positionClic.x / (LARGEUR / 1.7f);
        positionClic.y = positionClic.y / (HAUTEUR / 1.7f);
        return positionClic;
    }

    public void OnPointerClick(PointerEventData eventData) // 3
    {
        print("Click");
        Vector2 positionClic = convertirClicEnCase(eventData.position);
        controleur.reagirClicCase((int)positionClic.x, (int)positionClic.y);
    }

    void OnMouseDown()
    {
        Debug.Log("clic");
    }
    */

}
