using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PresentsScript : MonoBehaviour
{

    [SerializeField] private Text _presentsText;
    [SerializeField] private GameObject _presentsObj;
    private Color color1 = Color.red;
    private Color color2 = Color.white;
    private float flashSpeed = 0.5f;

    void Start()
    {

        StartCoroutine(FadeIn());
    }

    private IEnumerator FlashText()
    {
        while (true)
        {
            _presentsText.color = color1;
            yield return new WaitForSeconds(flashSpeed);
            _presentsText.color = color2;
            yield return new WaitForSeconds(flashSpeed);
        }
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(0.9f);
        Color color = _presentsText.color;
        float alpha = 0f;
        color.a = alpha;
        _presentsText.color = color;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime / 2.7f;
            color.a = Mathf.Clamp01(alpha);
            _presentsText.color = color;
            yield return null;
        }


        StartCoroutine(FlashText());
    }



}
