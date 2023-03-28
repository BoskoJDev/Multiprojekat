using UnityEngine;

public class Putanja : MonoBehaviour
{
    public bool jeDebug = true;
    public System.Collections.Generic.List<Cvor> putneTacke;

    public float BrojPutnihTacaka { get => putneTacke.Count; }

    public Vector3 PozicijaTacke(int index) => putneTacke[index].pozicija;

    void OnDrawGizmos()
    {
        if (!jeDebug)
            return;

        for (int i = 1; i < putneTacke.Count; i++)
            Debug.DrawLine(putneTacke[i-1].pozicija, putneTacke[i].pozicija, Color.red);
    }
}