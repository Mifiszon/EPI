using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction; 

    public float speed;

    public System.Action boom; //oznajmia o zniszczeniu rakiety

    //speed i direction odwrotne dla rakiety i lasera

    private void Update()
    {
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) //przy kolizji z obiektem
    {
        if (this.boom!=null) //glitche bez warunku
        {
            this.boom.Invoke(); //jesli rakieta/laser powinien byc zniszczony
        }
        Destroy(this.gameObject); //niszczy laser/rakiete przy kolizji
    }
}
