using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem projectileParticules;
    [SerializeField] float range = 15f;
    
    Transform target;
    BuildBar buildBar;

    void Start()
    {
        buildBar = GetComponentInChildren<BuildBar>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (buildBar.IsBuilding == true) { return; }

        FindClosestTarget();

        if (target != null) {
            AimWeapon();
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

        weapon.LookAt(target);
    }

    void Attack(bool isActive)
    {
        var emissionModule = projectileParticules.emission;
        emissionModule.enabled = isActive;
    }
}
