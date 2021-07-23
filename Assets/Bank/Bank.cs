using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI balanceText;
    [SerializeField] int startingBalance = 150;
    [SerializeField] float timingUpdateText = 0.02f;
    [SerializeField] TextMeshProUGUI goldSpentEndScreenText;

    int displayBalance;
    int currentBalance;
    int goldSpent = 0;

    public int CurrentBalance { get { return currentBalance; } }

    void Awake()
    {
        displayBalance = currentBalance = startingBalance;

        balanceText.text = displayBalance.ToString();

        StartCoroutine(UpdateBalanceText());
    }

    public void Deposit(int amount)
    {
        displayBalance = currentBalance;
        currentBalance += Mathf.Abs(amount); // Mathf.Abs converti tous les nombres en nombres positif (ex: Mathf.Abs(-10) = 10)

        StartCoroutine(UpdateBalanceText());
    }

    public void Withdraw(int amount)
    {
        displayBalance = currentBalance;
        currentBalance -= Mathf.Abs(amount);

        goldSpent += Mathf.Abs(amount);
        goldSpentEndScreenText.text = $"You spent {goldSpent} gold!";

        StartCoroutine(UpdateBalanceText());
    }

    IEnumerator UpdateBalanceText()
    {
        while (displayBalance != currentBalance) {
            if (displayBalance < currentBalance) {
                displayBalance++;
            } else {
                displayBalance--;
            }

            balanceText.text = displayBalance.ToString();

            yield return new WaitForSecondsRealtime(timingUpdateText);
        }
    }
}
