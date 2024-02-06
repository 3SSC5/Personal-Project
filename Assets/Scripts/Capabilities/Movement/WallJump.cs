using UnityEngine;

namespace PP
{
    [RequireComponent(typeof(Controller))]
    public class WallJump : MonoBehaviour
    {
        public bool WallJumping { get; private set; }

        [Header("Wall Slide")]
        [SerializeField][Range(0.1f, 5f)] private float WallMaxSpeed = 2f;
        [Header("Wall Jump")]
        [SerializeField] private Vector2 WallClimb = new Vector2(4f, 12f);
        [SerializeField] private Vector2 WallBounce = new Vector2(10.7f, 10f);
        [SerializeField] private Vector2 WallLeap = new Vector2(14f, 12f);

        private Ground ground;
        private Rigidbody2D Body;
        private Controller Controller;

        public Vector2 Velocity;
        public bool OnWall, OnGround, DesiredJump;
        private float WallX;

        // Start is called before the first frame update
        void Start()
        {
            ground = GetComponent<Ground>();
            Body = GetComponent<Rigidbody2D>();
            Controller = GetComponent<Controller>();
        }

        // Update is called once per frame
        void Update()
        {
            if (OnWall && !OnGround)
            {
                DesiredJump |= Controller.input.RetrieveJumpInput();
            }

        }

        private void FixedUpdate()
        {
            Velocity = Body.velocity;
            OnWall = ground.OnWall;
            OnGround = ground.onGround;
            WallX = ground.ContactNormal.x;

            #region Wall Sliding
            if (OnWall)
            {
                if(Velocity.y < -WallMaxSpeed)
                {
                    Velocity.y = -WallMaxSpeed;
                }
            }
            #endregion

            #region Wall Jump
            if ((OnWall && Velocity.x == 0) || OnGround)
            {
                WallJumping = false;
            }
            if (DesiredJump)
            {
                if (-WallX == Controller.input.RetrieveMoveInput())
                {
                    Velocity = new Vector2(WallClimb.x * WallX, WallClimb.y);
                    WallJumping = true;
                    DesiredJump = false;
                }
                else if (Controller.input.RetrieveMoveInput() == 0)
                {
                    Velocity = new Vector2(WallBounce.x * WallX, WallBounce.y);
                    WallJumping = true;
                    DesiredJump = false;
                }
                else
                {
                    Velocity = new Vector2(WallLeap.x * WallX, WallLeap.y);
                    WallJumping = true;
                    DesiredJump = false;
                }
            }
            #endregion

            Body.velocity = Velocity;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            ground.EvaluateCollision(collision);

            if (ground.OnWall && !ground.onGround && WallJumping)
            {
                Body.velocity = Vector2.zero;
            }
        }
    }
}
