using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI stateText;

    public void UpdateStateUI()
    {
        stateText.text = "GameState: " + GameManager.Instance.State.ToString();
    }
}
