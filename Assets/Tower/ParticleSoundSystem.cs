using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ParticleSoundSystem : MonoBehaviour
{
    [SerializeField] AudioClip shotSound;

    AudioSource audioSource;
    ParticleSystem particleSystemOfGameObject;
    int currentNumberOfParticles = 0;

    // Start is called before the first frame update
    void Start()
    {
        particleSystemOfGameObject = GetComponent<ParticleSystem>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Suppression d'une particule
        if (particleSystemOfGameObject.particleCount < currentNumberOfParticles) {
            // TODO : Particle die sound
        }

        // Création d'une particule
        if (particleSystemOfGameObject.particleCount > currentNumberOfParticles) {
            audioSource.PlayOneShot(shotSound, 1f);
        }

        currentNumberOfParticles = particleSystemOfGameObject.particleCount;
    }
}
