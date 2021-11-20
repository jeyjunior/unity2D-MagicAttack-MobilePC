using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsferaDaguaBehavior : MonoBehaviour
{
    public PlayerControll playerControll;
    public float speed = 6f;

    //Splash FX
    public GameObject esferaDaguaSplash;


    void Start()
    {
        Instantiate(esferaDaguaSplash, transform.position, Quaternion.identity); //FX no inicio da magia
        playerControll = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControll>();
    }
    
    void FixedUpdate()
    {
        if (playerControll.mirrored)
        {
            transform.position += new Vector3(2 * speed, 0, 0) * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(-2 * speed, 0, 0) * Time.deltaTime;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Alvo"))
        {
            Destroy(this.gameObject);
            Instantiate(esferaDaguaSplash, transform.position, Quaternion.identity);
        }
    }
}
