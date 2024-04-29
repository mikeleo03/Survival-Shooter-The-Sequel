using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Nightmare {
    public class GameOverManager : MonoBehaviour {
        private PlayerHealth playerHealth;
        [SerializeField] private TimerManager timerManager;
        Animator anim;
        LevelManager lm;

        public float restartDelay = 10f;
        float restartTimer;

        void Awake () {
            playerHealth = FindObjectOfType<PlayerHealth>();
            anim = GetComponent<Animator>();
            lm = FindObjectOfType<LevelManager>();
        }

        void Update() {
            if (playerHealth.currentHealth <= 0) {
                anim.SetTrigger("GameOver");
                restartTimer += Time.deltaTime;

                if (restartTimer >= restartDelay) {
                    SceneManager.LoadScene("Menu");
                    anim.SetBool("GameOver", false);
                }
            }
        }

        private void ResetLevel() {
            ScoreManager.score = 0;
            timerManager.ResetTimer();
        }
    }
}