using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniTools;
using UnityEngine;

public class GameHUD : WindowBase
{
    [SerializeField] private TMP_Text currentSpeedText;
    public void Show(CharacterBaseView characterBaseView)
    {
        connections += currentSpeedText.SetTextReactive(characterBaseView.CurrentSpeed, speed => $"Current speed: {speed}");
    }
}
