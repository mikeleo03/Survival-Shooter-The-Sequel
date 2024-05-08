using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Events;
using Unity.VisualScripting;

namespace Nightmare
{
    public class EnemyPetAction : MonoBehaviour
    {
        EnemyAttack attack;
        // Animator anim; 
        void Awake ()
        {
            attack = GetComponentInParent<EnemyAttack>();
            attack.Buff();
            Debug.Log("Buffed!");
        }

        void OnDestroy()
        {
            attack.Debuff();
            Debug.Log("Debuffed!");

        }
    }
}