using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float gameOverVFXTime = 3f;
    public Image gameOverPanel;
    public TMP_Text gameOverText;

    public void ResetTimeScale()
    {
        Time.timeScale = 1f;
    }
    public void GameOver()
    {
        GameOverVFX();
        Invoke(nameof(BackToMenu), gameOverVFXTime);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void GameOverVFX()
    {
        gameOverPanel.gameObject.SetActive(true);
        StartCoroutine(AlphaVFX());
    }

    private IEnumerator AlphaVFX()
    {
        int ticks = (int)(gameOverVFXTime * 10);
        Color panelColor = gameOverPanel.color;
        Color textColor = gameOverText.color;
        for (int i = 0; i < ticks; i++)
        {
            panelColor.a = (float)i / ticks;
            textColor.a = (float)i / ticks;
            gameOverPanel.color = panelColor;
            gameOverText.color = textColor;
            yield return new WaitForSeconds(0.07f);
        }
    }
}
