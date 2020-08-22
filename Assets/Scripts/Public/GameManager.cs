using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// 
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int enemyNumber = 2;
    public int friendNumber = 2;
    public float sensitive = 1;
    public Slider sensitiveSlider;
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;
    public GameObject aboutMe;
    //public GameObject gameOver;
    public Dropdown enemyNumberDropDown;
    public Dropdown friendNumberDropDown;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        UpdateSetUP();
    }
    public void SetUpInfo()
    {
        panel2.SetActive(true);
        panel1.SetActive(false);
    }
    public void BackMainMenu()
    {
        panel1.SetActive(true);
        panel2.SetActive(false);
        panel3.SetActive(false);
        aboutMe.SetActive(false);
    }
    public void Help()
    {
        panel3.SetActive(true);
        panel1.SetActive(false);
    }
    public void AboutMe()
    {
        aboutMe.SetActive(true);
        panel1.SetActive(false);
    }
    public void Play()
    {
        UpdateSetUP();
        SceneManager.LoadScene(1);
    }
    public void BackBegin()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    private void UpdateSetUP()
    {
        enemyNumber = enemyNumberDropDown.value;
        friendNumber = friendNumberDropDown.value;
        sensitive = sensitiveSlider.value*2 + 0.3f;
    }
}
