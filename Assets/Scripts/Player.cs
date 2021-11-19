using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Animations, Movement, Cam
    public Animator anim;
    public Rigidbody2D rb2d;
    public Joystick joystick;
    public Transform cam;
    public AnimationClip[] clipsAnim;

    //SpawnArea
    public Transform spawnPoint;

    //move, timeAnimAtk
    public float speedMove = 3, moveX;

    //Orientação para qual lado o player vira/olha
    public bool mirrored = true;


    //Magias para spawnar
    public float animDuration, delayAnim, delayMagiaEsferaDagua = 5;
    [SerializeField]float valueDelayMagiaEsferaDagua; //Essa var recebe os valores de delay
    public GameObject magiaEsferaDagua;


    //Btn ativado/desativado
    public Button btnMagic;



    private void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        //Passamos para clipsAnim todas as animações do player(obj)
        clipsAnim = anim.runtimeAnimatorController.animationClips;
        valueDelayMagiaEsferaDagua = delayMagiaEsferaDagua;
    }

    private void FixedUpdate(){
        //Camera
        cam.transform.position = new Vector3(transform.position.x, transform.position.y + 1, -10f);
        animDelayControll();
    }



    #region Controle Delay Anim ATK
    void animDelayControll()
    {

        if (!btnMagic.interactable)
        {
            valueDelayMagiaEsferaDagua -= Time.deltaTime;
            if(valueDelayMagiaEsferaDagua <= 0)
            {
                btnMagic.interactable = true;
                valueDelayMagiaEsferaDagua = delayMagiaEsferaDagua;
            }
        }


        //Começar o processo de delay sempre que um atk é realizado
        if (animDuration > 0)
        {
            animDuration -= Time.deltaTime;
            setValuesMagicStatus("isAtk", true);
        } 
        else if(animDuration < 0)
        {
            setValuesMagicStatus("isAtk", false);
        }

        //Quando o paramentro isAtk das anim é false, libera os controles de movimentação
        if (!anim.GetBool("isAtk")) Move();

    }
    void setValuesMagicStatus(string parAnim, bool valueParAnim)
    {
        anim.SetBool(parAnim, valueParAnim);
    }
    #endregion


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



    #region mobileAtk
    //Define isAtk como verdadeiro, qual atk é executado, e por quanto tempo isAtk ficara como true
    public void MobileAtk(){
        if (btnMagic.interactable)
        {
            btnMagic.interactable = false;

            anim.Play("mEsferaDagua");
            AnimClipTime();
            Invoke("mEsferaDagua", .1f);
         }
    }

    void mEsferaDagua()
    {
        Instantiate(magiaEsferaDagua, spawnPoint.position, Quaternion.identity);
    }

    //Definindo o valor do delay de acordo com a magia acionada
    void AnimClipTime()
    {
        //pegamos o valor de duração das anims de atk e passamos para as var de duração
        foreach (AnimationClip clip in clipsAnim)
        {
            switch (clip.name)
            {
                case "mEsferaDagua":
                    animDuration = clip.length;
                    break;
            }
        }
    }
    #endregion
}
