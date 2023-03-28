using UnityEngine;

public class KameraPrati : MonoBehaviour
{
    [SerializeField] float brzinaPracenja = 1.0f;
    [SerializeField] float yOfset = 1.0f;
    [SerializeField] Transform igrac;

    void Update()
    {
        Vector3 novaPozicija = new(this.igrac.position.x, this.igrac.position.y + this.yOfset, -10f);
        this.transform.position = Vector3.Slerp(this.transform.position, novaPozicija,
            this.brzinaPracenja * Time.deltaTime);
    }
}