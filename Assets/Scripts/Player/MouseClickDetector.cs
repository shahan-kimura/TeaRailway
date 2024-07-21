using UnityEngine;

public class MouseClickDetector : MonoBehaviour
{
    [SerializeField] LayerMask clickLayerMask; // マウスクリックで検出するレイヤー

    // マウス位置からRayを発射し、衝突した位置を返す関数
    public Vector3 RaycastMousePoint()
    {
        // メインカメラからマウス位置へのRayを取得
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Rayが指定したレイヤーマスク内で何かに衝突した場合
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickLayerMask))
        {
            Vector3 clickPosition = hit.point; // 衝突点の位置を取得
            Debug.Log("Clicked at: " + clickPosition); // デバッグログにクリックした位置を表示
            return clickPosition; // 衝突した位置を返す
        }

        return Vector3.zero; // 衝突しなかった場合はゼロベクトルを返す
    }
}