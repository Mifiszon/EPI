//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Ufok : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public Sprite[] SpriteAnimation; //publiczna (edytowalna w edytorze) tablica sprite'ow
    public float AnimationTime = 1.2f; //czas trwania animacji

    public System.Action unalif; //zabicie invadera

    private SpriteRenderer _Renderer; //do renderowania sprite'ow
    private int _CurrentFrame; //aktualna klatka animacji (element tablicy)

    private void Awake()
    {
        _Renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()//poczatek gry
    {
        InvokeRepeating(nameof(AnimateSprite), this.AnimationTime, this.AnimationTime);//cykl animacji
    }

    private void AnimateSprite()//do animacji
    {
        _CurrentFrame++;

        if(_CurrentFrame>=this.SpriteAnimation.Length)
        {
            _CurrentFrame=0;
        }

        _Renderer.sprite = this.SpriteAnimation[_CurrentFrame];
    }

    private void OnTriggerEnter2D(Collider2D dif) //kolizja z innym obiektem
    {
        if (dif.gameObject.layer == LayerMask.NameToLayer("Laser"))//jesli kolizja z warstwa na ktorej jest laser
        {
            this.unalif.Invoke();//zarejestrowanie smierci invadera
            this.gameObject.SetActive(false);//invader staje sie nieaktywny

        }
    }
}
