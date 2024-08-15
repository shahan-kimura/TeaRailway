using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI phaseText; // Phase情報を表示するUI Text
    [SerializeField] private PhaseManager phaseManager; // PhaseManagerへの参照

    void Update()
    {
        // PhaseManagerから現在のPhaseを取得してUIに表示
        phaseText.text = "Current Phase: " + phaseManager.CurrentPhase.ToString();
    }
}
