using UnityEngine;

namespace Nightmare
{
    public class PetManager : PausibleObject
    {
        public GameObject healingPet;
        public GameObject attackingPet;

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
        }

    }
}