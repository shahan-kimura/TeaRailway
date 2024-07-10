using UnityEngine;
using UnityEngine.UI;

public class ClearSceneManager : MonoBehaviour
{
    [SerializeField] private Text clearSpeedText;

    void Start()
    {
        if (clearSpeedText != null)
        {
            float clearSpeed = GameManager.Instance.VelocityScore;
            clearSpeedText.text = "Clear Speed: " + clearSpeed.ToString("F2") + " units/sec";
        }
        else
        {
            Debug.LogError("ClearSpeedText is not assigned.");
        }
    }
}