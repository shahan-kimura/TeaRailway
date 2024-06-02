using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ώۂ�ǔ�����J�����̋@�\��񋟂��܂��B
public class ChaseCamera : MonoBehaviour
{
    // �ǔ��Ώۂ��w�肵�܂��B
    [SerializeField]
    [Tooltip("�ǔ��Ώۂ��w�肵�܂��B")]
    private Transform target = null;
    // �ǔ��ΏۂƂ̃I�t�Z�b�g�l���w�肵�܂��B
    [SerializeField]
    [Tooltip("�ǔ��ΏۂƂ̃I�t�Z�b�g�l���w�肵�܂��B")]
    Vector2 offset = new(4, 1.5f);

    // Start is called before the first frame update
    void Start()
    {
        // �J�����̌��݈ʒu���X�V
        var position = transform.position;
        position.x = target.position.x + offset.x;
        // y���W���Œ肵�����ꍇ�́A�ȉ����R�����g�A�E�g
        position.y = target.position.y + offset.y;
        transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        // �J�����̌��݈ʒu���X�V
        var position = transform.position;
        position.x = target.position.x + offset.x;
        // y���W���Œ肵�����ꍇ�́A�ȉ����R�����g�A�E�g
        position.y = target.position.y + offset.y;
        transform.position = position;
    }
}
