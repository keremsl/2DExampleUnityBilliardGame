using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BilliardDemo {

    [RequireComponent (typeof (Rigidbody2D))]
    public class PlayerBall : Ball, IPointerDownHandler, IPointerUpHandler {
        #region Constants
        const float MAX_FORCE = 500F;
        const float FORCE_EXTENDER = 300f;
        Vector2 SELECTED_SCALE = Vector2.one * 0.1f;

        #endregion

        bool isPressing = false;

        float force;

        [SerializeField]
        Transform fillerParent;

        [SerializeField]
        Image forceFiller;

        [SerializeField]
        LineRenderer directionDrawer;



        private void Update () {
            if (!isPressing) return;

            if (force <= MAX_FORCE)
                force += Time.deltaTime * FORCE_EXTENDER;

            forceFiller.fillAmount = 1 - Mathf.Abs ((force / MAX_FORCE) - 1);
        }

        public void OnPointerDown (PointerEventData eventData) {
            ShowFiller ();
        }

        public void OnPointerUp (PointerEventData eventData) {
            if (isPressing) {
                ForceToRandomDirection ();
                HideFiller ();
                SoundManager.Instance.PlayFirstShotSound ();
            }
        }

        private void OnTriggerEnter2D (Collider2D other) {
            if (other.CompareTag (HOLE_TAG)) {
                FakeHolePhysics (other);
            }
        }

        void OnCollisionEnter2D (Collision2D collision) {
            if (mRigid2D.velocity.magnitude > 1) { // is meaningful to trigger drawer
                DrawLineRespectToDirection (collision);
                PlaySoundRespectToCol (collision);
                if (collision.transform.GetComponent<Ball> ())
                    OnBallCollide.Raise();
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

            DOTween.Complete (fillerParent);
            fillerParent.DOScale (Vector3.zero, 0.2f);

            DOTween.Complete (transform);
            transform.DOScale (MY_SCALE, 0.2f).SetEase (Ease.Linear);

            forceFiller.fillAmount = 0f;
        }

        public override void FakeHolePhysics (Collider2D other) {
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

        public void DrawLineRespectToDirection (Collision2D collision) {
            Vector2 colPoint = collision.GetContact (0).point;
            Vector2 ballVelocity =  mRigid2D.velocity.normalized;

            DOTween.Complete (directionDrawer.material);
            directionDrawer.material.DOFloat (0.5f, "_Cutoff", 0.5f).OnComplete (() => {
                directionDrawer.material.DOFloat (1f, "_Cutoff", 1f).SetDelay (1f);
            });

            directionDrawer.SetPosition (0, colPoint);
            directionDrawer.SetPosition (1, colPoint + ballVelocity * 3f);
        }
    }

}