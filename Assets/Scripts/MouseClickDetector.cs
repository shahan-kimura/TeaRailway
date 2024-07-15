using UnityEngine;

public class MouseClickDetector : MonoBehaviour
{
    [SerializeField] LayerMask clickLayerMask; // �}�E�X�N���b�N�Ō��o���郌�C���[

    // �}�E�X�ʒu����Ray�𔭎˂��A�Փ˂����ʒu��Ԃ��֐�
    public Vector3 RaycastMousePoint()
    {
        // ���C���J��������}�E�X�ʒu�ւ�Ray���擾
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Ray���w�肵�����C���[�}�X�N���ŉ����ɏՓ˂����ꍇ
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickLayerMask))
        {
            Vector3 clickPosition = hit.point; // �Փ˓_�̈ʒu���擾
            Debug.Log("Clicked at: " + clickPosition); // �f�o�b�O���O�ɃN���b�N�����ʒu��\��
            return clickPosition; // �Փ˂����ʒu��Ԃ�
        }

        return Vector3.zero; // �Փ˂��Ȃ������ꍇ�̓[���x�N�g����Ԃ�
    }
}