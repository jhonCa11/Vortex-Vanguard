using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update

    //Shooting
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float ShootingDelay = 2f;

    //Burst
    public int bulletsPerBurst = 3;
    public int burstBulletsLeft;

    //Spread
    public float spreadIntensity;

    //Bullet    
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30;
    public float bulletPrefabLifeTime = 3f;

    public GameObject muzzleEffect;

    private Animator animator;

    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }

    public ShootingMode currentShootingMode;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Left mouse click
        if (currentShootingMode == ShootingMode.Auto)
        {
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if (currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst)
        {
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }
        
        if (readyToShoot && isShooting)
        {
            burstBulletsLeft = bulletsPerBurst;
            FireWeapon();
        }
    }

    private void FireWeapon()
    {

        //Muzzle flash
        muzzleEffect.GetComponent<ParticleSystem>().Play();

        //Play the shooting animation
        animator.SetTrigger("RECOIL");

        //Play the shooting sound
        SoundManager.Instance.shootingSound_02.Play();

        readyToShoot = false;
        
        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        //Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        //Pointng the bullet
        bullet.transform.forward = shootingDirection;
        //Shoot the bullet
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);
        //Destroy the bullet after a certain amount of time
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));

        if (allowReset)
        {
            Invoke("ResetShot", ShootingDelay);
            allowReset = false;
        }

        //Burst Mode
        if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1)
        {
            burstBulletsLeft--;
            Invoke("FireWeapon", ShootingDelay);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    public Vector3 CalculateDirectionAndSpread()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit))
        {
            //Hitting something
            targetPoint = hit.point;
        }
        else
        {
            //Shoting at the air
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;

        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        return direction + new Vector3(x, y, 0);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(bullet);
    }
}
