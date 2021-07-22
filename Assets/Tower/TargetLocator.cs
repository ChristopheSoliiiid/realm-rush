using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem projectileParticules;
    [SerializeField] float range = 15f;
    [SerializeField] float weaponRotationSpeed = 10f;
    
    Transform target;
    BuildBar buildBar;
    LevelLoader levelLoader;

    void Start()
    {
        buildBar = GetComponentInChildren<BuildBar>();
        levelLoader = FindObjectOfType<LevelLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (levelLoader.IsPlaying) {
            if (buildBar.IsBuilding == true) {
                Attack(false);
                return;
            }

            FindClosestTarget();

            if (target != null) {
                AimWeapon();
            } else {
                Attack(false);
            }
        } else {
            Attack(false);
        }
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies) {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxDistance) {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        target = closestTarget;
    }

    void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, target.position);

        Attack(targetDistance < range);

        weapon.rotation = Quaternion.Slerp(
            weapon.rotation,
            Quaternion.LookRotation(target.position - weapon.position),
            weaponRotationSpeed * Time.deltaTime
        );
    }

    void Attack(bool isActive)
    {
        var emissionModule = projectileParticules.emission;
        emissionModule.enabled = isActive;
    }
}
