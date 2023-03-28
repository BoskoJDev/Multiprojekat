using UnityEngine;
using System;

public class Cvor
{
    public float cenaDoSada;
    public float fVrednost;         //Estimated cost from this node to the goal node
    public bool jePrepreka;
    public Cvor roditelj;
    public Vector3 pozicija;
    public Cvor(Vector3 pos)
    {
        fVrednost = 0.0f;
        cenaDoSada = 0.0f;
        jePrepreka = false;
        roditelj = null;
        pozicija = pos;
    }
    public void OznaciKaoPrepreku() => jePrepreka = true;

    public override bool Equals(object obj) => obj is Cvor node && pozicija.Equals(node.pozicija);

    public override int GetHashCode() => HashCode.Combine(pozicija);
}