using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Global
{
    public static class GlobalManager
    {
        public static int levelNum = 1;

        private static PoolManager poolManager;
        private static CameraController cameraController;
        private static UI_Manager ui_Manager;
        private static Coroutines coroutines;
        private static GameManager gameManager;
        private static LevelManager levelManager;
        private static SaveGame saveGame;


        public static PoolManager PoolManager
        {
            get
            {
                if (poolManager == null)
                {
                    Debug.LogError("PoolManager   does not exist in the scene.");
                }
                return poolManager;

            }
            set
            {
                poolManager = value;
            }
        }
        public static CameraController CameraController
        {
            get
            {
                if (cameraController == null)
                {
                    Debug.LogError("CameraController   does not exist in the scene.");
                }
                return cameraController;

            }
            set
            {
                cameraController = value;
            }
        }
        public static UI_Manager UI_Manager
        {
            get
            {
                if (ui_Manager == null)
                {
                    Debug.LogError("UI_Manager   does not exist in the scene.");
                }
                return ui_Manager;

            }
            set
            {
                ui_Manager = value;
            }
        }
        public static Coroutines Coroutines
        {
            get
            {
                if (coroutines == null)
                {
                    Debug.LogError("Coroutines   does not exist in the scene.");
                }
                return coroutines;

            }
            set
            {
                coroutines = value;
            }
        }
        public static GameManager GameManager
        {
            get
            {
                if (gameManager == null)
                {
                    Debug.LogError("GameManager   does not exist in the scene.");
                }
                return gameManager;

            }
            set
            {
                gameManager = value;
            }
        }
        public static LevelManager LevelManager
        {
            get
            {
                if (levelManager == null)
                {
                    Debug.LogError("LevelManager   does not exist in the scene.");
                }
                return levelManager;

            }
            set
            {
                levelManager = value;
            }
        }
        public static SaveGame SaveGame
        {
            get
            {
                if (saveGame == null)
                {
                    Debug.LogError("SaveGame   does not exist in the scene.");
                }
                return saveGame;

            }
            set
            {
                saveGame = value;
            }
        }
    }
}
