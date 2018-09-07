using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ComboManager comboScript;
    public static GameManager instance;
    public GameObject player;
    private int waveCount;

    public int WaveCount
    {
        get { return waveCount; }
        set
        {
            waveCount = value;
            waveTxt.text = waveCount.ToString();
        }
    }

    [SerializeField] private int score;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value * comboScript.ComboMultiplier;
            scoreTxt.text = Score.ToString();
        }
    }


    [Header("Interface")]
    public Text scoreTxt;
    public Text scoreBilan;
    public Text waveTxt;
    public HUDMoveFromTo waveDisplay;
    HUD_Display goTarget;

    public GameObject gameOverPanel;

    private void Awake()
    {
        instance = GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        goTarget = gameOverPanel.GetComponent<HUD_Display>();

      
    }

    void Start()
    {  
        StartCoroutine(goTarget.Hide(0));
        waveDisplay.Show(1);
    }
    public void GameOver()
    {
        StopEnemies();

        scoreBilan.text = Score.ToString();

        StartCoroutine(goTarget.Show());


    }

    void StopEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().GameOver();
        }
    }
    public void RestartLevel()
    {
        Scene curScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(curScene.name);
    }


}
