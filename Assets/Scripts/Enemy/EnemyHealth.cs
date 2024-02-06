using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

namespace PP
{
    public class EnemyHealth : MonoBehaviour
    {
        public Transform pT;
        private PlayerHealth pH;
        public int damage = 2;

        public int health;
        public int mHealth;

        private Rigidbody2D rb;
        public float speed;
        public float rayD;
        public float chaseD;

        private bool isChase;

        public LayerMask Ground;

        public Transform ledgeDetector;

        private void Start()
        {
            health = mHealth;   
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            RaycastHit2D hit = Physics2D.Raycast(ledgeDetector.position, Vector2.down, rayD, Ground);

            rb.velocity = new Vector2(speed, rb.velocity.y);

            if (hit.collider == null)
            {
                Rotate();
            }

            if(isChase)
            {

                if (transform.position.x > pT.position.x && speed > 0)
                {
                    Rotate();
                }
                if(transform.position.x < pT.position.x && speed < 0)
                {
                    Rotate();
                }
            }

            if(Vector2.Distance(transform.position, pT.position) < chaseD)
            {
                isChase = true;
            }
            else
            {
                isChase = false;
            }
        }

        void Rotate()
        {
            transform.Rotate(0,180,0);
            speed *= -1;
        }

        //Coroutine that runs to allow the enemy to receive damage again
        private void OnCollisionEnter2D(Collision2D collision)
        {
            pH = collision.gameObject.GetComponent<PlayerHealth>();
            if (collision.gameObject.tag == "Player")
            {
                pH.TakeDamage(damage);
            }

            if(collision.gameObject.tag == "Trap")
            {
                Rotate();
            }
        }

        public void TakeDamage(int Amount)
        {
            health -= Amount;

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}