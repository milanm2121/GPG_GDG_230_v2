using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingSlider : MonoBehaviour
{
    //This is to make a loading bar.
    private float loadTime = 0;

    public GameObject mainHUB;

    public GameObject blankObject;
    public Slider loadingSilder;

    public Text percentage;
    private float time;

    private void Update()
    {
        if (loadTime >= 10.02)
        {
            StopCoroutine("LoadingBarPercentage");
            mainHUB.SetActive(true);
            blankObject.SetActive(false);
            loadTime = 0;
        }
    }

    public void LoadingBar()
    {
        StartCoroutine("LoadingBarPercentage");
    }

    IEnumerator LoadingBarPercentage()
    {
        while (true)
        {
            loadTime += Time.deltaTime;
            loadingSilder.value = loadTime;
            time = loadTime * 10f;
            percentage.text = time.ToString("F0") + "%";
            yield return null;
        }
    }
}
