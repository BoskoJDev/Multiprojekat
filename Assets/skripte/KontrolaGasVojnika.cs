using UnityEngine;

public class KontrolaGasVojnika : MonoBehaviour
{
    [SerializeField] bool koristi1DBlendTree;
    [SerializeField] float ubrzanje = 0.1f;
    [SerializeField] float usporavanje = 0.5f;
    private float brzina = 0.0f;
    private float brzinaX = 0.0f;
    private float brzinaZ = 0.0f;
    private Animator animator;
    private int brzinaHash;
    private int brzinaXHash;
    private int brzinaZHash;
    private int cuciHash;

    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.brzinaHash = Animator.StringToHash("brzina");
        this.brzinaXHash = Animator.StringToHash("brzinaX");
        this.brzinaZHash = Animator.StringToHash("brzinaZ");
        this.cuciHash = Animator.StringToHash("cuci");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
            this.animator.SetBool(this.cuciHash, !this.animator.GetBool(this.cuciHash));

        if (this.koristi1DBlendTree)
        {
            this.Unos(KeyCode.W, ref this.brzina);
            this.animator.SetFloat(this.brzinaHash, this.brzina);
            return;
        }

        this.Unos(KeyCode.A, ref this.brzinaX);
        this.Unos(KeyCode.D, ref this.brzinaZ);

        this.animator.SetFloat(this.brzinaXHash, this.brzinaX);
        this.animator.SetFloat(this.brzinaZHash, this.brzinaZ * -1);
    }

    void Unos(KeyCode taster, ref float promenljiva)
    {
        if (Input.GetKey(taster))
            promenljiva += promenljiva < 1.0f ? Time.deltaTime * this.ubrzanje : 0.0f;
        else
            promenljiva -= promenljiva > 0.0f ? Time.deltaTime * this.usporavanje : 0.0f;
    }
}