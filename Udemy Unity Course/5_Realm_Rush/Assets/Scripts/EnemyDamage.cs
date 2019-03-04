using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] Collider collisionMesh;
    [SerializeField] int hitPoints = 10;
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] ParticleSystem deathParticlePrefab;
    [SerializeField] AudioClip hitEnemySFX;
    [SerializeField] AudioClip deathEnemySFX;

    AudioSource myAudioSource;

    // Use this for initialization
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }
    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoints<1)
        {
            KillEnemy();
        }
    }

    void ProcessHit()
    {
        myAudioSource.PlayOneShot(hitEnemySFX);
        hitPoints--;
        hitParticlePrefab.Play();
    }

    private void KillEnemy()
    {
        var vfx = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        vfx.Play();
        Destroy(vfx.gameObject, vfx.main.duration);
        AudioSource.PlayClipAtPoint(deathEnemySFX,Camera.main.transform.position);
        Destroy(gameObject);
    }


}
