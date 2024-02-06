using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PP
{
    public class PlayerHealth : MonoBehaviour
    {
        public Transform spawn;

        public bool attack;

        public int health;
        public int mHealth;

        public Sprite EmptyHeart;
        public Sprite FullHeart;

        private SpriteRenderer sR;

        public GameManager gameManager;

        public Animator anim;

        private bool isDead;

        private Ground g;
        private Move m;
        private Jump j;
        private WallJump w;
        private AttackScript a;

        public Image[] Hearts;

        // Start is called before the first frame update
        void Start()
        {
            transform.position = spawn.position;

            health = mHealth;

            g = GetComponent<Ground>();
            m = GetComponent<Move>();
            j = GetComponent<Jump>();
            w = GetComponent<WallJump>();
            a = GetComponent<AttackScript>();

            sR = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            for(int i = 0; i < Hearts.Length; i++)
            {
                if (i < health)
                {
                    Hearts[i].sprite = FullHeart;
                }
                else
                {
                    Hearts[i].sprite = EmptyHeart;
                }

                if(i < mHealth)
                {
                    Hearts[i].enabled = true;
                }
                else
                {
                    Hearts[i].enabled = false;
                }
            }

            if(Input.GetKey(KeyCode.U))
            {
                transform.position = spawn.position;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {

            if (collision.gameObject.tag == "Trap")
            {
                TakeDamage(2);
            }
        }

        public void TakeDamage(int Amount)
        {
            if(!attack)
            {
                health -= Amount;
            }

            if (health <= 0 & !isDead)
            {
                isDead = true;
                g.enabled = false;
                a.enabled = false;
                m.enabled = false;
                w.enabled = false;
                j.enabled = false;

                sR.enabled = false;

                gameManager.GameOver();
            }
        }
    }
}
