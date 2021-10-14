using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    //Animations, Movement, Cam
    public Animator anim;
    public Rigidbody2D rb2d;
    public Joystick joystick;
    public Transform cam;

    //SpawnArea
    public Transform spawnPoint;

    //move, timeAnimAtk
    public float speedMove = 3, moveX, magicA1Duration, delayAtk;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        AnimClipTime();
    }
    void AnimClipTime()
    {
      //Passamos para clipsAnim todas as animações do player(obj)
      AnimationClip[] clipsAnim = anim.runtimeAnimatorController.animationClips;
        //pegamos o valor de duração das anims de atk e passamos para as var de duração
        foreach(AnimationClip clip in clipsAnim)
        {
            switch (clip.name)
            {
                case "magicA1":
                    magicA1Duration = clip.length;
                break;
            }
        }
    }

    private void FixedUpdate(){
        //Camera
        cam.transform.position = new Vector3(transform.position.x, transform.position.y + 1, -10f);

        //Quando o delay do atk finalizar, muda o paramento isAtk para false
        if (delayAtk > 0) delayAtk -= Time.deltaTime;
        else anim.SetBool("isAtk", false);
        
        //Quando o paramentro isAtk das anim é false, libera os controles de movimentação
        if(!anim.GetBool("isAtk")) Move();

        //Combo utilizando o teclado
        KeyboardAtk();
    }

    void Move()
    {
        
        moveX = joystick.Horizontal; //Mobile
        float movePC = Input.GetAxisRaw("Horizontal"); //Keyboard
        
        if (moveX > 0 || movePC > 0)
        {
            anim.SetBool("isRuning", true);
            transform.eulerAngles = new Vector3(0, 0, 0);
            transform.position += new Vector3(2 * speedMove, 0, 0) * Time.deltaTime;
        }
        else if(moveX < 0 || movePC < 0)
        {

            anim.SetBool("isRuning", true);
            transform.eulerAngles = new Vector3(0, 180, 0);
            transform.position += new Vector3(-2 * speedMove, 0, 0) * Time.deltaTime;
        }
        else
        {
            anim.SetBool("isRuning", false);
        }
    }

    #region mobileAtk
    //Define isAtk como verdadeiro, qual atk é executado, e por quanto tempo isAtk ficara como true
    public void MobileAtk(){
        if (!anim.GetBool("isAtk"))
        {
            anim.Play("magicA1");
            anim.SetBool("isAtk", true);
            delayAtk = magicA1Duration;
            //Invoke("HitBox", 0.1f);
        }
    }
    #endregion

    #region pcAtk
    void KeyboardAtk()
    {
        if (!anim.GetBool("isAtk") && Input.GetKeyDown(KeyCode.Space))
        {
            anim.Play("magicA1");
            anim.SetBool("isAtk", true);
            delayAtk = magicA1Duration;
            //Invoke("HitBox", 0.1f);
        }
    }
    #endregion

}
