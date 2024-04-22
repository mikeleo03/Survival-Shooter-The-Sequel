﻿using UnityEngine;
using UnityEngine.Events;
using System.Text;
using UnitySampleAssets.CrossPlatformInput;
using System.Collections.Generic;
using System;

namespace Nightmare
{
    public class PlayerShooting : PausibleObject
    {
        public GameObject grenade;
        public float grenadeSpeed = 200f;
        public float grenadeFireDelay = 0.5f;
        int grenadeStock = 99;
        
        float timer;
        public List<GameObject> weaponsList;
        private Weapons currWeapon;
        private int currWeaponIdx;
        [SerializeField] Transform weaponPos;
  
        private UnityAction listener;

        void Awake ()
        {
            timer = 0;
            ChangeWeapon(0);
            AdjustGrenadeStock(0);

            listener = new UnityAction(CollectGrenade);

            EventManager.StartListening("GrenadePickup", CollectGrenade);

            StartPausible();
        }

        void OnDestroy()
        {
            EventManager.StopListening("GrenadePickup", CollectGrenade);
            StopPausible();
        }

        void Update ()
        {
            if (isPaused)
                return;

            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;

            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                currWeaponIdx -= Mathf.RoundToInt(Mathf.Sign(Input.GetAxis("Mouse ScrollWheel")));
                ChangeWeapon(currWeaponIdx);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangeWeapon(0);
            } else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChangeWeapon(1);
            } else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ChangeWeapon(2);
            }

#if !MOBILE_INPUT
            if (timer >= currWeapon.timeBetweenBullets && Time.timeScale != 0)
            {
                // If the Fire1 button is being press and it's time to fire...
                if (Input.GetButton("Fire2") && grenadeStock > 0)
                {
                    // ... shoot a grenade.
                    ShootGrenade();
                }

                // If the Fire1 button is being press and it's time to fire...
                else if (Input.GetButton("Fire1"))
                {
                    // ... shoot the gun.
                    Shoot();
                }
            }
            
#else
            // If there is input on the shoot direction stick and it's time to fire...
            if ((CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0) && timer >= timeBetweenBullets)
            {
                // ... shoot the gun
                Shoot();
            }
#endif
            // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
            if(timer >= currWeapon.timeBetweenBullets * currWeapon.effectsDisplayTime)
            {
                // ... disable the effects.
                DisableEffects ();
            }
        }


        public void DisableEffects ()
        {
            currWeapon.DisableEffects();
        }

        void ChangeWeapon(int idx)
        {
            if (currWeapon != null)
            {
                DisableEffects();
                Destroy(currWeapon.gameObject);
            }
            if (idx < 0)
            {
                idx = weaponsList.Count - 1;
            } else if (idx >= weaponsList.Count) 
            { 
                idx = 0;
            }
            currWeaponIdx = idx;
            GameObject currWeaponObj = Instantiate(weaponsList[currWeaponIdx], transform);
            currWeaponObj.transform.SetPositionAndRotation(transform.position, transform.rotation);
            currWeapon = currWeaponObj.GetComponent<Weapons>();
            if (currWeaponIdx != 2)
            {
                currWeapon.weaponModel.transform.position = weaponPos.position;
            }
        }

        void Shoot ()
        {
            // Reset the timer.
            timer = 0f;
            currWeapon.Shoot();
        }

        //private void ChangeGunLine(float midPoint)
        //{
        //    AnimationCurve curve = new AnimationCurve();

        //    curve.AddKey(0f, 0f);
        //    curve.AddKey(midPoint, 0.5f);
        //    curve.AddKey(1f, 1f);

        //    gunLine.widthCurve = curve;
        //}

        public void CollectGrenade()
        {
            AdjustGrenadeStock(1);
        }

        private void AdjustGrenadeStock(int change)
        {
            grenadeStock += change;
            GrenadeManager.grenades = grenadeStock;
        }

        void ShootGrenade()
        {
            AdjustGrenadeStock(-1);
            timer = currWeapon.timeBetweenBullets - grenadeFireDelay;
            GameObject clone = PoolManager.Pull("Grenade", transform.position, Quaternion.identity);
            EventManager.TriggerEvent("ShootGrenade", grenadeSpeed * transform.forward);
            //GameObject clone = Instantiate(grenade, transform.position, Quaternion.identity);
            //Grenade grenadeClone = clone.GetComponent<Grenade>();
            //grenadeClone.Shoot(grenadeSpeed * transform.forward);
        }
    }
}