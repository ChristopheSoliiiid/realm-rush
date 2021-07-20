using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int goldReward = 25;
    [SerializeField] int damageToPlayer = 1;
    [SerializeField] AudioClip ramAttackSFX;
    [SerializeField] [Range(0f, 1f)] float volumeSFX = 0.5f;

    Bank bank;
    PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    public void RewardGold()
    {
        if (bank == null) { return; }

        bank.Deposit(goldReward);
    }

    public void InflictDamageToPlayer()
    {
        if (playerHealth == null) { return; }

        playerHealth.increaseHitTaken(damageToPlayer);

        AudioSource.PlayClipAtPoint(ramAttackSFX, Camera.main.transform.position, volumeSFX);
    }
}
