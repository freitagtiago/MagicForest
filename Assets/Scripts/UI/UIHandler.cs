using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHandler : MonoBehaviour
{
    [SerializeField] TMP_Text coinsText;
    [SerializeField] TMP_Text livesText;
    [SerializeField] Image[] health = new Image[3];
    [SerializeField] Color colorWhenHit = Color.gray;

    private void Start()
    {
        GameSession gs = FindObjectOfType<GameSession>();
        gs?.SetUIHandler(this);
    }

    public void UpdateLives(int lives)
    {
        livesText.text = lives + " LIVES";
    }

    public void UpdateCoins(int coins)
    {
        coinsText.text = coins.ToString();
    }

    public void UpdateHealth(int currentHealth)
    {
        for(int i= 0; i< health.Length; i++)
        {
            if(i <= currentHealth - 1)
            {
                health[i].color = Color.white;
            }
            else
            {
                health[i].color = colorWhenHit;
            }
        }
    }
}
