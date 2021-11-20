using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMagics : MonoBehaviour
{
    //PlayerControll class
    PlayerControll playerControll;

    //Btn ativado/desativado
    public Button btnMagic;
    public Text txtBtnMagic;

    //Magia Esfera Dagua
    public float animDuration, delayAnim, delayMagiaEsferaDagua = 2;
    [SerializeField] float valueDelayMagiaEsferaDagua; //Essa var recebe os valores de delay
    public GameObject magiaEsferaDagua;




    private void Start()
    {
        playerControll = GetComponent<PlayerControll>();
        valueDelayMagiaEsferaDagua = delayMagiaEsferaDagua;
        btnMagic.interactable = false;
        txtBtnMagic.enabled = false;
    }

    private void Update()
    {
        animDelayControll();

    }


    #region Controle delay da magia
    void animDelayControll()
    {

        //Botao da magia esfera dagua fica ativado ou desativado de acordo com o delay apos castar a magia
        if (!btnMagic.interactable)
        {
            valueDelayMagiaEsferaDagua -= Time.deltaTime;
            
            txtBtnMagic.enabled = true;
            txtBtnMagic.text = valueDelayMagiaEsferaDagua.ToString("F2");


            if (valueDelayMagiaEsferaDagua <= 0)
            {
                btnMagic.interactable = true;
                txtBtnMagic.enabled = false;
                valueDelayMagiaEsferaDagua = delayMagiaEsferaDagua;
            }
        }



        //Começar o processo de delay sempre que um atk é realizado
        if (animDuration > 0)
        {
            animDuration -= Time.deltaTime;
            setValuesMagicStatus("isAtk", true);
        }
        else if (animDuration < 0)
        {
            setValuesMagicStatus("isAtk", false);
        }
    }
    void setValuesMagicStatus(string parAnim, bool valueParAnim)
    {
       playerControll.anim.SetBool(parAnim, valueParAnim);
    }
    #endregion

    #region Controle uso da magia com um botão
    //Define isAtk como verdadeiro, qual atk é executado, e por quanto tempo isAtk ficara como true
    public void EsferaDaguaAtk()
    {
        if (btnMagic.interactable)
        {
            btnMagic.interactable = false;

            playerControll.anim.Play("mEsferaDagua");
            AnimClipTime();
            Invoke("mEsferaDagua", .1f);
        }
    }

    void mEsferaDagua()
    {
        Instantiate(magiaEsferaDagua, playerControll.spawnPoint.position, Quaternion.identity);
    }

    //Definindo o valor do delay de acordo com a magia acionada
    void AnimClipTime()
    {
        //pegamos o valor de duração das anims de atk e passamos para as var de duração
        foreach (AnimationClip clip in playerControll.clipsAnim)
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
