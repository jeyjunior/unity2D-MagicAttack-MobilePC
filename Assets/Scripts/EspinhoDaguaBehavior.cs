using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspinhoDaguaBehavior : MonoBehaviour
{

    public GameObject[] spikesBase = new GameObject[5];
    public int spikeSpawned = 0;
    public float delayValue = 0.2f;
    public float delay;
    public float delayDestroy = 1f;

    public Vector3 postAdd;
    public float postX = 0;

    private void Start()
    {
        delay = delayValue;
        for(int i = 0; i < spikesBase.Length; i++)
        {
            spikesBase[i].SetActive(false);
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
