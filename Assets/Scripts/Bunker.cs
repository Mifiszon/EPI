using UnityEngine;

public class Bunker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)//niszczenie bunkrow
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ufok"))//jesli invaderzy ich dosiegna
        {
            this.gameObject.SetActive(false);
        }
    }
}
