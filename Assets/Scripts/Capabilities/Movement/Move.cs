using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PP
{
    [RequireComponent(typeof(Controller))]
    public class Move : MonoBehaviour
    {
        [SerializeField] private Input_Controller input = null;
        [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
        [SerializeField, Range(0f, 100f)] private float maxAcceleration = 35f;
        [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 20f;
        [SerializeField, Range(0.05f, 0.5f)] private float wallStickTime = 0.25f;

        [SerializeField] private float dashingVelocity = 14f;
        [SerializeField] private float dashingTime = 0.5f;

        private Vector2 direction, desiredVelocity, velocity, dashingdir;
        private Rigidbody2D body;
        private Ground ground;
        private Controller controller;
        private WallJump walljump;

        private float maxSpeedChange, acceleration, wallStickCounter;
        private bool onGround, isDashing;
        private bool canDash = true;

        // Start is called before the first frame update
        void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            controller = GetComponent<Controller>();
            ground = GetComponent<Ground>();
            walljump = GetComponent<WallJump>();
        }

        // Update is called once per frame
        void Update()
        {
            direction.x =  input.RetrieveMoveInput();
            desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - ground.GetFriction(), 0f);

            var jumpRelease = Input.GetButtonUp("Jump");

            if (input.RetrieveDashInput() && canDash)
            {
                isDashing = true;
                canDash = false;
                dashingdir = new Vector2(input.RetrieveMoveInput(), Input.GetAxisRaw("Vertical"));

                if(dashingdir == Vector2.zero)
                {
                    dashingdir = new Vector2(transform.localScale.x, 0);
                }
                StartCoroutine(StopDash());
            }

            if(onGround || walljump.OnWall)
            {
                canDash = true;
            }

            if(isDashing)
            {
                body.velocity = dashingdir.normalized * dashingVelocity;
            }
        }

        private void FixedUpdate()
        {
            onGround = ground.GetOnGround();
            velocity = body.velocity;

            acceleration = onGround ? maxAcceleration : maxAirAcceleration;
            maxSpeedChange = acceleration * Time.deltaTime;
            velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

            #region Wall Stick
            if (ground.OnWall && !ground.onGround && !walljump.WallJumping)
            {
                if (wallStickCounter > 0)
                {
                    velocity.x = 0;

                    if (controller.input.RetrieveMoveInput() == ground.ContactNormal.x)
                    {
                        wallStickCounter -= Time.deltaTime;
                    }
                    else
                    {
                        wallStickCounter = wallStickTime;
                    }
                }
                else
                {
                    wallStickCounter = wallStickTime;
                }
            }
            #endregion

            body.velocity = velocity;
        }

        private IEnumerator StopDash()
        {
            yield return new WaitForSeconds(dashingTime);
            isDashing = false;
        }
    }
}
