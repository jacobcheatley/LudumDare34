using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutOnStart : MonoBehaviour
{
    [SerializeField] private float preFadeTime = 2f;
    [SerializeField] private float fadeTime = 2f;

    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(preFadeTime);
        float elapsedTime = 0;
        while (elapsedTime <= fadeTime)
        {
            image.color = Color.Lerp(Color.white, Color.clear, elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
