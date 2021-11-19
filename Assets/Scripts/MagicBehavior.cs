using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBehavior : MonoBehaviour
{
    public PlayerControll playerControll;
    public float speed = 5f;

    void Start()
    {
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
        }
    }
}
