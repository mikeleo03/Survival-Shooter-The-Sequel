using UnityEngine;
using UnityEngine.Events;

namespace Nightmare
{
    public class PausibleObject : MonoBehaviour
    {
        public UnityAction<bool> pauseListener;
        internal bool isPaused = false;

        public void StartPausible()
        {
            pauseListener = new UnityAction<bool>(Pause);

            EventManager.StartListening("Pause", Pause);
        }

        public void StopPausible()
        {
            EventManager.StopListening("Pause", Pause);
        }

        public void Pause(bool state)
        {
            isPaused = state;
            if (isPaused)
            {
                OnPause();
            }
            else{
                OnUnPause();
            }
        }

        virtual public void OnPause()
        {

        }

        virtual public void OnUnPause()
        {

        }
    }
}