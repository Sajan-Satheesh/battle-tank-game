
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScripts : MonoBehaviour
{

    [SerializeField] private Slider enemyCountSlider;
    [SerializeField] private TMP_Text enemyCountText;

    string enemyCount_PP = "enemyCount";

    private void Start()
    {
        if (!PlayerPrefs.HasKey(enemyCount_PP))
        {
            PlayerPrefs.SetFloat(enemyCount_PP, enemyCountSlider.value);
        }
        else enemyCountSlider.value = PlayerPrefs.GetFloat(enemyCount_PP);

        updateEnemyCount();
    }
    public void updateEnemyCount()
    {
        enemyCountText.text = enemyCountSlider.value.ToString();
        PlayerPrefs.SetFloat(enemyCount_PP,enemyCountSlider.value);
    }

}
