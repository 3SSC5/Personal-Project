using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace PP
{
    [RequireComponent(typeof(Controller))]

    public class AttackScript : MonoBehaviour
    {
        private const double V = 0.1;
        private bool isLeft;
        private bool isRight;
        private bool isDwn;

        private Controller co;
        private Ground ground;
        public Animator anim;

        // Start is called before the first frame update
        void Start()
        {
            ground = GetComponent<Ground>();
            co = GetComponent<Controller>();
        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetAxis("Vertical") < 0 && !ground.onGround && !ground.OnWall)
            {
                isLeft = false;
                isRight = false;
                isDwn = true;
            }
            else if (!ground.OnWall)
            {
                isDwn = false;

                if (Input.GetKeyDown(KeyCode.A))
                {
                    isLeft = true;
                    isRight = false;
                }
                
                if (Input.GetKeyDown(KeyCode.D))
                {
                    isLeft = false;
                    isRight = true;
                }
            }

            if (co.input.RetrieveAttackInput() && isRight)
            {
                anim.SetTrigger("AttackRight");
            }
            else if (co.input.RetrieveAttackInput() && isLeft)
            {
                anim.SetTrigger("AttackLeft");
            }
            else if (co.input.RetrieveAttackInput() && isDwn)
            {
                anim.SetTrigger("AttackDown");
            }

        }
    }
}
