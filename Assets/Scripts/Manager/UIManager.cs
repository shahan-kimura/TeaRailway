using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI phaseText; // Phase����\������UI Text
    [SerializeField] private PhaseManager phaseManager; // PhaseManager�ւ̎Q��

    void Update()
    {
        // PhaseManager���猻�݂�Phase���擾����UI�ɕ\��
        phaseText.text = "Current Phase: " + phaseManager.CurrentPhase.ToString();
    }
}
