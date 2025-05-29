using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LogoFadeIn : MonoBehaviour
{
    [SerializeField] private RawImage _wellChromedLogo;
    private float fadeInTimer = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        Color color = _wellChromedLogo.color;
        float alpha = 0f;
        color.a = alpha;
        _wellChromedLogo.color = color;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime / fadeInTimer;
            color.a = Mathf.Clamp01(alpha);
            _wellChromedLogo.color = color;
            yield return null;
        }

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        Color color = _wellChromedLogo.color;
        float alpha = 1f;
        color.a = alpha;
        _wellChromedLogo.color = color;

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime / fadeInTimer;
            color.a = Mathf.Clamp01(alpha);
            _wellChromedLogo.color = color;
            yield return null;
        }
    }
}
