using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;

namespace BilliardDemo {

    [System.Serializable]
    public struct PlayerNameRequestWindow {
        public GameObject windowGo;
        public TMP_InputField playerNameField;
        public Button okBtn;
    }

    public class UIManager : MonoBehaviour {
        [SerializeField]
        PlayerNameRequestWindow nameRequestWindow;
        [SerializeField]
        GameObject loadingWindowGo;
        [SerializeField]
        Image bgFaderImg;
        [SerializeField]
        TMP_Text greetingTxt;
        [SerializeField]
        GameEventListener OnPlayerNameSetted;
        [SerializeField]
        TMP_Text scoreTxt;
        private void OnEnable () {
            nameRequestWindow.playerNameField.characterLimit = 20;
            nameRequestWindow.okBtn.onClick.AddListener (() => {
                OnPlayerNameSetted.OnEventRaised ();
            });
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

        public void CheckAndSetInteractableContinueBtn () {
            nameRequestWindow.okBtn.interactable = !string.IsNullOrEmpty (nameRequestWindow.playerNameField.text);
        }

        public void ShowGreetingMessage () {
            string str = "";
            if (string.IsNullOrEmpty (GameManager.Instance.GetSunsetTime ()))
                str = $"Great time {GameManager.Instance.GetUserName()} to play a billiard game!";
            else
                str = $"Great time {GameManager.Instance.GetUserName()} to play a {GameManager.Instance.GetTimePeriodAsString()} billiard game! <color=\"green\">Sunset</color> today starts at {GameManager.Instance.GetSunsetTime()}.";

            greetingTxt.text = str;
            greetingTxt.DOFade (1F, 0.5F);
            greetingTxt.DOFade (0F, 0.5F).SetDelay (10f);
        }

        public void UpdateScoreTxt() {
            scoreTxt.text = $"SCORE: {GameManager.Instance.GetUserScore()}"; 
        }

    }
}