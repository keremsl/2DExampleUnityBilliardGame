using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BilliardDemo {

    [RequireComponent (typeof (Rigidbody2D))]
    public class PlayerBall : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
        #region Constants
        const float MAX_FORCE = 500F;
        const string HOLE_TAG = "Hole";
        const float FORCE_EXTENDER = 200F;
        Vector2 MY_SCALE = Vector2.one * 0.11f;
        Vector2 SELECTED_SCALE = Vector2.one * 0.13f;

        #endregion

        bool isPressing = false;
        Rigidbody2D mRigid2D;
        Collider2D mCollider;
        SpriteRenderer mSpriteRenderer;

        float force;

        [SerializeField]
        Transform fillerParent;

        [SerializeField]
        Image forceFiller;

        private void Start () {
            mRigid2D = GetComponent<Rigidbody2D> ();
            mCollider = GetComponent<Collider2D> ();
            mSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update () {
            if (!isPressing) return;

            if (force <= MAX_FORCE)
                force += Time.deltaTime * FORCE_EXTENDER;
            else
                force -= Time.deltaTime * FORCE_EXTENDER;

            if (force <= 0)
                force = 0;

            forceFiller.fillAmount = 1 - Mathf.Abs ((force / MAX_FORCE) - 1);

        }

        public void OnPointerDown (PointerEventData eventData) {
            ShowFiller ();
        }

        public void OnPointerUp (PointerEventData eventData) {
            if (isPressing) {
                ForceToRandomDirection ();
                HideFiller ();
            }
        }

        private void OnTriggerEnter2D (Collider2D other) {
            if (other.CompareTag (HOLE_TAG)) {
                FakeHolePhysics (other);
            }
        }

        public void ShowFiller () {
            isPressing = true;
            if (!DOTween.IsTweening (fillerParent))
                fillerParent.DOScale (Vector3.one, 0.2f);

            if (!DOTween.IsTweening (transform))
                transform.DOScale (SELECTED_SCALE, 0.2f).SetEase (Ease.Linear);
        }

        public void HideFiller () {
            isPressing = false;
            if (!DOTween.IsTweening (fillerParent))
                fillerParent.DOScale (Vector3.zero, 0.2f);

            if (!DOTween.IsTweening (transform))
                transform.DOScale (MY_SCALE, 0.2f).SetEase (Ease.Linear);

            forceFiller.fillAmount = 0f;
        }

        public void FakeHolePhysics (Collider2D other) {
            mCollider.enabled = false;
            mRigid2D.velocity = Vector2.zero;
            transform.DOMove (other.ClosestPoint (transform.position), 0.4f);
            mSpriteRenderer.DOFade (0, 0.5F).OnComplete (() => {
                transform.position = Vector2.zero;
                mSpriteRenderer.DOFade (1, 0.5F);
                mCollider.enabled = true;
            });
        }

        public void ForceToRandomDirection () {
            float x = Random.Range (-1f, 1f);
            float y = Random.Range (-1f, 1f);
            Vector2 direction = new Vector2 (x, y).normalized;
            mRigid2D.AddForce (direction * force);
            force = 0;
        }
    }

}