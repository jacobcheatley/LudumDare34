using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject fireUI;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject score;
    [SerializeField] private GameObject scoreManager;
    [SerializeField] private float fadeInTime;

    private Image fireUIImage;
    private Text gameOverText;
    private Text scoreText;

    private Color fireUIImageColor;
    private Color gameOverTextColor;
    private Color scoreTextColor;

    private BackgroundStage bg;

    void Start()
    {
        fireUIImage = fireUI.GetComponent<Image>();
        fireUIImageColor = fireUIImage.color;
        fireUIImage.color = Color.clear;
        
        gameOverText = gameOver.GetComponent<Text>();
        gameOverTextColor = gameOverText.color;
        gameOverText.color = Color.clear;
        
        scoreText = score.GetComponent<Text>();
        scoreTextColor = scoreText.color;
        scoreText.color = Color.clear;

        bg = GetComponent<BackgroundStage>();
        bg.EndGame += GameOver;
    }

    private void GameOver(object sender, EndGameArgs e)
    {
        Debug.Log("END");
        if (e.Died)
            gameOverText.text = "YOU DIED!";
        scoreText.text = "FINAL SCORE: " + scoreManager.GetComponent<ScoreManager>().CurrentScore;
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0;
        while (elapsedTime <= fadeInTime)
        {
            float percent = elapsedTime / fadeInTime;
            fireUIImage.color = Color.Lerp(Color.clear, fireUIImageColor, percent);
            gameOverText.color = Color.Lerp(Color.clear, gameOverTextColor, percent);
            scoreText.color = Color.Lerp(Color.clear, scoreTextColor, percent);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
