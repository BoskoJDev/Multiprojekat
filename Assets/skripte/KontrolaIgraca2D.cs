using UnityEngine;

public class KontrolaIgraca2D : MonoBehaviour
{
    [SerializeField] float brzina = 1.0f;
    [SerializeField] float jacinaSkoka = 1.0f;
    Rigidbody2D rb;
    Animator animator;

    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        bool nijeSkocio = !this.animator.GetBool("jeSkocio");
        if (Input.GetKeyDown(KeyCode.Space) && nijeSkocio)
        {
            this.animator.SetBool("jeSkocio", true);
            this.rb.AddForce(100 * this.jacinaSkoka * Vector2.up);
        }

        float brzinaIgraca = Input.GetAxis("Horizontal") * this.brzina;
        Vector3 pozicija = this.transform.position;
        pozicija.x += brzinaIgraca * Time.deltaTime;
        this.transform.position = pozicija;
        this.animator.SetFloat("brzina", nijeSkocio ? Mathf.Abs(brzinaIgraca) : 0.0f);

        Quaternion rotacija = this.transform.rotation;
        rotacija.y = brzinaIgraca < 0.0f ? 90.0f : 0.0f;
        this.transform.rotation = rotacija;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        this.animator.SetBool("jeSkocio", false);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        this.animator.SetBool("jeSkocio", true);
    }
}