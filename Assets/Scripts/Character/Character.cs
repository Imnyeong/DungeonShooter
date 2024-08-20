using UnityEngine;

namespace DungeonShooter
{
    public class Character : MonoBehaviour
    {
        public Collider hitCol;
        public Collider bottomCol;
        public Rigidbody rigid;

        public string id;
        public int hp;
        public int weaponId;

        private const int maxHp = 100;
        public bool isLive { get; private set; } = true;

        [HideInInspector] public CharacterInput inputController;
        [HideInInspector] public CharacterAnimation animController;
        [HideInInspector] public CharacterAttack attackController;
        [HideInInspector] public FollowCam followCam;

        private void LateUpdate()
        {
            if (id != GameManager.instance.currentPlayer)
                return;

            SendPacket();
        }
        public void OnTriggerEnter(Collider other)
        {
            if (inputController != null && other.gameObject.tag == StringData.TagMap)
            {
                inputController.InGround();
            }
        }
        public void CanAttack()
        {
            if(inputController != null)
                inputController.CanAttack();
        }
        public void HitWeapon(Weapon _weapon)
        {
            if (inputController != null && _weapon.playerID != id)
            {
                _weapon.gameObject.SetActive(false);
                hp -= _weapon.atkDamage;
                if(hp <= 0)
                {
                    Death();
                }
            }
        }
        public void SetInfo(string _id)
        {
            id = _id;
            hp = maxHp;
            if (GameManager.instance.currentPlayer == _id)
            {
                inputController = gameObject.AddComponent<CharacterInput>();
            }
            animController = gameObject.AddComponent<CharacterAnimation>();
            attackController = gameObject.AddComponent<CharacterAttack>();
        }
        public void Death()
        {
            isLive = false;
        }
        public void ClearData()
        {
            Destroy(inputController);
            Destroy(hitCol);
            Destroy(rigid);
        }
        private void SendPacket()
        {
            CharacterPacket packet = new CharacterPacket()
            {
                id = id,
                hp = hp,
                position = transform.localPosition,
                rotation = transform.localRotation,
                animation = animController.animationType
            };
            WebSocketRequest request = new WebSocketRequest()
            {
                packetType = PacketType.Character,
                data = JsonUtility.ToJson(packet)
            };
            WebSocketManager.instance.SendPacket(JsonUtility.ToJson(request));
        }
        public void DoSync(int _hp, Vector3 _position, Quaternion _rotation, AnimationType _anim)
        {
            hp = _hp;
            transform.localPosition = _position;
            transform.localRotation = _rotation;
            animController.DoAnimation(_anim);
        }
    }
}