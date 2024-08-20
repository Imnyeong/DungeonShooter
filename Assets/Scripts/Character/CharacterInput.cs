using UnityEngine;

namespace DungeonShooter
{
    [RequireComponent(typeof(Character))]
    public class CharacterInput : MonoBehaviour
    {
        private Character player;

        public float moveX;
        public float moveZ;

        public float frontSpeed = 8.0f;
        public float backSpeed = 5.0f;

        public bool isJump = false;
        public bool isGround = true;
        public float jumpPower = 7.0f;

        public bool canShoot = true;

        private void Start()
        {
            player = GetComponent<Character>();
        }
        private void Update()
        {
            GetInputValues();
        }
        private void FixedUpdate()
        {
            Move();
            Jump();
        }
        public void InGround()
        {
            isGround = true;
        }
        private void GetInputValues()
        {
            moveX = Input.GetAxisRaw("Horizontal");
            moveZ = Input.GetAxisRaw("Vertical");

            if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonUp(1))
                player.followCam.ChangeView();

            if (Input.GetMouseButtonDown(0) && canShoot)
                player.shootController.OnClickAttack();

            if (Input.GetButtonDown("Jump") && isGround)
                isJump = true;
        }
        private void Move()
        {
            if (moveZ > 0)
                player.transform.Translate(new Vector3(moveX * Time.fixedDeltaTime * frontSpeed, 0, moveZ * Time.fixedDeltaTime * frontSpeed));
            else
                player.transform.Translate(new Vector3(moveX * Time.fixedDeltaTime * backSpeed, 0, moveZ * Time.fixedDeltaTime * backSpeed));
        }
        private void Jump()
        {
            if (isJump && isGround)
            {
                player.rigid.AddForce(0, jumpPower, 0, ForceMode.Impulse);
                isJump = false;
                isGround = false;
            }
        }
    }
}
