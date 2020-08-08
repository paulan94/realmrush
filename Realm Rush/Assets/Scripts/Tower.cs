using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] Transform targetEnemy;

    [SerializeField] float towerRange = 45f;
    [SerializeField] ParticleSystem particleSystem;



    private void Start()
    {
        particleSystem.enableEmission = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (targetEnemy)
        {
            HandleAttack();
        }
        else
        {
            ShootProjectile(false);
        }
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
        //turn particle system on and handle collisions
        emissionModule.enabled = shouldShoot;
        
    }
}
