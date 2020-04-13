using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text pointsText;
    [SerializeField] private Text endGameText;
    [SerializeField] private Button restartButton;

    public void ChangePointsText(int value)
    {
        pointsText.text = value.ToString();
    }

    public void EndGame()
    {
        endGameText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
}
