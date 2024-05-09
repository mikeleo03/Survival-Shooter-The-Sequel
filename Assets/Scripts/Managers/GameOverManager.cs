using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Nightmare {
    public class GameOverManager : MonoBehaviour {
        private PlayerHealth playerHealth;
        [SerializeField] private TimerManager timerManager;
        [SerializeField] private CountdownManager countdownManager;
        [SerializeField] GameObject mobileHUD;
        Animator anim;
        LevelManager lm;

        public float restartDelay = 10f;
        float restartTimer;

        void Awake () {
            playerHealth = FindObjectOfType<PlayerHealth>();
            anim = GetComponent<Animator>();
            lm = FindObjectOfType<LevelManager>();
#if MOBILE_INPUT
            mobileHUD.SetActive(true);
#endif
        }

        void Update() {
            if (playerHealth.currentHealth <= 0) {
                mobileHUD.SetActive(false);
                anim.SetTrigger("GameOver");
                countdownManager.StartCountdown();
            }
        }

        private void ResetLevel() {
            // ScoreManager.score = 0;
            if (InGameTextStatistics.score > TextStatistics.highScore)
            {
                TextStatistics.highScore = InGameTextStatistics.score;
            }
            // lm.LoadInitialLevel();
            // playerHealth.ResetPlayer();
            timerManager.ResetTimer();
        }
    }
}