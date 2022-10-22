using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    GameManager gameManager;
    public TMP_InputField gridCountText;
    public TMP_Text matchCount;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void ChangeMatchCount(int count)
    {
        matchCount.text = "Match Count: " + count.ToString();
    }

    public void ChangeGrid()
    {
        if (gridCountText.text != "" && int.Parse(gridCountText.text.ToString()) > 2 && int.Parse(gridCountText.text.ToString()) <= 30)
        {
        int gridCount = int.Parse(gridCountText.text.ToString());
        gameManager.GenerateGame(gridCount);
        }
    }
}
