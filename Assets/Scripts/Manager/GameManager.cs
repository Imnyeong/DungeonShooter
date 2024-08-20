using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonShooter
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public GameObject characterPrefab;
        public GameObject cameraPrefab;
        public GameObject map;

        public string currentPlayer;

        public RoomData currentRoom = null;
        public UserData[] users = null;

        public List<Character> players;
        public Transform[] spawnPoint;

        public ObjectPool objectPool;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        private void Start()
        {
            WebRequestManager.instance.GetRoomInfo(LocalDataBase.instance.currentRoom, (response) =>
            {
                SetGame(response);
            });
        }

        public void SetGame(WebRequestResponse _response)
        {
            if (_response.code == 400)
                return;

            currentPlayer = LocalDataBase.instance.loginData.ID;
            currentRoom = JsonConvert.DeserializeObject<RoomData>(_response.message);
            users = JsonConvert.DeserializeObject<UserData[]>(currentRoom.Players);

            for (int i = 0; i < users.Length; i++)
            {
                SpawnCharacter(users[i].ID, i);
            }
        }
        public void SetCamera(string _id)
        {
            GameObject camera = Instantiate(cameraPrefab, map.transform);
            FollowCam followCam = camera.GetComponent<FollowCam>();
            followCam.SetTarget(_id);
        }
        public void SpawnCharacter(string _id, int _index)
        {
            GameObject character = Instantiate(characterPrefab, map.transform);
            Character player = character.GetComponent<Character>();
            player.transform.position = spawnPoint[_index].position;
            
            player.SetInfo(_id);
            players.Add(player);

            if (currentPlayer == _id)
                SetCamera(_id);
        }
        public void SyncCharacters(string _data)
        {
            CharacterPacket info = JsonConvert.DeserializeObject<CharacterPacket>(_data);
            Character player = players.Find(x => x.id == info.id);

            if (player == null)
                return;

            player.DoSync(info.hp, info.position, info.rotation, info.animation);
        }
        public void SyncWeapon(string _data)
        {
            WeaponPacket info = JsonConvert.DeserializeObject<WeaponPacket>(_data);
            Character player = players.Find(x => x.id == info.playerID);

            if (player == null)
                return;

            player.attackController.SyncAttack(info.playerID, info.startPos, info.direction);
        }
    }
}