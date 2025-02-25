using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

namespace player
{
    //https://www.youtube.com/watch?v=wZ2UUOC17AY
#if ENABLE_INPUT_SYSTEM
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class PlayerShoot : MonoBehaviour
    {
        [Header("Bullet settings")]
        public float ShootForce;
        public float UpwardForce, AbilityForce;
        [Header("Gun Stats")]
        public float TimeBetweenShooting;
        public float Spread, ReloadTime, TimeBetweenShots;
        public int MagSize, BulletsPerTap;
        public int bulletsLeft  {  get; set; }
        public int BulletsAvalible;
        int BulletsShot;
        bool Shooting, ReadyToShoot; 
        public bool Reloading {  get; set; }
        public bool AutoReload = true;

        [Header("Ability Stats")]
        public float TimeBetweenAbilities;
        public float saveCoolDown;
        public int Ability1Bullets;
        bool Ability1Active, ReadyToActivate; 
        public bool SaveCoolDownActive {  get; set; }

        [Header("Referance Objects")]
        public Camera Cam;
        public Transform AttackPoint;

        [Header("Debuging")]
        public bool AllowInvoke = true;
        public bool AllowInvokeAbility = true;
        private StarterAssetsInputs _input;

        [Header("Graphics")]
        public GameObject MuzzleFlash;
        public TextMeshProUGUI AmmoDisplay;

        public void Awake()
        {
            bulletsLeft = MagSize;
            ReadyToShoot = true;
            ReadyToActivate = true;
        }
        void Start()
        {
            saveCoolDown = TimeBetweenAbilities;
            _input = GetComponent<StarterAssetsInputs>();
        }

        void Update()
        {
            MyInput();
            if (AmmoDisplay != null)
                AmmoDisplay.SetText(bulletsLeft / BulletsPerTap + " / " + MagSize / BulletsPerTap);
            if(SaveCoolDownActive)
            {
                saveCoolDown -= Time.deltaTime;
            }

        }
        private void MyInput()
        {
            if (_input.Reload /*&& bulletsLeft < MagSize*/ && !Reloading) Reload();
            if (AutoReload)
            {
                if (ReadyToShoot && Shooting && !Reloading && bulletsLeft <= 0) Reload();
            }


            Shooting = _input.shoot;
            if (ReadyToShoot && Shooting && !Reloading && bulletsLeft > 0)
            {
                BulletsShot = 0;
                Shoot();
            }
            Ability1Active = _input.Ability1;
            if (ReadyToActivate && Ability1Active && !Reloading && bulletsLeft > Ability1Bullets)
            {
                BulletsShot = 0;
                Ability1();
            }
        }
        private void Shoot()
        {
            ReadyToShoot = false;
            Ray ray = Cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit)) targetPoint = hit.point;
            else targetPoint = ray.GetPoint(75);

            Vector3 directionWithoutSpread = targetPoint - AttackPoint.position;

            float x = Random.Range(-Spread, Spread);
            float y = Random.Range(-Spread, Spread);

            Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

            GameObject currentBullet = ObjectPooling.SharedInstance.GetPooledObject();
            if (currentBullet != null)
            {
                currentBullet.transform.position = AttackPoint.transform.position;
                currentBullet.transform.rotation = AttackPoint.transform.rotation;
                currentBullet.SetActive(true);

                currentBullet.transform.forward = directionWithSpread.normalized;
            }
            currentBullet.transform.forward = directionWithSpread.normalized;

            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * ShootForce, ForceMode.Impulse);
            currentBullet.GetComponent<Rigidbody>().AddForce(Cam.transform.up * UpwardForce, ForceMode.Impulse);

            if (MuzzleFlash != null)
                Instantiate(MuzzleFlash, AttackPoint.position, Quaternion.identity);

            bulletsLeft--;
            BulletsShot++;

            if (AllowInvoke)
            {
                //Invoke("ResetShot", 3f); calls function after 3 seconds
                Invoke("ResetShot", TimeBetweenShooting);
                AllowInvoke = false;
            }
            if (BulletsShot < BulletsPerTap && bulletsLeft > 0)
                Invoke("Shoot", TimeBetweenShots);
        }
        private void Ability1()
        {
            ReadyToActivate = false;
            Ray ray = Cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit)) targetPoint = hit.point;
            else targetPoint = ray.GetPoint(75);

            Vector3 directionWithoutSpread = targetPoint - AttackPoint.position;

            float x = Random.Range(-Spread, Spread);
            float y = Random.Range(-Spread, Spread);

            Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

            GameObject currentBullet = ObjectPooling.SharedInstance.GetPooledObject();
            if (currentBullet != null)
            {
                currentBullet.transform.position = AttackPoint.transform.position;
                currentBullet.transform.rotation = AttackPoint.transform.rotation;
                currentBullet.SetActive(true);

                currentBullet.transform.forward = directionWithSpread.normalized;
            }
            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * AbilityForce, ForceMode.Impulse);
            currentBullet.GetComponent<Rigidbody>().AddForce(Cam.transform.up * UpwardForce, ForceMode.Impulse);

            if (MuzzleFlash != null)
                Instantiate(MuzzleFlash, AttackPoint.position, Quaternion.identity);

            bulletsLeft--;
            BulletsShot++;

            if (AllowInvokeAbility)
            {
                if (saveCoolDown == TimeBetweenAbilities)
                {
                    //Invoke("ResetShot", 3f); calls function after 3 seconds
                    Invoke("ResetAbility", TimeBetweenAbilities);
                    AllowInvokeAbility = false;
                    SaveCoolDownActive = true;
                }
            }
            if (BulletsShot < Ability1Bullets && bulletsLeft > 0)
                Invoke("Ability1", TimeBetweenShots);
        }
        private void ResetAbility()
        {
            SaveCoolDownActive = false;
            saveCoolDown = TimeBetweenAbilities;
            ReadyToActivate = true;
            AllowInvokeAbility = true;
        }
        private void ResetShot()
        {
            ReadyToShoot = true;
            AllowInvoke = true;
        }
        private void Reload()
        {
            Reloading = true;
            if(Reloading) Invoke("ReloadFinished", ReloadTime);
        }
        private void ReloadFinished()
        {
            if (BulletsAvalible >= MagSize)
            {
                bulletsLeft = MagSize;
                BulletsAvalible -= MagSize;
            } else
            {
                //get the billets left then only add an amout that will make it equal to at max MagSize
                bulletsLeft = BulletsAvalible;
                BulletsAvalible = 0;
            }
            Reloading = false;
        }
    }
}
