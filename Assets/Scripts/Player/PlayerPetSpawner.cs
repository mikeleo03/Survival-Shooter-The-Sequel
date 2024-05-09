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
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Instantiate(healingPet, transform.position, Quaternion.identity);
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                Instantiate(attackingPet, transform.position, Quaternion.identity);
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                Instantiate(enemyPet, transform.position, Quaternion.identity);
            }
        }

    }
}