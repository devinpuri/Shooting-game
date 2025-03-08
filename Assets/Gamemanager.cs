using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Gamemanager : MonoBehaviour
{
    public GameObject gameoverscreen;
    public TextMeshProUGUI gameOvertext;
    public TextMeshProUGUI text;
    public int score;
    public void Updatescore(int point)
    {
        score = score+point;
        text.text = "score:" + score;
        gameOvertext.text = "score:" + score; 
    }
    public void gameover() { gameoverscreen.SetActive(true); }
    
}
