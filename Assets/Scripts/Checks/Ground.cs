using UnityEngine;

namespace PP
{
    public class Ground : MonoBehaviour
    {
        public bool onGround { get; private set; }
        public float friction { get; private set; }
        public bool OnWall { get; private set; }
        public Vector2 ContactNormal { get; private set; }

    private PhysicsMaterial2D material;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            EvaluateCollision(collision);
            RetrieveCollision(collision);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            EvaluateCollision(collision);
            RetrieveCollision(collision);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            onGround = false;
            friction = 0.0f;
            OnWall = false;
        }

        public void EvaluateCollision(Collision2D collision)
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                ContactNormal = collision.GetContact(i).normal;
                OnWall = Mathf.Abs(ContactNormal.x) >= 0.9f;
                onGround |= ContactNormal.y >= 0.9f;
            }
        }

        private void RetrieveCollision(Collision2D collision)
        {
            PhysicsMaterial2D material = collision.rigidbody.sharedMaterial;

            friction = 0;

            if (material != null)
            {
                friction = material.friction;
            }
        }

        public bool GetOnGround()
        {
            return onGround;
        }

        public float GetFriction()
        {
            return friction;
        }
    }
}