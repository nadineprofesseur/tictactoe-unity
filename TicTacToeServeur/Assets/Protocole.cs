using System;

public class Protocole
{
    // https://docs.unity3d.com/Manual/JSONSerialization.html
    [System.Serializable]
    public class Coup
    {
        public string symbole;
        public int colonne;
        public int rangee;
    };
}
