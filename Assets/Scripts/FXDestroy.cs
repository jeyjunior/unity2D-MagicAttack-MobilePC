using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXDestroy : MonoBehaviour
{
    float delayAnim;
    public Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
        delayAnim = anim.runtimeAnimatorController.animationClips.Length; // anim duration time
    }

    private void Update()
    {
        Invoke("DestroyFX", delayAnim);
    }

    void DestroyFX()
    {
        Destroy(this.gameObject);
    }

}
