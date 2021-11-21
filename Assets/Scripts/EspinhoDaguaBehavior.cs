using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspinhoDaguaBehavior : MonoBehaviour
{
    public PlayerControll playerControll;

    //FX Spawn - FX Collision
    public GameObject fxCollision;

    //Espinhos obj
    public GameObject[] spikesBase = new GameObject[5];

    //Controle quantos spikes foram "habilitados"
    public int spikeSpawned = 0;

    //delayValue, valor base para delay
    //delay, tempo para habilitar proximo espinho,
    //delayDestroy, tempo para destruir este obj 
    public float delayValue = 0.2f, delay, delayDestroy = 1f;


    private void Start()
    {
        playerControll = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControll>();

        delay = delayValue;
        for(int i = 0; i < spikesBase.Length; i++)
        {
            spikesBase[i].SetActive(false);
        }
        
        //Se player olhar para direita, espinhos são spawnados para direita
        if (!playerControll.mirrored)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

    }


    private void Update()
    {

        if (spikeSpawned < spikesBase.Length)
        {
            if (delay <= 0)
            {
                spikesBase[spikeSpawned].SetActive(true);
                spikeSpawned++;
                delay = delayValue;
            }
            else
            {
                delay -= Time.deltaTime;
            }
        }

        if(spikeSpawned >= spikesBase.Length)
        {
            delayDestroy -= Time.deltaTime;
            if (delayDestroy <= 0) Destroy(this.gameObject);
        }
    }

}
