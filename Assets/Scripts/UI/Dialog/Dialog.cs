using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text contentText;
    public GameObject background;

    public virtual void Show(bool isShow)
    {
        gameObject.SetActive(isShow);
    }

    public virtual void UpdateDialog(string title, string content)
    {
        if (titleText != null)
            titleText.text = title;
        if (contentText != null)
            contentText.text = content;
    }

    public virtual void Close()
    {
        LeanTweenManager.Ins.CloseDialog(background);
        StartCoroutine(TimeDelay());
    }

    IEnumerator TimeDelay()
    {
        yield return new WaitForSeconds(0.2f);
        Show(false);
    }
}
