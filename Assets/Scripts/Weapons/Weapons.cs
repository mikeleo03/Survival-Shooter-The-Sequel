using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public abstract class Weapons : MonoBehaviour
{
    public Mesh weaponMesh;
    public int damagePerShot;
    public float timeBetweenBullets;
    public float range;
    public float effectsDisplayTime;
    public int shootableMask;

    [HideInInspector] public AudioSource triggerAudio;

    private void Start()
    {
        shootableMask = LayerMask.GetMask("Shootable", "Enemy");
    }

    public abstract void Shoot();
    public abstract void DisableEffects();
}
