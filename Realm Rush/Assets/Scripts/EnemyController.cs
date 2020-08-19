using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] float hitPoints = 5f;
    [SerializeField] Collider collisionMesh;
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] ParticleSystem deathParticlePrefab;
    [SerializeField] AudioClip damagedSfx;
    [SerializeField] AudioClip enemyDeathSfx;

    AudioSource myAudioSource;

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }


    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();

        if (hitPoints <= 0)
        {
            KillEnemy();
        }
    }

    private void ProcessHit()
    {
        hitPoints--;
        hitParticlePrefab.Play();

        myAudioSource.PlayOneShot(damagedSfx);
    }

    private void KillEnemy()
    {
        var vfx = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        vfx.Play();

        //destroy particle after delay
        Destroy(vfx.gameObject, vfx.main.duration);
        AudioSource.PlayClipAtPoint(enemyDeathSfx, Camera.main.transform.position);
        Destroy(gameObject);
    }   
}
