using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private float resetTime = 5f;
    [SerializeField] private GameObject fireUI;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject score;
    [SerializeField] private GameObject scoreManager;
    [SerializeField] private GameObject clock;
    [SerializeField] private float fadeInTime;

    private Image fireUIImage;
    private Text gameOverText;
    private Text scoreText;
    private Image clockImage;

    private Color fireUIImageColor;
    private Color gameOverTextColor;
    private Color scoreTextColor;
    private Color clockImageColor;

    private BackgroundStage bg;

    private AudioSource tickSource;

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

        clockImage = clock.GetComponent<Image>();
        clockImageColor = clockImage.color;
        clockImage.color = Color.clear;

        tickSource = GetComponent<AudioSource>();

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlantController>().ChangedHealth += MaybeGameOver;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<BackgroundStage>().EndGame += GameOver;
    }

    private void MaybeGameOver(object sender, ChangedHealthArgs e)
    {
        if (e.NewHealth <= 0)
            GameOver(sender, new EndGameArgs { Died = true });
    }

    private void GameOver(object sender, EndGameArgs e)
    {
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
            clockImage.color = Color.Lerp(Color.clear, clockImageColor, percent);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(Restart());
    }

    private IEnumerator Restart()
    {
        float totalElapsedTime = 0;
        for (int i = 0; i < (int)resetTime; i++)
        {
            tickSource.Play();
            float tickElapsedTime = 0;
            while (tickElapsedTime < 1.0f)
            {
                float percent = totalElapsedTime / resetTime;
                clockImage.fillAmount = (1 - percent);
                tickElapsedTime += Time.deltaTime;
                totalElapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        SceneManager.LoadScene(0);
    }
}
