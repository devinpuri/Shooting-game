using UnityEngine;
using System.Collections;

public class BulletShooter : MonoBehaviour
{
    public enum FiringMode
    {
        SemiAuto,
        FullAuto
    }

    [Header("Bullet Settings")]
    public GameObject bulletPrefab;      // Prefab of the bullet to shoot.
    public Transform bulletSpawn;        // Transform from which the bullet will be spawned.
    public float bulletSpeed = 10f;      // Speed at which the bullet travels.
    public float bulletLifeTime = 2f;    // Time (in seconds) after which the bullet is destroyed.

    [Header("Firing Settings")]
    public FiringMode firingMode = FiringMode.SemiAuto; // Choose between semi-auto and full-auto.
    public float fireRate = 0.2f;        // Minimum time between shots (or bursts).
    private float nextFireTime = 0f;     // Timer to control the firing rate.

    [Header("Shotgun Settings")]
    public bool shotgunMode = false;         // Toggle for shotgun mode.
    public int shotgunBulletCount = 5;       // Number of bullets per shotgun shot.
    public float shotgunSpreadAngle = 30f;   // Total spread angle for shotgun mode.

    [Header("Burst Mode Settings")]
    public bool burstMode = false;       // Toggle for burst mode.
    public int burstCount = 3;           // Number of bullets in a burst.
    public float burstInterval = 0.1f;   // Delay between each bullet in a burst.
    private bool isBursting = false;     // Prevents overlapping bursts.


    void Update()
    {
        // Toggle modes with keys.
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFiringMode();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            ToggleShotgunMode();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleBurstMode();
        }

        bool firePressed = Input.GetButtonDown("Fire1");
        bool fireHeld = Input.GetButton("Fire1");

        // When burst mode is active, use the burst routine.
        if (burstMode)
        {
            // If in full auto, fire bursts continuously while holding the fire button.
            if (firingMode == FiringMode.FullAuto)
            {
                if (fireHeld && Time.time >= nextFireTime && !isBursting)
                {
                    StartCoroutine(ShootBurst());
                    nextFireTime = Time.time + fireRate;
                }
            }
            // Otherwise, in semi-auto burst mode fire on button press.
            else
            {
                if (firePressed && Time.time >= nextFireTime && !isBursting)
                {
                    StartCoroutine(ShootBurst());
                    nextFireTime = Time.time + fireRate;
                }
            }
        }
        // When burst mode is off, behave as before.
        else
        {
            if (firingMode == FiringMode.FullAuto)
            {
                if (fireHeld && Time.time >= nextFireTime)
                {
                    Shoot();
                    nextFireTime = Time.time + fireRate;
                }
            }
            else // SemiAuto
            {
                if (firePressed && Time.time >= nextFireTime)
                {
                    Shoot();
                    nextFireTime = Time.time + fireRate;
                }
            }
        }
    }

    // Determines whether to shoot as a shotgun or a single bullet.
    void Shoot()
    {
        if (shotgunMode)
        {
            ShootShotgun();
        }
        else
        {
            ShootBullet();
        }
    }

    // Shoots a single bullet.
    void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = bulletSpawn.up * bulletSpeed;
        }
        Destroy(bullet, bulletLifeTime);
    }

    // Shoots multiple bullets in a spread (shotgun mode).
    void ShootShotgun()
    {
        float angleStep = shotgunBulletCount > 1 ? shotgunSpreadAngle / (shotgunBulletCount - 1) : 0f;
        float startingAngle = -shotgunSpreadAngle / 2f;
        for (int i = 0; i < shotgunBulletCount; i++)
        {
            float currentAngle = startingAngle + angleStep * i;
            Quaternion bulletRotation = bulletSpawn.rotation * Quaternion.Euler(0, 0, currentAngle);
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletRotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = bullet.transform.up * bulletSpeed;
            }
            Destroy(bullet, bulletLifeTime);
        }
    }

    // Coroutine to fire a burst of bullets.
    IEnumerator ShootBurst()
    {
        isBursting = true;
        for (int i = 0; i < burstCount; i++)
        {
            if (shotgunMode)
            {
                ShootShotgun();
            }
            else
            {
                ShootBullet();
            }
            yield return new WaitForSeconds(burstInterval);
        }
        isBursting = false;
    }

    // Toggle between semi-auto and full-auto firing modes.
    void ToggleFiringMode()
    {
        firingMode = (firingMode == FiringMode.SemiAuto) ? FiringMode.FullAuto : FiringMode.SemiAuto;
        Debug.Log(firingMode == FiringMode.FullAuto ? "Switched to Full Auto mode" : "Switched to Semi Auto mode");
    }

    // Toggle shotgun mode on or off.
    void ToggleShotgunMode()
    {
        shotgunMode = !shotgunMode;
        Debug.Log(shotgunMode ? "Shotgun mode enabled" : "Shotgun mode disabled");
    }

    // Toggle burst mode on or off.
    void ToggleBurstMode()
    {
        burstMode = !burstMode;
        Debug.Log(burstMode ? "Burst mode enabled" : "Burst mode disabled");
    }
}
