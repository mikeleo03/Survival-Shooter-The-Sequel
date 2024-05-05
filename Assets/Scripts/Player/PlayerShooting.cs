using UnityEngine;
using UnityEngine.Events;
using System.Text;
using UnitySampleAssets.CrossPlatformInput;
using System.Collections.Generic;
using System;
using UnityEngine.InputSystem;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Nightmare
{
    public class PlayerShooting : PausibleObject
    {
        public GameObject grenade;
        public float grenadeSpeed = 200f;
        public float grenadeFireDelay = 0.5f;
        public float damagePercent = 1;
        int grenadeStock = 99;
        
        float timer;
        public List<GameObject> weaponsList;
        private Weapons currWeapon;
        private int currWeaponIdx;
        [SerializeField] Transform weaponPos;
        float realWeaponDamage;
  
        private UnityAction listener;

        // Input actions
        PlayerInput pInput;
        InputAction fire, grenadeAct, changeWeapon, scroll;

        void Awake ()
        {
            pInput = GetComponent<PlayerInput>();
            fire = pInput.actions["Fire"];
            grenadeAct = pInput.actions["Grenade"];
            changeWeapon = pInput.actions["ChangeWeapon"];
            scroll = pInput.actions["Scroll"];

            timer = 0;
            ChangeWeapon(0);
            AdjustGrenadeStock(0);

            listener = new UnityAction(CollectGrenade);

            EventManager.StartListening("GrenadePickup", CollectGrenade);

            StartPausible();
        }

        private void OnEnable()
        {
            fire.Enable();
            grenadeAct.Enable();
            changeWeapon.Enable();
            scroll.Enable();

            changeWeapon.performed += OnChangeWeaponInput;

        }

        private void OnDisable()
        {
            changeWeapon.performed -= OnChangeWeaponInput;

            fire.Disable();
            grenadeAct.Disable();
            changeWeapon.Disable();
            scroll.Disable();
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

            currWeapon.damagePerShot = Mathf.RoundToInt(realWeaponDamage * damagePercent);

            if (scroll.ReadValue<float>() != 0)
            {
                currWeaponIdx -= Mathf.RoundToInt(Mathf.Sign(scroll.ReadValue<float>()));
                ChangeWeapon(currWeaponIdx);
            }

            if (timer >= currWeapon.timeBetweenBullets && Time.timeScale != 0)
            {
#if !MOBILE_INPUT
                // If the Fire1 button is being press and it's time to fire...
                if (grenadeAct.IsPressed() && grenadeStock > 0)
                {
                    // ... shoot a grenade.
                    ShootGrenade();
                }

                // If the Fire1 button is being press and it's time to fire...
                else if (fire.IsPressed())
                {
                    // ... shoot the gun.
                    Shoot();
                }
#else
                foreach (var touch in Touch.activeTouches)
                {
                    if (touch.startScreenPosition != Vector2.zero)
                    {
                        if (touch.startScreenPosition.x > Screen.width / 2)
                        {
                            Shoot();
                        }
                    }
                }
#endif
            }

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

        void OnChangeWeaponInput(InputAction.CallbackContext ctx)
        {
            ChangeWeapon(Mathf.RoundToInt(ctx.ReadValue<float>()-1));
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
            realWeaponDamage = currWeapon.damagePerShot;
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