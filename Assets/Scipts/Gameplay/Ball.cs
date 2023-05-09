using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace BilliardDemo {
    [RequireComponent (typeof (Rigidbody2D))]
    public class Ball : MonoBehaviour {

        protected Rigidbody2D mRigid2D;
        protected Collider2D mCollider;
        protected SpriteRenderer mSpriteRenderer;
        protected const string HOLE_TAG = "Hole";
        protected Vector2 MY_SCALE;
        
        private void Start () {
            mRigid2D = GetComponent<Rigidbody2D> ();
            mCollider = GetComponent<Collider2D> ();
            mSpriteRenderer = GetComponent<SpriteRenderer> ();
            MY_SCALE = transform.localScale;
        }

        private void OnTriggerEnter2D (Collider2D other) {
            if (other.CompareTag (HOLE_TAG)) {
                FakeHolePhysics (other);
            }
        }

        public virtual void FakeHolePhysics (Collider2D other) {
            mCollider.enabled = false;
            mRigid2D.velocity = Vector2.zero;
            transform.DOMove (other.ClosestPoint (transform.position), 0.4f);
            mSpriteRenderer.DOFade (0, 0.5F).OnComplete (() => {
                mSpriteRenderer.DOFade (1, 0.5F);
                mCollider.enabled = true;
            });
        }
    }
}