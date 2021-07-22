using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;
    [SerializeField] [Tooltip("Adds amounts to maxHitPoints when enemy dies.")] int difficultyRamp = 1;
    [SerializeField] Canvas healthCanvas;
    [SerializeField] Image healthFillBar;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] ParticleSystem deathVFX;
    [SerializeField] float destroyVFXAfterSeconds = 0.5f;

    int currentHitPoints = 0;
    Enemy enemy;

    void OnEnable()
    {
        currentHitPoints = 0;

        UpdateHealthBar();
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        healthCanvas.transform.LookAt(transform.position + Camera.main.transform.rotation * -Vector3.back, Camera.main.transform.rotation * -Vector3.down);
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    void ProcessHit()
    {
        currentHitPoints++;

        UpdateHealthBar();

        if (currentHitPoints >= maxHitPoints) {
            Death();
        }
    }

    void UpdateHealthBar()
    {
        healthFillBar.fillAmount = (float) (maxHitPoints - currentHitPoints) / maxHitPoints;
    }

    void Death()
    {
        gameObject.SetActive(false);

        AudioSource.PlayClipAtPoint(deathSFX, transform.position);

        var deathParticleSystem = Instantiate(deathVFX, (transform.position + new Vector3(0, 3, 0)), Quaternion.identity);
        Destroy(deathParticleSystem, destroyVFXAfterSeconds);

        maxHitPoints += difficultyRamp;

        enemy.Rewards();
    }
}
