using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;
    [SerializeField] [Tooltip("Adds amounts to maxHitPoints when enemy dies.")] int difficultyRamp = 1;
    [SerializeField] Image healthBar;
    [SerializeField] Image healthFillBar;

    int currentHitPoints = 0;
    Enemy enemy;

    void OnEnable()
    {
        currentHitPoints = 0;
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
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
            gameObject.SetActive(false);
            maxHitPoints += difficultyRamp;
            enemy.RewardGold();
        }
    }

    void UpdateHealthBar()
    {
        healthFillBar.fillAmount = (float) (maxHitPoints - currentHitPoints) / maxHitPoints;
    }
}
