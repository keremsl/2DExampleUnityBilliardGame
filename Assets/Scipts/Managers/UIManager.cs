using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;

[System.Serializable]
public struct PlayerNameRequestWindow {
    public GameObject windowGo;
    public TMP_InputField playerNameField;
    public Button okBtn;
}

public class UIManager : MonoBehaviour {
    public static UIManager Instance = null;
    public PlayerNameRequestWindow nameRequestWindow;
    public GameObject loadingWindowGo;
    public Image bgFaderImg;
    private void Awake () {
        if (Instance == null)
            Instance = this;
    }

    public void FadeBG (float _alpha) {
        bgFaderImg.DOFade (_alpha, 0.5f);
    }

    public void ShowRequestWindow () {
        nameRequestWindow.windowGo.transform.DOScale (Vector3.one, 0.5f).SetEase (Ease.InBounce);
    }

    public void ShowLoadingWindow () {
        loadingWindowGo.transform.DOScale (Vector3.one, 0.5f).SetEase (Ease.InBounce);
    }

    public void CloseRequestWindow () {
        nameRequestWindow.windowGo.transform.DOScale (Vector3.zero, 0.5f).SetEase (Ease.OutBounce);
    }

    public void CloseLoadingWindow () {
        loadingWindowGo.transform.DOScale (Vector3.zero, 0.5f).SetEase (Ease.OutBounce);
    }

}