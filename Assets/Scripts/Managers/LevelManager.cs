using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Nightmare
{
    public class LevelManager : MonoBehaviour
    {
        public string[] levels;

        private int currentLevel = 0;
        private Scene currentScene;
        private PlayerMovement playerMove;
        private Vector3 playerRespawn;
        private CinematicController cinema;
        [SerializeField] private Text TimerTextComp;
        [SerializeField] private Text QuestTextComp;
        [SerializeField] private Button shopBtn;

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        void Start()
        {
            cinema = FindObjectOfType<CinematicController>();
            SceneManager.LoadSceneAsync(levels[0], LoadSceneMode.Additive);
            playerMove = FindObjectOfType<PlayerMovement>();
            playerRespawn = playerMove.transform.position;
        }

        public void AdvanceLevel()
        {
            LoadLevel(currentLevel + 1);
        }

        public void LoadInitialLevel()
        {
            LoadLevel(0);
        }

        private void LoadLevel(int level)
        {
            currentLevel = level;

            //Load next level in background
            string loadingScene = levels[level % levels.Length];
            SceneManager.LoadSceneAsync(loadingScene, LoadSceneMode.Additive);
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (mode != LoadSceneMode.Additive)
                return;

            // Optionally, make the text component invisible
            if (scene.name == "Shop")
            {
                TimerTextComp.gameObject.SetActive(false);
                Debug.Log("Setting " + TimerTextComp.gameObject.name + " to inactive.");
                QuestTextComp.gameObject.SetActive(false);
                Debug.Log("Setting " + QuestTextComp.gameObject.name + " to inactive.");
            } 
            else
            {
                TimerTextComp.gameObject.SetActive(true);
                Debug.Log("Setting " + TimerTextComp.gameObject.name + " to active.");
                QuestTextComp.gameObject.SetActive(true);
                Debug.Log("Setting " + QuestTextComp.gameObject.name + " to active.");
            }

            playerMove.transform.position = playerRespawn;
            SceneManager.SetActiveScene(scene);

            DisableOldScene();

            currentScene = scene;

            // Play realtime cinematic?
            if (scene.name != "Shop")
            {
                shopBtn.gameObject.SetActive(false);
                cinema.StartCinematic(CinematicController.CinematicType.Realtime);
            }
            else
            {
                shopBtn.gameObject.SetActive(true);
                ShopManager sm = FindObjectOfType<ShopManager>();
                shopBtn.onClick.AddListener(sm.OpenShop);
                cinema.StartCinematic(CinematicController.CinematicType.PreRendered);
            }
        }

        private void DisableOldScene()
        {
            if (currentScene.IsValid())
            {
                // Disable old scene.
                GameObject[] oldSceneObjects = currentScene.GetRootGameObjects();
                for (int i = 0; i < oldSceneObjects.Length; i++)
                {
                    oldSceneObjects[i].SetActive(false);
                }

                // Unload it.
                SceneManager.UnloadSceneAsync(currentScene);
            }
        }

        void OnSceneUnloaded(Scene scene)
        {

        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }

        public int GetCurrLevel()
        {
            return currentLevel;
        }
    }
}