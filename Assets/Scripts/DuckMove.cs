//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DuckMove : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}

    public Projectile MahLazor;  //projectile laser
    private bool _laserActive; //czy laser aktywny

    public float speed = 5.6f; //szybkosc ruchu gracza


    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) //ruch w lewo
        {
            this.transform.position += Vector3.left * this.speed * Time.deltaTime; //deltatime do fps
        }

        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) //w prawo
        {
            this.transform.position += Vector3.right * this.speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space)) //strzal
        {
            Pew();
        }
    }

        private void Pew()//funkcja strzalu
        {
           if (!_laserActive) //tylko 1 laser naraz
           {
               Projectile MahLazor = Instantiate(this.MahLazor, this.transform.position, Quaternion.identity);
               //Quaternion.identity do rotacji domyslnej (w tym przypadku be znaczenia)
               MahLazor.boom += LaserDestroyed; //jesli laser zniszczony wywolaj funkcje
               _laserActive = true;
           }

        }

    private void LaserDestroyed()
    {
        _laserActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)//smierc gracza
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ufok")
            || other.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

