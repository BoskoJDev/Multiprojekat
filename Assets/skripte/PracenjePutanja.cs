using UnityEngine;

public class PracenjePutanja : MonoBehaviour
{
    public Test test;
    public float brzina = 10.0f;

    [Range(1.0f, 1000.0f)]
    public float inercija = 100.0f;
    public float poluprecnijPutneTacke = 0.1f;

    public bool vrtiSeUKrug = true;

    private float trenutnaBrzina;

    private int indeksTrenutneTacke = 0;
    private float duzinaPutanje;
    private Vector3 krajnaTacka;

    Vector3 vektorBrzine;

    void Start()
    {
        test.PronadjiPut();
        duzinaPutanje = test.cvoroviPutanje.Count;
        vektorBrzine = transform.forward;
    }

    void Update()
    {
        trenutnaBrzina = brzina * Time.deltaTime;
        krajnaTacka = test.cvoroviPutanje[indeksTrenutneTacke].pozicija;

        // Predji na sledecu putnu tacku kada se dovoljno priblizi trenutnoj
        if (Vector3.Distance(transform.position, krajnaTacka) < poluprecnijPutneTacke)
        {
            if (indeksTrenutneTacke < duzinaPutanje - 1 || test.cvoroviPutanje.Count > 0)
                test.cvoroviPutanje.RemoveAt(indeksTrenutneTacke);
            else if (vrtiSeUKrug)
                indeksTrenutneTacke = 0;
            else
                return;
        }

        if (indeksTrenutneTacke >= duzinaPutanje)
            return;

        if (indeksTrenutneTacke >= duzinaPutanje - 1 && !vrtiSeUKrug)
            vektorBrzine += RacunajBrzinu(krajnaTacka, true);
        else
            vektorBrzine += RacunajBrzinu(krajnaTacka);

        transform.position += vektorBrzine;
        transform.rotation = Quaternion.LookRotation(vektorBrzine);
    }

    public Vector3 RacunajBrzinu(Vector3 meta, bool jeDosaoDoKranjeTacke = false)
    {
        Vector3 trazeniVektorBrzine = (meta - transform.position);
        float magnituda = trazeniVektorBrzine.magnitude;

        trazeniVektorBrzine.Normalize();
        if (jeDosaoDoKranjeTacke && magnituda < poluprecnijPutneTacke)
            trazeniVektorBrzine *= trenutnaBrzina * (magnituda / poluprecnijPutneTacke);
        else
            trazeniVektorBrzine *= trenutnaBrzina;

        Vector3 skretanje = trazeniVektorBrzine - vektorBrzine;
        return skretanje / inercija;
    }
}