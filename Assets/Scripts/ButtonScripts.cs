
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScripts : MonoBehaviour
{

    [SerializeField] private Slider enemyCountSlider;
    [SerializeField] private TMP_Text enemyCountText;
    [SerializeField] private TMP_Text HighScoreText;

    string enemyCount_PP = "enemyCount";
    string highScore_PP = "highScore";

    private void Start()
    {
        InitializeEnemyCount();
        InitializeHighScore();
        UpdateEnemyCount();
    }

    private void InitializeEnemyCount()
    {
        if (!PlayerPrefs.HasKey(enemyCount_PP))
        {
            PlayerPrefs.SetFloat(enemyCount_PP, enemyCountSlider.value);
        }
        else enemyCountSlider.value = PlayerPrefs.GetFloat(enemyCount_PP);
    }
    private void InitializeHighScore()
    {
        if (!PlayerPrefs.HasKey(highScore_PP))
        {
            PlayerPrefs.SetInt(highScore_PP, (int)Convert.ToSingle(HighScoreText.text));
        }
        else HighScoreText.text = PlayerPrefs.GetInt(highScore_PP).ToString();
    }

    public void ResetHighSCore()
    {
        PlayerPrefs.SetInt(highScore_PP, 0);
        InitializeHighScore();
    }

    public void UpdateHighScore()
    {
        HighScoreText.text = PlayerPrefs.GetFloat(highScore_PP).ToString();
    }
    public void UpdateEnemyCount()
    {
        enemyCountText.text = enemyCountSlider.value.ToString();
        PlayerPrefs.SetFloat(enemyCount_PP,enemyCountSlider.value);
    }

    public void OnQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
}
