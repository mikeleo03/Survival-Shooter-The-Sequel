using UnityEngine;
using UnityEngine.Events;
using System.Text;
using UnitySampleAssets.CrossPlatformInput;
using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine.Playables;
using UnityEngine.InputSystem;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Nightmare
{
    public class PlayerShooting : PausibleObject, IDataPersistance
    {
        public GameObject grenade;
        public float grenadeSpeed = 200f;
        public float grenadeFireDelay = 5f;
        public float damagePercent = 1;
        int grenadeStock = int.MaxValue;
        
        float timer, grenadeTimer;
        public List<GameObject> weaponsList;
        private Weapons currWeapon;
        private int currWeaponIdx;
        [SerializeField] Transform weaponPos;
        float realWeaponDamage;
        private bool isShootingGrenade;

        // For skill cutscene
        PlayableDirector director;
  
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
            grenadeTimer = 0;
            ChangeWeapon(0);
            isShootingGrenade = false;

            GameObject directorObject = GameObject.Find("Timeline");

            // Get the PlayableDirector component
            if (directorObject != null)
            {
                director = directorObject.GetComponent<PlayableDirector>();
            }
            else
            {
                Debug.Log("Director Object Null");
            }

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
            grenadeTimer += Time.deltaTime;

            currWeapon.damagePerShot = Mathf.RoundToInt(realWeaponDamage * damagePercent);

            if (scroll.ReadValue<float>() != 0)
            {
                IncOrDecWeapon(-Mathf.RoundToInt(Mathf.Sign(scroll.ReadValue<float>())));
            }

#if !MOBILE_INPUT
            // If the Fire1 button is being press and it's time to fire...
            if (grenadeAct.IsPressed() && grenadeStock > 0)
            {
                // ... shoot a grenade.
                ShootGrenade();
            }

            
            // If the Fire1 button is being press and it's time to fire...
            if (fire.IsPressed())
            {
                // ... shoot the gun.
                Shoot();
            }
#else
                foreach (var touch in Touch.activeTouches)
                {
                    if (touch.startScreenPosition != Vector2.zero)
                    {
                        if (touch.startScreenPosition.x > Screen.width / 2 &&
                            touch.startScreenPosition.x < Screen.width-Screen.width/7)
                        {
                            Shoot();
                        }
                    }
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

        public void IncOrDecWeapon(int amount)
        {
            currWeaponIdx += amount;
            ChangeWeapon(currWeaponIdx);
        }

        void Shoot ()
        {
            if (timer >= currWeapon.timeBetweenBullets && Time.timeScale != 0 && !isShootingGrenade)
            {
                // Reset the timer.
                timer = 0f;
                currWeapon.Shoot();
            }
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
        }

        public void ShootGrenade()
        {
            if (!isShootingGrenade && grenadeTimer >= grenadeFireDelay)
            {
                isShootingGrenade = true;
                StartCoroutine(ShootCutscene());
            }
            
            //GameObject clone = Instantiate(grenade, transform.position, Quaternion.identity);
            //Grenade grenadeClone = clone.GetComponent<Grenade>();
            //grenadeClone.Shoot(grenadeSpeed * transform.forward);
        }

        IEnumerator ShootCutscene()
        {
            // Freeze the enemies
            FreezeEnemies();

            // Play the cutscene
            director.Play();

            // Wait for the cutscene to finish
            yield return new WaitForSeconds((float)director.duration);

            // Unfreeze the enemies
            UnfreezeEnemies();

            for (int i=0; i<30; i++)
            {
                GameObject clone = PoolManager.Pull("Grenade", transform.position, Quaternion.identity);
                EventManager.TriggerEvent("ShootGrenade", grenadeSpeed * transform.forward);
            }
            grenadeTimer = 0;
            isShootingGrenade = false;
        }

        public void FreezeEnemies()
        {
            EnemyMovement.isFreeze = true;
        }

        public void UnfreezeEnemies()
        {
            EnemyMovement.isFreeze = false;
        }

        public void ResetPlayerDamage()
        {
            damagePercent = 1;
        }

        public void ActivateCheatOneHitKill()
        {
            damagePercent = 100000;
        }

        public void LoadData(GameData data)
        {
            this.damagePercent = data.damagePercent;
        }

        public void SaveData(ref GameData data)
        {
            data.damagePercent = this.damagePercent;
        }

    }
}