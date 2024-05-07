using UnityEngine;

namespace Nightmare
{
    public class PlayerPetSpawner : PausibleObject
    {
        public GameObject healingPet;
        public GameObject attackingPet;
        public GameObject enemyPet;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Instantiate(healingPet, transform.position, Quaternion.identity);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Instantiate(attackingPet, transform.position, Quaternion.identity);
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                Instantiate(enemyPet, transform.position, Quaternion.identity);
            }
        }

    }
}