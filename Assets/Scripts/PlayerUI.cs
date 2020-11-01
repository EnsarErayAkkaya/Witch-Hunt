using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public Image healthImage;
    public Image fKeyImage;
    public TextMeshProUGUI mockCooldownText, restartText;
    public GameObject mockCoolDownPanel;

    public void UpdateHealthImage(float playerHealth)
    {   
        healthImage.fillAmount = playerHealth / 100f;
    }
    public void ActivatePickUpButton()
    {
        fKeyImage.gameObject.SetActive(true);
    }
    public void DeactivatePickUpButton()
    {
        fKeyImage.gameObject.SetActive(false);
    }
    public void ActivateMockCooldownPanel()
    {
        mockCoolDownPanel.SetActive(true);
    }
    public void DeactivateMockCooldownPanel()
    {
        mockCoolDownPanel.SetActive(false);
    }
    public void UpdateMockCooldownText(float t)
    {
        mockCooldownText.text = t.ToString("#.#");
    }

}
