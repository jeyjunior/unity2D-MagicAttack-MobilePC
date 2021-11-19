using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBehavior : MonoBehaviour
{
    public Player player;
    public float speed = 5f;
    Vector3 dir;

    Rigidbody2D rb2d;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rb2d = GetComponent<Rigidbody2D>();
        dir = !player.mirrored ? new Vector3(-speed * Time.deltaTime, 0, 0) : new Vector3(speed * Time.deltaTime, 0, 0);
    }
    
    void FixedUpdate()
    {
        if (player.mirrored)
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
        }
    }
}
