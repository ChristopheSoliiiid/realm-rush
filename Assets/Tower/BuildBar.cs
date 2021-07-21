using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildBar : MonoBehaviour
{
    [SerializeField] Image fillBar;

    float buildDuration = 1f;
    bool isBuilding = true;

    public bool IsBuilding { get { return isBuilding; } }

    // Update is called once per frame
    void Update()
    {
        if (isBuilding) {
            fillBar.fillAmount += 1f / buildDuration * Time.deltaTime;

            if (fillBar.fillAmount >= 1f) {
                isBuilding = false;
                gameObject.SetActive(false);
            }
        }
    }

    public void SetBuildDuration(float duration)
    {
        buildDuration = duration;
    }
}
