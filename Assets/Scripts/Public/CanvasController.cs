using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// 
/// </summary>
public class CanvasController : MonoBehaviour
{
    public Gun gun;
    public Text killNumber;
    public Text bulletSpeed;
    public Text attack;
    public Text remainBullet;
    public GameObject Information;
    public GameObject GunHotWarning;
    public Image bloodImage;
    public Slider GunHot;
    public bool isOpen = true;
    public Vector3 WarningPoint;
    public Transform Right;
    public Transform Left;
    private float CountTime=5;//枪口热量超过时的提醒间隔
    public GameObject Panel;
    public GameObject[] allEnemys;
    public GameObject[] allFriends;
    public GameObject GamePanel;
    public bool isOpenGamePanel;
    public Slider redBase;
    public Slider greednBase;
    public float backHomeTimeCounter = 0f;
    private float backHomeTime = 3f;
    public Slider backHomeSlider;
    public GameObject backSlider;
    public GameObject SetUpPanel;
    public GameObject GameOverPanel;
    public GameObject redWin;
    public GameObject GreenWin;
    public bool isSetUp = false;
    public float TimeCounter;
    public Text timeText;

    private void Start()
    {
        TimeCounter = 300f;
       // WarningPoint = Camera.main.ScreenToWorldPoint(GunHotWarning.transform.position);
        for(int i=0;i<EnemySpawn.EnemyNumber;i++)
        {
            allEnemys[i].SetActive(true);
        }
        for(int i=1;i<FriendSpawn.friendNumber+1;i++)
        {
            allFriends[i].SetActive(true);
        }
    }
    private void Update()
    {
        TimeCounter -= Time.deltaTime;
        CountTime += Time.deltaTime;
        OpenInformation();
        ShowGunHot();
        UpdateInformation();
        //Death();
        GameOver();
        ShowGamePanel();
        UpdateGamePanel();
        UpdateBaseHP();
        BackHome();
        SetUp();
        ChangeTime();
    }
    private void OpenInformation()//关闭打开面板
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            if(isOpen)
            {
                Information.SetActive(false);
            }
            if(!isOpen)
            {
                Information.SetActive(true);
            }
            isOpen = !isOpen;
        }
    }
    private void UpdateInformation()
    {
        killNumber.text = "击杀:" + "<Color=green>" + PlayerHealth.instance.killNumber + "</Color>";
        bulletSpeed.text = "子弹速度:" + "<Color=green>" + gun.bulletSpeed.ToString() + "</Color>";
        if (PlayerHealth.instance.attackRate==1)
        {
            attack.text = "攻击力:" + "<Color=green>" + (gun.attack * PlayerHealth.instance.attackRate).ToString() + "</Color>";
        }
        else
        {
            attack.text = "攻击力:" + "<Color=yellow>" + (gun.attack * PlayerHealth.instance.attackRate).ToString() + "</Color>";
        }
        if (gun.bulletNumber<10)
        {
            remainBullet.text = "剩余子弹:" + "<Color=red>" + gun.bulletNumber.ToString() + "</Color>";
        }
        else
        {
            remainBullet.text = "剩余子弹:" + "<Color=green>" + gun.bulletNumber.ToString() + "</Color>";
        }
        bloodImage.fillAmount = PlayerHealth.instance.HP / PlayerHealth.instance.MaxHP;

    }
    private void ShowGunHot()
    {
        GunHot.value = gun.gunHot / PlayerHealth.instance.MaxGunHot;
        if(GunHot.value>=1&&CountTime>=5)
        {
            ITWeenMove();
            CountTime = 0;
        }
        if (Vector3.SqrMagnitude(GunHotWarning.transform.position - Left.position) < 10f)
        {
            GunHotWarning.transform.position = Right.transform.position;
        }
    }
    private void ITWeenMove()//利用ITween实现UI移动效果
    {
        iTween.MoveTo(GunHotWarning, iTween.Hash(
                "position", Left.position,
                "time", 3,
                "EaseType", "linear"
                ));
    }
    private void GameOver()
    {
        if(RedBase.isGameOver)
        {
            GreenWin.SetActive(true);
            Time.timeScale = 0f;
            GameOverPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if(GreenBase.isGameOver)
        {
            redWin.SetActive(true);
            Time.timeScale = 0f;
            GameOverPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    private void ShowGamePanel()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (isOpenGamePanel)
            {
                GamePanel.SetActive(false);
            }
            if (!isOpenGamePanel)
            {
                GamePanel.SetActive(true);
            }
            isOpenGamePanel = !isOpenGamePanel;
        }
    }
    private void UpdateGamePanel()//暂时这么解决,后面用泛型来做
    {
        for (int i = 0; i < EnemySpawn.allSpawnEnemys.Length;i++ )
        {
            if (EnemySpawn.allSpawnEnemys[i].GetComponent<EnemyInfo>().HP==0)
            {
                allEnemys[i].transform.GetChild(0).GetComponent<Image>().fillAmount = EnemySpawn.allSpawnEnemys[i].GetComponent<EnemyInfo>().reliveTimeCounter / EnemySpawn.allSpawnEnemys[i].GetComponent<EnemyInfo>().ReLiveTime;
            }
            allEnemys[i].transform.GetChild(3).GetComponent<Text>().text =string.Format( "{0:f0}",EnemySpawn.allSpawnEnemys[i].GetComponent<EnemyInfo>().killNumber);
            allEnemys[i].transform.GetChild(4).GetComponent<Text>().text = string.Format( "{0:f0}",EnemySpawn.allSpawnEnemys[i].GetComponent<EnemyInfo>().Level);
            allEnemys[i].transform.GetChild(5).GetComponent<Text>().text = string.Format( "{0:f0}",EnemySpawn.allSpawnEnemys[i].GetComponent<EnemyInfo>().HP);
        }
        //显示玩家信息
        if(PlayerHealth.instance.HP<=0)
        {
            allFriends[0].transform.GetChild(0).GetComponent<Image>().fillAmount = PlayerHealth.instance.reliveTimeCounter / PlayerHealth.instance.ReLiveTime;
        }
        allFriends[0].transform.GetChild(3).GetComponent<Text>().text = string.Format("{0:f0}", PlayerHealth.instance.HP);
        allFriends[0].transform.GetChild(4).GetComponent<Text>().text = string.Format("{0:f0}", PlayerHealth.instance.Level);
        allFriends[0].transform.GetChild(5).GetComponent<Text>().text = string.Format("{0:f0}", PlayerHealth.instance.killNumber);
        //显示哨兵信息
        if(RedAutoSolider.instance.HP<=0)
        {
            allFriends[4].transform.GetChild(0).GetComponent<Image>().fillAmount = 0;
        }
        allFriends[4].transform.GetChild(3).GetComponent<Text>().text = RedAutoSolider.instance.HP.ToString();
        if (GreenAutoSolider.instance.HP <= 0)
        {
            allEnemys[4].transform.GetChild(0).GetComponent<Image>().fillAmount = 0;
        }
        allEnemys[4].transform.GetChild(5).GetComponent<Text>().text = GreenAutoSolider.instance.HP.ToString();
        //显示友军信息
        for(int i=1;i<FriendSpawn.allSpawnFriends.Length+1;i++)
        {
            if(FriendSpawn.allSpawnFriends[i-1].GetComponent<FriendInfo>().HP==0)
            {
                allFriends[i].transform.GetChild(0).GetComponent<Image>().fillAmount = FriendSpawn.allSpawnFriends[i - 1].GetComponent<FriendInfo>().reliveTimeCounter / FriendSpawn.allSpawnFriends[i - 1].GetComponent<FriendInfo>().ReLiveTime;
            }
            allFriends[i].transform.GetChild(3).GetComponent<Text>().text = string.Format("{0:f0}", FriendSpawn.allSpawnFriends[i-1].GetComponent<FriendInfo>().HP);
            allFriends[i].transform.GetChild(4).GetComponent<Text>().text = string.Format("{0:f0}", FriendSpawn.allSpawnFriends[i-1].GetComponent<FriendInfo>().Level);
            allFriends[i].transform.GetChild(5).GetComponent<Text>().text = string.Format("{0:f0}", FriendSpawn.allSpawnFriends[i-1].GetComponent<FriendInfo>().killNumber);
        }
    }
    private void UpdateBaseHP()
    {
        redBase.value = RedBase.instance.HP / RedBase.instance.MaxHp;
        greednBase.value = GreenBase.instance.HP / GreenBase.instance.MaxHp;
    }
    private void BackHome()
    {
        if (Input.GetKey(KeyCode.B))
        {
            backSlider.SetActive(true);
            backHomeTimeCounter += Time.deltaTime;
            if (backHomeTimeCounter >= backHomeTime)
            {
                PlayerHealth.instance.transform.localPosition = PlayerHealth.instance.startPosition;
                PlayerHealth.instance.transform.rotation = PlayerHealth.instance.startRotation;
                backHomeTimeCounter = 0f;
            }
        }
        if(Input.GetKeyUp(KeyCode.B))
        {
            backHomeTimeCounter = 0f;
            backSlider.SetActive(false);
        }
        backHomeSlider.value = backHomeTimeCounter / backHomeTime;
    }
    public void SetUp()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isSetUp = !isSetUp;
            if (isSetUp)
            {
                SetUpPanel.SetActive(true);
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            if (!isSetUp)
            {
                SetUpPanel.SetActive(false);
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
    public void BackGame()
    {
        SetUpPanel.SetActive(false);
        isSetUp = !isSetUp;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void ReLoadGame()
    {
        TimeCounter = 300f;
        RedBase.isGameOver = false;
        GreenBase.isGameOver = false;
        Time.timeScale = 1f;
        GameOverPanel.SetActive(false);
        redWin.SetActive(false);
        GreenWin.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BackMainMenu()
    {
        TimeCounter=300f;
        RedBase.isGameOver = false;
        GreenBase.isGameOver = false;
        Time.timeScale = 1;
        GameOverPanel.SetActive(false);
        redWin.SetActive(false);
        GreenWin.SetActive(false);
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ChangeTime()
    {
        if(TimeCounter<=0)
        {
            TimeCounter = 0f;
            if(RedBase.instance.HP>GreenBase.instance.HP)
            {
                GreenBase.isGameOver = true;
            }
            else
            {
                RedBase.isGameOver = true;
            }
        }
        int a = (int)TimeCounter / 60;
        int b = (int)TimeCounter % 60;
        timeText.text = string.Format("{0:D2}", a) + ":" + string.Format("{0:D2}", b);
    }
}
