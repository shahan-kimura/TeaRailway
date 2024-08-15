using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI phaseText; // Phase情報を表示するUI Text
    [SerializeField] private TextMeshProUGUI currentEnemyText; // 残存Enemy数情報を表示するUI Text
    [SerializeField] private Image hpGage;                      //hpゲージ画像
    [SerializeField] private GameObject gameOverUI;      //ゲームオーバーUI


    [SerializeField] private PhaseManager phaseManager; // PhaseManagerへの参照
    [SerializeField] private StatusManager playerStatusManager; // PhaseManagerへの参照

    private void Start()
    {
        gameOverUI.SetActive(false);
    }

    void Update()
    {
        // PhaseManagerから現在のPhaseを取得してUIに表示
        phaseText.text = "Current Phase: " + phaseManager.CurrentPhase.ToString();
        currentEnemyText.text = "CurrentEnemy: " + phaseManager.CurrentEnemy.ToString();
        hpGage.fillAmount = (float)playerStatusManager.HP / (float)playerStatusManager.MaxHP;
        
    }

    public void GameOver() 
    {
        Debug.Log("gameover");
        //ゲームオーバー時にUI表示
        gameOverUI.SetActive (true);
    }
}
