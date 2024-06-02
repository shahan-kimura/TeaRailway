using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���[�U�[���͂��󂯎���ăv���C���[�𑀍삵�܂��B
public class Player : MonoBehaviour
{
    // �ړ����x���w�肵�܂��B
    [SerializeField]
    [Tooltip("�ړ����x���w�肵�܂��B")]
    private float speed = 2;
    // �W�����v�͂��w�肵�܂��B
    [SerializeField]
    [Tooltip("�W�����v�͂��w�肵�܂��B")]
    private Vector2 jumpPower = new(0, 6);
    // �n�ʂƂ̌�������p�̃`�F�b�J�[���w�肵�܂��B
    //[SerializeField]
    //[Tooltip("�n�ʂƂ̌�������p�̃`�F�b�J�[���w�肵�܂��B")]
    //private LineCaster2D groundChecker = null;

    // �R���|�[�l���g���Q�Ƃ��Ă����ϐ�
    new Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        // �R���|�[�l���g�����O�ɎQ��
        rigidbody = GetComponent<Rigidbody>();
    }

    // ���t���[���Ɉ�x���s�����X�V�����ł��B
    void Update()
    {
        // �ڒn��Ԃ̏ꍇ
        //if (groundChecker.IsCasted)
        //{
            // �W�����v
            if (Input.GetButtonDown("Jump"))
            {
                rigidbody.AddForce(jumpPower, ForceMode.Impulse);
            }

            // �����x�^��
            var velocity = rigidbody.velocity;
            velocity.x = speed;
            rigidbody.velocity = velocity;
        //}
    }
}