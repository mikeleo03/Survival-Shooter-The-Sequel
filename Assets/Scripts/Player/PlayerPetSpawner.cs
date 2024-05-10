using UnityEngine;
using UnityEngine.InputSystem;

namespace Nightmare
{
    public class PlayerPetSpawner : PausibleObject
    {
        public GameObject healingPet;
        public GameObject attackingPet;
        public GameObject enemyPet;

        void Update()
        {
            if (Keyboard.current.qKey.wasPressedThisFrame)
            {
                Instantiate(healingPet, transform.position, Quaternion.identity);
            }
            if (Keyboard.current.bKey.wasPressedThisFrame)
            {
                Instantiate(attackingPet, transform.position, Quaternion.identity);
            }
            if (Keyboard.current.vKey.wasPressedThisFrame)
            {
                Instantiate(enemyPet, transform.position, Quaternion.identity);
            }
        }

    }
}