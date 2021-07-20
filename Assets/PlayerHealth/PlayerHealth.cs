using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] [Range(1,10)] int maxHealthPoint = 5;
    [SerializeField] Sprite heartFullSprite;
    [SerializeField] Sprite heartEmptySprite;
    [SerializeField] Vector2 spriteSize;

    public int currentHitTaken = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < maxHealthPoint; i++) {
            CreateHealthUI(i);
        }
    }

    void CreateHealthUI(int position)
    {
        GameObject HeartGameObject = new GameObject(); // Create the GameObject
        Image heartImg = HeartGameObject.AddComponent<Image>(); // Add the Image Component script
        heartImg.sprite = heartFullSprite; // Set the Sprite of the Image Component on the new GameObject

        RectTransform HGORT = HeartGameObject.GetComponent<RectTransform>();

        HGORT.SetParent(gameObject.transform); // Assign the newly created Image GameObject as a Child of the Parent Panel.

        HGORT.anchoredPosition = new Vector3(position * spriteSize.x, 0);
        HGORT.localScale = new Vector3(1, 1, 1);
        HGORT.sizeDelta = spriteSize;
        HeartGameObject.SetActive(true); // Activate the GameObject
    }

    void UpdateHealth()
    {
        Image[] images = gameObject.GetComponentsInChildren<Image>();

        for (int i = 0; i < currentHitTaken; i++) {
            images[maxHealthPoint - currentHitTaken].sprite = heartEmptySprite;
        }
    }

    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void increaseHitTaken(int value)
    {
        currentHitTaken += value;

        UpdateHealth();

        if (currentHitTaken >= maxHealthPoint) {
            ReloadScene();
        }
    }
}
