using UnityEngine;

public class ParalaxEfekat : MonoBehaviour
{
    [SerializeField] float paralaksMnozac;
    Camera kamera;
    private float duzina;
    private float pocetnaXPozicija;

    void Start()
    {
        this.kamera = GetComponentInParent<Camera>();
        this.pocetnaXPozicija = transform.position.x;
        this.duzina = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float razdaljina = this.kamera.transform.position.x * this.paralaksMnozac;
        Vector3 pozicija = this.transform.position;
        pozicija.x = this.pocetnaXPozicija + razdaljina;
        this.transform.position = pozicija;

        float temp = this.kamera.transform.position.x * (1 - this.paralaksMnozac);
        if (temp > this.pocetnaXPozicija + this.duzina)
            this.pocetnaXPozicija += this.duzina;
        else if (temp < this.pocetnaXPozicija - this.duzina)
            this.pocetnaXPozicija -= this.duzina;
    }
}