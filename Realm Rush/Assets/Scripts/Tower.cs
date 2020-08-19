using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//todo add sound
public class Tower : MonoBehaviour
{
    //Parameters
    [SerializeField] Transform objectToPan;
    [SerializeField] float towerRange = 45f;
    [SerializeField] ParticleSystem particleSystem;

    //State of each tower
    [SerializeField] Transform targetEnemy;

    public Waypoint baseWaypoint;

    private void Start()
    {
        particleSystem.enableEmission = false;
    }
    // Update is called once per frame
    void Update()
    {
        SetTargetEnemy();
        if (targetEnemy)
        {
            HandleAttack();
        }
        else
        {
            ShootProjectile(false);
        }
    }

    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyController>();
        if (sceneEnemies.Length == 0) { return; }

        Transform closestEnemy = sceneEnemies[0].transform;

        foreach (EnemyController testEnemy in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
        }
        targetEnemy = closestEnemy;
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        var distToA = Vector3.Distance(transform.position, transformA.position);
        var distToB = Vector3.Distance(transform.position, transformB.position);

        if (distToA < distToB) { return transformA; }
        return transformB;
    }

    private void HandleAttack()
    {
        float dist = Vector3.Distance(objectToPan.position, targetEnemy.position);

        if (dist <= towerRange)
        {
            objectToPan.LookAt(targetEnemy);
            ShootProjectile(true);
        }
        else
        {
            ShootProjectile(false);
        }
    }

    private void ShootProjectile(bool shouldShoot)
    {
        var emissionModule = particleSystem.emission;
        emissionModule.enabled = shouldShoot;
        
    }
}
