using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;
    [SerializeField] float buildDelay = 3f;

    BuildBar progressBar;

    public int Cost { get { return cost; } }

    void Awake()
    {
        progressBar = GetComponentInChildren<BuildBar>();
        progressBar.SetBuildDuration(buildDelay);
    }

    void Start()
    {
        StartCoroutine(Build());
    }

    IEnumerator Build()
    {
        List<Transform> transformChildren = new List<Transform>(); // Liste des enfants de Type EXACTEMENT Transform

        foreach (Transform child in transform) {
            if (child.GetType() == typeof(UnityEngine.Transform)) {
                transformChildren.Add(child); // Ajout dans la liste

                child.gameObject.SetActive(false);

                foreach (Transform grandChild in child) {
                    transformChildren.Add(grandChild); // Ajout dans la liste

                    grandChild.gameObject.SetActive(false);
                }
            }
        }

        float delayBetweenParts = buildDelay / transformChildren.Count; // Calcul du delai entre chaque partie à réactiver

        foreach (Transform child in transformChildren) {
            child.gameObject.SetActive(true);

            yield return new WaitForSeconds(delayBetweenParts);

            foreach (Transform grandChild in child) {
                grandChild.gameObject.SetActive(true);
            }
        }
    }

    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();
        GameObject parentContainer = GameObject.FindGameObjectWithTag("Player");

        if (bank != null && bank.CurrentBalance >= cost) {
            GameObject instantiatedTower = Instantiate(tower.gameObject, position, Quaternion.identity, parentContainer.transform);

            bank.Withdraw(cost);

            return true;
        }

        return false;
    }
}
