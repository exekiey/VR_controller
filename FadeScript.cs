using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FadeScript : MonoBehaviour
{
    RawImage blackImage;
    float colorCount;

    [SerializeField] float increaseFactor;
    [SerializeField] float secondsWaited;

    public event Action FadeFinished;

    private void Awake()
    {
        blackImage = GetComponent<RawImage>();
        colorCount = 0;
    }
    IEnumerator TeleportCoroutine()
    {
        while (colorCount < 1f)
        {
            colorCount += increaseFactor;
            blackImage.color = new Color(0, 0, 0, colorCount);
            yield return new WaitForSeconds(secondsWaited);
        }

        FadeFinished();

        while (colorCount > 0)
        {
            colorCount -= increaseFactor;
            blackImage.color = new Color(0, 0, 0, colorCount);
            yield return new WaitForSeconds(secondsWaited);
        }


    }

    public void StartFade()
    {
        StartCoroutine(TeleportCoroutine());
    }
}
