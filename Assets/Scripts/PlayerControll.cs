using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControll : MonoBehaviour
{
    //Animations, Movement, Cam
    public Animator anim;
    public Rigidbody2D rb2d;
    public Joystick joystick;

    public Transform cam;
    public float camAxisY = -0.7f;

    public AnimationClip[] clipsAnim;

    //SpawnArea
    public Transform spawnPointFxSpawn;
    public Transform spawnPointHand;
    public Transform spawnPointEspinhos;

    //move
    public float speedMove = 3, moveX;

    //Orientação para qual lado o player vira/olha
    public bool mirrored = true;






    private void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        //Passamos para clipsAnim todas as animações do player(obj)
        clipsAnim = anim.runtimeAnimatorController.animationClips;
 
    }

    private void FixedUpdate(){
        //Camera
        cam.transform.position = new Vector3(transform.position.x, transform.position.y + camAxisY, -10f);
        if (!anim.GetBool("isAtk")) Move();
    }



   


    void Move()
    {
        
        moveX = joystick.Horizontal; //Mobile
        float movePC = Input.GetAxisRaw("Horizontal"); //Keyboard
        
        if (moveX > 0 || movePC > 0)
        {
            anim.SetBool("isRuning", true); mirrored = true;
            transform.position += new Vector3(2 * speedMove, 0, 0) * Time.deltaTime;
        }
        else if(moveX < 0 || movePC < 0)
        {
            anim.SetBool("isRuning", true); mirrored = false;
            transform.position += new Vector3(-2 * speedMove, 0, 0) * Time.deltaTime;
        }
        else
        {
            anim.SetBool("isRuning", false);
        }

        //Orientação para qual lado o player olha
        transform.eulerAngles = mirrored ? new Vector3(0, 0, 0) : new Vector3(0, 180, 0);
    }


}
