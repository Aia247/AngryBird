using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{

    public float maxSpeed = 10;
    public float minSpeed = 5;
    private SpriteRenderer render;
    public Sprite pigHurt;
    public GameObject Boom;
    public GameObject pigScore;

    public bool isPig = false;

    public AudioClip pigCollision, pigDead;




    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // print(collision.relativeVelocity.magnitude);
        if(collision.relativeVelocity.magnitude > maxSpeed) // À¿Õˆ
        {
            Dead();

        }else if(collision.relativeVelocity.magnitude > minSpeed) //  ‹…À
        {
            Hurt();
        }
    }

    void Dead()
    {
        if (isPig)
        {
            AudioPlay(pigDead); // 
            GameManager._instance.Pigs.Remove(this);
        }
        Destroy(gameObject);
        Instantiate(Boom, transform.position, Quaternion.identity);
        GameObject scorePic = Instantiate(pigScore, transform.position + new Vector3(0, 0.7f, 0), Quaternion.identity);
        Destroy(scorePic, 1.5f);
    }

    void Hurt()
    {
        AudioPlay(pigCollision);
        render.sprite = pigHurt;
    }

    // ≤•∑≈“Ù¿÷
    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
