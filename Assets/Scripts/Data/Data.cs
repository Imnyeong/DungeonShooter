﻿using System;
using UnityEngine;

namespace DungeonShooter
{
    #region Enum
    public enum ViewModelType
    {
        Login,
        Register,
        Lobby,
        Room
    }
    public enum PopupType
    {
        Error,
        CreateRoom
    }
    #endregion
    #region String
    public class StringData
    {
        // Scene
        public const string SceneLobby = "Lobby";
        public const string SceneInGame = "InGame";
        // Animation
        public const string AnimationMove = "Move";
        public const string AnimationBack = "Back";
        public const string AnimationJump = "Jump";
        public const string AnimationAttack = "Attack";
        public const string AnimationDeath = "Death";
        // Room
        public const string RoomJoin = "Join";
        public const string RoomExit = "Exit";
        public const string RoomDelete = "Delete";
        public const string RoomCanJoin = "대기 중";
        public const string RoomCantJoin = "플레이 중";
        // Game
        public const string GameStart = "Start";
        // Tag
        public const string TagMap = "Map";
        public const string TagPlayer = "Player";
        public const string TagWeapon = "Weapon";
    }
    #endregion
    #region WebRequestPacket
    public class WebRequestResponse
    {
        public int code;
        public string message;
    }
    [Serializable]
    public class UserData
    {
        public string ID;
        public string PW;
        public string Nickname;
        public int Win = 0;
        public int Lose = 0;
        public int Gold = 0;
    }
    public class LoginData
    {
        public string ID;
        public string PW;
    }

    [Serializable]
    public class RoomData
    {
        public int RoomID;
        public string RoomName;
        public string MasterID;
        public int CanJoin;
        public string Players;
    }
    #endregion
    #region WebSocketPacket
    public enum PacketType
    {
        Room,
        Spawn,
        Character,
        Game,
        Weapon
    }
    public enum AnimationType
    {
        Idle,
        Move,
        Back,
        Jump,
        Attack,
        Death
    }
    public class WebSocketRequest
    {
        public PacketType packetType;
        public string data;
    }
    public class WebSocketResponse
    {
        public PacketType packetType;
        public string data;
    }
    public class CharacterPacket
    {
        public string id;
        public int hp;
        public Vector3 position;
        public Quaternion rotation;
        public AnimationType animation;
    }
    public class WeaponPacket
    {
        public string playerID;
        public Vector3 startPos;
        public Vector3 direction;
    }
    #endregion
}
