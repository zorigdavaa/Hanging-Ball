using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZPackage
{
    public static class Z
    {
        public static GameManager GM => GameManager.Instance;
        public static CameraController CamC => CameraController.Instance;
        public static CanvasManager CanM => CanvasManager.Instance;
        public static LevelSpawner LS => LevelSpawner.Instance;
        private static Player _player;
        public static Player Player
        {
            get
            {
                if (_player == null)
                {
                    _player = Mb.FindObjectOfType<Player>();
                }
                return _player;
            }
            set
            {
                _player = value;
            }
        }
        private static DemolishBall _ball;
        public static DemolishBall Ball
        {
            get
            {
                if (_ball == null)
                {
                    _ball = Mb.FindObjectOfType<DemolishBall>();
                }
                return _ball;
            }
            set
            {
                _ball = value;
            }
        }
    }
}

