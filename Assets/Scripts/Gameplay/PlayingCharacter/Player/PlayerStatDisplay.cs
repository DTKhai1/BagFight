using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatDisplay : MonoBehaviour
{
    public TMP_Text _healthText;
    public TMP_Text _shieldText;
    public void UpdateShieldText(float amount)
    {
        _shieldText.text = ((int)amount).ToString();
    }
    public void UpdateHealthText(float amount)
    {
        _healthText.text = ((int)amount).ToString();
    }
}
