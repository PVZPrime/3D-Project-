using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

namespace player
{
    //https://www.youtube.com/watch?v=wZ2UUOC17AY 5:50
#if ENABLE_INPUT_SYSTEM
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class ability1 : MonoBehaviour
    {
        public GameObject Bullet;
        public float ShootForce, UpwardForce;
        public float TimeBetweenShooting, Spread, ReloadTime, TimeBetweenShots;
        public int MagSize, BulletsPerTap;
        int bulletsLeft, BulletsShot;
        bool Shooting, ReadyToShoot, Reloading;

        public Camera Cam;
        public Transform AttackPoint;

        public bool AllowInvoke = true;
        private StarterAssetsInputs _input;

        public void Awake()
        {
            bulletsLeft = MagSize;
            ReadyToShoot = true;
        }
        void Start()
        {
            _input = GetComponent<StarterAssetsInputs>();
        }

        void Update()
        {
            MyInput();
            //if (_input.shoot)

        }
        private void MyInput()
        {
            Shooting = _input.shoot;
            if (ReadyToShoot && Shooting && !Reloading && bulletsLeft > 0)
            {
                BulletsShot = 0;

                Shoot();
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

            GameObject currentBullet = Instantiate(Bullet, AttackPoint.position, Quaternion.identity);

            currentBullet.transform.forward = directionWithSpread.normalized;

            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * ShootForce, ForceMode.Impulse);
            currentBullet.GetComponent<Rigidbody>().AddForce(Cam.transform.up * UpwardForce, ForceMode.Impulse);

            bulletsLeft--;
            BulletsShot++;

        }
    }
}
