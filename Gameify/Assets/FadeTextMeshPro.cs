using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeTextMeshPro : MonoBehaviour
{
    TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(Fade());
    }

    public IEnumerator Fade()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
        yield return new WaitForSeconds(1f);
        while (text.color.a > 0.0)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime * 1f));
            yield return null;
        }
        Destroy(gameObject);
    }
}
