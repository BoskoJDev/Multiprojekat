using UnityEngine;
using System.Collections.Generic;

public class Test : MonoBehaviour
{
    private Transform pocetak, kraj;
    public Cvor PocetniCvor { get; set; }
    public Cvor CiljniCvor { get; set; }

    public List<Cvor> cvoroviPutanje; // Putanja nadjena pomocu A* algoritma

    GameObject objekatPocetnogCvora, objekatKrajnjegCvora;

    private float protekloVreme = 0.0f;
    public float interval = 1.0f; //Interval izmedju pronalazenja putanje

    void Update()
    {
        protekloVreme += Time.deltaTime;
        if (protekloVreme <= interval)
            return;

        protekloVreme = 0.0f;
        PronadjiPut();
    }

    public void PronadjiPut()
    {
        if (objekatPocetnogCvora == null && objekatKrajnjegCvora == null)
        {
            objekatPocetnogCvora = GameObject.FindGameObjectWithTag("Start");
            objekatKrajnjegCvora = GameObject.FindGameObjectWithTag("End");
        }

        pocetak = objekatPocetnogCvora.transform;
        kraj = objekatKrajnjegCvora.transform;

        var (pocetnaKolona, pocetniRed) = Mreza.Instanca.MrezneKoordinate(pocetak.position);
        var (ciljnaKolona, ciljniRed) = Mreza.Instanca.MrezneKoordinate(kraj.position);
        PocetniCvor = new Cvor(Mreza.Instanca.CentarPolja(pocetnaKolona, pocetniRed));
        CiljniCvor = new Cvor(Mreza.Instanca.CentarPolja(ciljnaKolona, ciljniRed));

        cvoroviPutanje = new ZvezdaA().NadjiPut(PocetniCvor, CiljniCvor);
    }

    void OnDrawGizmos()
    {
        if (cvoroviPutanje == null || cvoroviPutanje.Count == 0)
            return;

        int indeks = 1;
        foreach (Cvor cvor in cvoroviPutanje)
        {
            if (indeks == cvoroviPutanje.Count)
                break;

            Debug.DrawLine(cvor.pozicija, cvoroviPutanje[indeks++].pozicija, Color.green);
        }
    }
}