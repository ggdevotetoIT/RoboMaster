using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 
/// </summary>
public class PlayerRelive : MonoBehaviour
{
    public static PlayerHealth instance;
    private Gun playerGun;
    private PlayerMove playerMove;
    private PlayerHealth playerHealth;
    public Image image;
    public GameObject panel;
    public Text text;
    public Slider ExpSlider;
    public GameObject image1;
    public GameObject image2;
    public bool isSuppy;
    public GameObject Supply;
    public float supplyTimeCounter;
    public bool isBuff;
    public GameObject Buff;
    public float buffTimeCounter;
    public GameObject SupplyImage;
    public GameObject BuffImage;
    public GameObject mapCamera;
    private bool isOpenMap = true;
    public AudioClip audioClip1;
    private AudioSource audioSource;
    public AudioClip audioClip2;
    public float countTime;//计算补血时间
    public float soundCount = 1;//确保声音只播放一次
  
    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerGun = GetComponent<Gun>();
        playerMove = GetComponent<PlayerMove>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        countTime += Time.deltaTime;
        if(playerHealth.isReliveFinished)
        {
            playerGun.enabled = true;
            playerMove.enabled = true;
            playerHealth.isReliveFinished = false;
        }
        DieImage();
        if(playerHealth.HP<=0)
        {
            playerHealth.Death();
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
        supplyTimeCounter -= Time.deltaTime;
        if(supplyTimeCounter<=0f)
        {
            supplyTimeCounter = 0f;
            isSuppy = false;
        }
        buffTimeCounter -= Time.deltaTime;
        if(buffTimeCounter<=0f)
        {
            buffTimeCounter = 0f;
            isBuff = false;
            playerHealth.attackRate = 1f;
        }
        UpdateExp();
        UpdateLevelImage();
        UpdateSupply();
        UpdateBuff();
        MapCamera();
    }
    private void DieImage()
    {
        if(playerHealth.HP<=0)
        {
            image.fillAmount = 0;
        }
        if(playerHealth.isRelive)
        {
            image.fillAmount = playerHealth.reliveTimeCounter / playerHealth.ReLiveTime;
            text.text = "距离复活还有" + string.Format("{0:f1}", (playerHealth.ReLiveTime - playerHealth.reliveTimeCounter)) + "秒";
        }
    }
    private void UpdateExp()
    {
        ExpSlider.value = (playerHealth.AllExp - (playerHealth.Level - 1) * 5) / 5 > 1 ? 1 : (playerHealth.AllExp - (playerHealth.Level - 1) * 5) / 5;
    }
    private void UpdateLevelImage()
    {
        if(playerHealth.Level==2)
        {
            image1.SetActive(true);
        }
        if(playerHealth.Level==3)
        {
            image2.SetActive(true);
        }
    }
    private void UpdateSupply()
    {
        if(isSuppy)
        {
            Supply.SetActive(true);
            SupplyImage.SetActive(true);
        }
        else
        {
            Supply.SetActive(false);
            SupplyImage.SetActive(false);
        }
    }
    private void UpdateBuff()
    {
        if(isBuff)
        {
            Buff.SetActive(true);
            BuffImage.SetActive(true);      
        }
        else
        {
            Buff.SetActive(false);
            BuffImage.SetActive(false);  
        }
    }
    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("RedSupply"))
        {
            isSuppy = true;
            supplyTimeCounter = 0.3f;
            if (countTime >= 1f)
            {
                playerGun.bulletNumber += 20;
                if (playerGun.bulletNumber > playerHealth.maxBulletNumber)
                {
                    playerGun.bulletNumber = playerHealth.maxBulletNumber;
                }
                playerHealth.HP += 100;
                countTime = 0f;
            }
        }
        if(other.CompareTag("Buff"))
        {
            isBuff = true;
            playerHealth.attackRate = 2f;
            buffTimeCounter = 0.3f;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(soundCount==1)
        {
            if (other.CompareTag("RedSupply"))
            {
                audioSource.PlayOneShot(audioClip2);
                soundCount--;
            }
            if (other.CompareTag("Buff"))
            {
                audioSource.PlayOneShot(audioClip1);
                soundCount--;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (soundCount == 0)
        {
            if (other.CompareTag("RedSupply"))
            {
                soundCount++;
            }
            if (other.CompareTag("Buff"))
            {
                soundCount++;
            }
        }
    }
    private void MapCamera()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)||Input.GetKeyDown(KeyCode.I)||Input.GetKeyDown(KeyCode.Keypad1))
        {
            isOpenMap = !isOpenMap;
        }
        if(isOpenMap)
        {
            mapCamera.SetActive(true);
        }
        if(!isOpenMap)
        {
            mapCamera.SetActive(false);
        }
    }
}
