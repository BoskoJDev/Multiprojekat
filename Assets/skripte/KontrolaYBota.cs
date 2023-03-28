using UnityEngine;

public class KontrolaYBota : MonoBehaviour
{
    private Animator animator;
    private int skacehash;
    private int hodahash;

    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.skacehash = Animator.StringToHash("skace");
        this.hodahash = Animator.StringToHash("brzinaHodanja");
    }

    void Update()
    {
        this.animator.SetBool(this.skacehash, Input.GetKey(KeyCode.Space));
        this.animator.SetFloat(this.hodahash, Input.GetAxis("Vertical"));
    }
}