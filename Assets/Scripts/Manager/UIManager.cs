using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI phaseText; // Phase情報を表示するUI Text
    [SerializeField] private TextMeshProUGUI currentEnemyText; // 残存Enemy数情報を表示するUI Text

    [SerializeField] private PhaseManager phaseManager; // PhaseManagerへの参照

    void Update()
    {
        // PhaseManagerから現在のPhaseを取得してUIに表示
        phaseText.text = "Current Phase: " + phaseManager.CurrentPhase.ToString();
        currentEnemyText.text = "CurrentEnemy: " + phaseManager.CurrentEnemy.ToString();
    }
}
