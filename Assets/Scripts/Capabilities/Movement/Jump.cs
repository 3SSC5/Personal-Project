using UnityEngine;

namespace PP
{
    [RequireComponent(typeof(Controller))]
    public class Jump : MonoBehaviour
    {
        [SerializeField, Range(0f, 10f)] private float jumpHeight = 3f;
        [SerializeField, Range(0, 5)] private int maxAirJumps = 0;
        [SerializeField, Range(0f, 5f)] private float downwardMovementMultiplier = 3f;
        [SerializeField, Range(0f, 5f)] private float upwardMovementMultiplier = 1.7f;
        [SerializeField, Range(0f, 0.3f)] private float coyoteTime = 0.2f;
        [SerializeField, Range(0f, 0.3f)] private float jumpBufferTime = 0.2f;

        private Controller controller;
        private Rigidbody2D body;
        private Ground ground;
        private Vector2 velocity;

        private bool desiredJump, onGround, isJumping;

        private int jumpPhase;
        private float defaultGravityScale, jumpSpeed, coyoteCounter, jumpBufferCounter;
        // Start is called before the first frame update
        void Start()
        {
            body = GetComponent<Rigidbody2D>();
            ground = GetComponent<Ground>();
            controller = GetComponent<Controller>();

            defaultGravityScale = 1f;
        }

        // Update is called once per frame
        void Update()
        {
            desiredJump |= controller.input.RetrieveJumpInput();
        }

        private void FixedUpdate()
        {
            onGround = ground.GetOnGround();
            velocity = body.velocity;

            if (onGround && body.velocity.y == 0)
            {
                jumpPhase = 0;
                coyoteCounter = coyoteTime;
                isJumping = false;
            }
            else
            {
                coyoteCounter -= Time.deltaTime;
            }

            if (desiredJump)
            {
                desiredJump = false;
                JumpAction();
                jumpBufferCounter = jumpBufferTime;
            }
            else if (!desiredJump && jumpBufferCounter > 0)
            {
                jumpBufferCounter -= Time.deltaTime;
            }

            if (jumpBufferCounter > 0)
            {
                JumpAction();
            }

            if (controller.input.RetrieveJumpHoldInput() && body.velocity.y > 0)
            {
                body.gravityScale = upwardMovementMultiplier;
            }
            else if (!controller.input.RetrieveJumpHoldInput() || body.velocity.y < 0)
            {
                body.gravityScale = downwardMovementMultiplier;
            }
            else if (body.velocity.y == 0)
            {
                body.gravityScale = defaultGravityScale;
            }

            body.velocity = velocity;
        }

        private void JumpAction()
        {
            if (coyoteCounter > 0f || jumpPhase < maxAirJumps)
            {
                if (isJumping)
                {
                    jumpPhase += 1;
                }

                jumpBufferCounter = 0;
                coyoteCounter = 0;
                jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);
                isJumping = true;

                if (velocity.y > 0f)
                {
                    jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
                }
                velocity.y += jumpSpeed;
            }
        }
    }
}
