using UnityEngine;
using System.Collections.Generic;

public class Mreza : MonoBehaviour
{
    private static Mreza INSTANCA = null;

    public static Mreza Instanca
    {
        get
        {
            if (INSTANCA == null)
            {
                INSTANCA = FindObjectOfType(typeof(Mreza)) as Mreza;
                if (INSTANCA == null)
                    Debug.Log("Could not locate an Mreza object. \n You have to have exactly one Mreza in the scene.");
            }
            return INSTANCA;
        }
    }

    void OnApplicationQuit() => INSTANCA = null;

    public int redovi;
    public int kolone;
    public float velicinaPolja;
    public float poluprecnikPrepreke = 0.2f;
    public bool prikaziMrezu = true;
    public bool prikaziCvorovePrepreke = true;

    public Cvor[,] MatricaCvorova { get; set; }

    public float VelicinaPolja { get => velicinaPolja; }

    void Awake()
    {
        MatricaCvorova = new Cvor[kolone, redovi];
        for (int i = 0; i < kolone; i++)
        {
            for (int j = 0; j < redovi; j++)
            {
                Vector3 pozicijaPolja = CentarPolja(i, j);
                Cvor cvor = new(pozicijaPolja);

                var kolajderi = Physics.OverlapSphere(pozicijaPolja,
                    velicinaPolja,
                    1 << LayerMask.NameToLayer("Prepreka"));
                if (kolajderi.Length != 0)
                    cvor.OznaciKaoPrepreku();
                
                MatricaCvorova[i, j] = cvor;
            }
        }
    }

    public Vector3 CentarPolja(int kolona, int red)
    {
        Vector3 pozicijaPolja = PozicijaPolja(kolona, red);
        pozicijaPolja.x += velicinaPolja / 2.0f;
        pozicijaPolja.z += velicinaPolja / 2.0f;

        return pozicijaPolja;
    }

    public Vector3 PozicijaPolja(int kolona, int red)
    {
        float xPozicija = kolona * velicinaPolja;
        float zPozicija = red * velicinaPolja;

        return transform.position + new Vector3(xPozicija, 0.0f, zPozicija);
    }

    public (int,int) MrezneKoordinate(Vector3 pozicija)
    {
        if (!NijeIzvanOkvira(pozicija))
            return (-1,-1);

        int kolona = (int)Mathf.Floor((pozicija.x-transform.position.x) / this.velicinaPolja);
        int red = (int)Mathf.Floor((pozicija.z-transform.position.z) / this.velicinaPolja);

        return (kolona, red);
    }

    public bool NijeIzvanOkvira(Vector3 pos)
    {
        float sirina = kolone * velicinaPolja;
        float duzina = redovi * velicinaPolja;

        return pos.x >= transform.position.x && pos.x <= transform.position.x + sirina &&
            pos.x <= transform.position.z + duzina && pos.z >= transform.position.z;
    }

    public bool JePoljePrelazno(int kolona, int red)
    {
        return kolona >= 0 && red >= 0 && kolona < kolone && red < redovi &&
            !MatricaCvorova[kolona, red].jePrepreka;
    }

    public List<Cvor> IzvuciKomsije(Cvor cvor)
    {
        List<Cvor> komsije = new();
        var (kolona, red) = MrezneKoordinate(cvor.pozicija);

        // Kretanje po horizontalnim i vertikalnim cvorovima
        if (JePoljePrelazno(kolona - 1, red)) 
            komsije.Add(MatricaCvorova[kolona - 1, red]);
        if (JePoljePrelazno(kolona + 1, red))
            komsije.Add(MatricaCvorova[kolona + 1, red]);
        if (JePoljePrelazno(kolona, red - 1))
            komsije.Add(MatricaCvorova[kolona, red - 1]);
        if (JePoljePrelazno(kolona, red + 1))
            komsije.Add(MatricaCvorova[kolona, red + 1]);

        //Kretanje po dijagonalnim cvorovima
        if (JePoljePrelazno(kolona - 1, red - 1))
            komsije.Add(MatricaCvorova[kolona - 1, red - 1]);
        if (JePoljePrelazno(kolona + 1, red - 1))
            komsije.Add(MatricaCvorova[kolona + 1, red - 1]);
        if (JePoljePrelazno(kolona - 1, red + 1))
            komsije.Add(MatricaCvorova[kolona - 1, red + 1]);
        if (JePoljePrelazno(kolona + 1, red + 1))
            komsije.Add(MatricaCvorova[kolona + 1, red + 1]);

        return komsije;
    }

    void OnDrawGizmos()
    {
        if (prikaziMrezu)
            IscrtavajMrezu(Color.blue);

        //Grid Start Position
        Gizmos.DrawSphere(transform.position, 0.5f);

        if (MatricaCvorova == null)
            return;

        //Draw Obstacle obstruction
        if (!prikaziCvorovePrepreke)
            return;

        Vector3 velicinaPolja = new(this.velicinaPolja, 1.0f, this.velicinaPolja);
        Gizmos.color = Color.red;
        for (int i = 0; i < kolone; i++)
        {
            for (int j = 0; j < redovi; j++)
            {
                if (MatricaCvorova != null && MatricaCvorova[i, j].jePrepreka)
                    Gizmos.DrawCube(CentarPolja(i, j), velicinaPolja);
            }
        }
    }

    public void IscrtavajMrezu(Color boja)
    {
        float sirina = kolone * velicinaPolja;
        float duzina = redovi * velicinaPolja;

        // Crtanje horizontala
        for (int i = 0; i < redovi + 1; i++)
        {
            Vector3 pocetak = transform.position + i * velicinaPolja * new Vector3(0.0f, 0.0f, 1.0f);
            Vector3 kraj = pocetak + sirina * new Vector3(1.0f, 0.0f, 0.0f);
            Debug.DrawLine(pocetak, kraj, boja);
        }

        // Crtanje vertikala
        for (int i = 0; i < kolone + 1; i++)
        {
            Vector3 pocetak = transform.position + i * velicinaPolja * new Vector3(1.0f, 0.0f, 0.0f);
            Vector3 kraj = pocetak + duzina * new Vector3(0.0f, 0.0f, 1.0f);
            Debug.DrawLine(pocetak, kraj, boja);
        }
    }
}