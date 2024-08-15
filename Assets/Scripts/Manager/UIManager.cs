using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI phaseText; // Phaseî•ñ‚ğ•\¦‚·‚éUI Text
    [SerializeField] private PhaseManager phaseManager; // PhaseManager‚Ö‚ÌQÆ

    void Update()
    {
        // PhaseManager‚©‚çŒ»İ‚ÌPhase‚ğæ“¾‚µ‚ÄUI‚É•\¦
        phaseText.text = "Current Phase: " + phaseManager.CurrentPhase.ToString();
    }
}
