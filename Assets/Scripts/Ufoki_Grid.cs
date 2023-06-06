//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ufoki_Grid : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    // void Update()
    //{

    //}


    public Ufok[] prefabs;
    public int rows = 5;
    public int columns = 11;
    private Vector3 _direction = Vector3.right;
    public AnimationCurve speed; //szybkosc w zaleznosci od ilosci zabitych invaderow
    public Projectile missilePrefab; //rakieta
    public float missileAttackRate = 1.0f; //szybkosc spawnowania rakiet

    public int carnage { get; private set; } //ilosc zabitych invaderow
    public int current => this.rows * this.columns; //kompletna ilosc zyjacych invaderow
    public float percentDed => (float)this.carnage / (float)this.current; //procent zabitych invaderow
    public int alive => this.current - this.carnage; //ilosc zywych invaderow

    private void Awake()
    {
        for(int i=0; i<this.rows; i++) //kod na rozmieszczenie Invaders na planszy
        {
            float width = 2.0f * (this.columns - 1);
            float height = 2.0f * (this.rows - 1);
            Vector3 centering = new Vector2(-width/2, -height/2);
            Vector3 RowPosition=new Vector3(centering.x, centering.y+(i*2.0f), 0.0f);
            for (int j = 0; j < this.columns; j++)
            {
                Ufok invader=Instantiate(this.prefabs[i], this.transform);
                invader.unalif += UfokDed; //odwolanie do zabica pojedynczego invadera (Invoke) i spowodowanie ze "zginie"
                Vector3 position = RowPosition;
                position.x += j * 2.0f;
                invader.transform.localPosition = position;

            }
        }
    }

    private void Start() //funkcja do spawnowania rakiet
    {
        InvokeRepeating(nameof(MissileAttack), this.missileAttackRate, this.missileAttackRate);
    }

    private void Update()//ruch Invaderow
    {
        this.transform.position += _direction * this.speed.Evaluate(this.percentDed) * Time.deltaTime;

        Vector3 LEdge = Camera.main.ViewportToWorldPoint(Vector3.zero); //lewa krawedz, ustalana przez wbudowane funkcje Unity
        Vector3 REdge = Camera.main.ViewportToWorldPoint(Vector3.right); //to samo po prawej

        foreach (Transform invader in this.transform) //ruch lewo i prawo po dotknieciu ostatniego Invadera krawedzi pocznej planszy
        {
            if(!invader.gameObject.activeInHierarchy)//jesli Invader nie zyje
            {
                continue;
            }

            if(_direction==Vector3.right && invader.position.x>=(REdge.x-1.0f))
            {
                NextRow();
            }
            else if (_direction == Vector3.left && invader.position.x <= (LEdge.x+1.0f))
            {
                NextRow();
            }
        }
    }

    private void NextRow() //w dol po dotknieciu brzegu ekranu
    {
        _direction.x *= -1.0f;

        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;
    }

    private void MissileAttack() //szansa na spawnowaie rakiety
    {
        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)//jesli Invader nie zyje
            {
                continue;
            }

            if (Random.value < (1.0f / (float)this.alive)) //szansa na spawn
            {
                Instantiate(this.missilePrefab, invader.position, Quaternion.identity);
                //jak dla lasera
            }
        }
    }

    public GameObject complete;

    private void UfokDed()
    {
        this.carnage++;

        if (this.carnage>=this.current)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name); //przeladowanie sceny jesli wszyscy zabici
            PlayGame();
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
