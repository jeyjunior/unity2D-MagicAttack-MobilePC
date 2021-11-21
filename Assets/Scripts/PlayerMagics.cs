using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMagics : MonoBehaviour
{
    //PlayerControll class
    PlayerControll playerControll;

    //Btn ativado/desativado
    public Button btnEsfera, btnEspinhos;

    public Text txtBtnEsfera, txtBtnEspinhos;

    //Magia Esfera Dagua
    public float animDuration, delayAnim, delayMagiaEsferaDagua = 2;
    [SerializeField] float valueDelayMagiaEsferaDagua; //Essa var recebe os valores de delay
    public GameObject magiaEsferaDagua;


    //Magia Espinhos
    public float delayMagiaEspinhoDagua = 5f;
    [SerializeField] float valueDelayMagiaEspinhosDagua;
    public GameObject magiaEspinhosDagua;


    //FX Spawns
    public GameObject fxSpawn;

    private void Start()
    {
        playerControll = GetComponent<PlayerControll>();

        //Esfera dagua delay
        valueDelayMagiaEsferaDagua = delayMagiaEsferaDagua;
        btnEsfera.interactable = false;
        txtBtnEsfera.enabled = false;

        //Espinhos dagua delay
        valueDelayMagiaEspinhosDagua = delayMagiaEspinhoDagua;
        btnEspinhos.interactable = false;
        txtBtnEspinhos.enabled = false;
    }

    private void Update()
    {
        animDelayControll();


    }


    #region Controle delay da magia
    void animDelayControll()
    {

        //Botao da magia fica ativado ou desativado de acordo com o delay apos castar a magia
        #region Delay magias

        //DELAY ESFERA DAGUA
        if (!btnEsfera.interactable)
        {
            valueDelayMagiaEsferaDagua -= Time.deltaTime;

            txtBtnEsfera.enabled = true;
            txtBtnEsfera.text = valueDelayMagiaEsferaDagua.ToString("F2");


            if (valueDelayMagiaEsferaDagua <= 0)
            {
                btnEsfera.interactable = true;
                txtBtnEsfera.enabled = false;
                valueDelayMagiaEsferaDagua = delayMagiaEsferaDagua;
            }
        }


        //DELAY ESPINHOS DAGUA
        if (!btnEspinhos.interactable)
        {
            valueDelayMagiaEspinhosDagua -= Time.deltaTime;

            txtBtnEspinhos.enabled = true;
            txtBtnEspinhos.text = valueDelayMagiaEspinhosDagua.ToString("F2");

            if(valueDelayMagiaEspinhosDagua <= 0)
            {
                btnEspinhos.interactable = true;
                txtBtnEspinhos.enabled = false;
                valueDelayMagiaEspinhosDagua = delayMagiaEspinhoDagua;
            }
        }

        #endregion


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



    #region Cast Magias
    //Define isAtk como verdadeiro, qual atk é executado, e por quanto tempo isAtk ficara como true
    public void SpawnEsferaDagua()
    {
        if (btnEsfera.interactable)
        {
            InstantiateObjects("magiaEsferaDagua");
        }
    }

    public void SpawnEspinhosDagua()
    {
        if (btnEspinhos.interactable)
        {
            InstantiateObjects("magiaEspinhosDagua");
        }
    }



    //Spawn Magia
    void InstantiateObjects(string gameObject)
    {
        playerControll.anim.Play("spawn");

        //SpawnFX
        Instantiate(fxSpawn, playerControll.spawnPointFxSpawn.transform.position, Quaternion.identity);

        //Delay para voltar a andar
        animDuration = 0.3f;

        //Spawn Magias
        if (gameObject == "magiaEsferaDagua") 
        {
            btnEsfera.interactable = false;
            Instantiate(magiaEsferaDagua, playerControll.spawnPointHand.position, Quaternion.identity); 
        }
        else if (gameObject == "magiaEspinhosDagua") 
        {
            btnEspinhos.interactable = false;
            Instantiate(magiaEspinhosDagua, playerControll.spawnPointEspinhos.position, Quaternion.identity); 
        }
    }

    #endregion



}
