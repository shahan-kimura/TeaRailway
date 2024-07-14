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

    private PlayerAttack playerAttack;          //�U���pScript��playerAttack��錾

    // Start is called before the first frame update
    void Start()
    {
        // �R���|�[�l���g�����O�ɎQ��
        rigidbody = GetComponent<Rigidbody>();

        // PlayerAttack �X�N���v�g�����R���|�[�l���g���擾
        playerAttack = GetComponent<PlayerAttack>();



    }

    // ���t���[���Ɉ�x���s�����X�V�����ł��B
    void Update()
    {

        // �W�����v
        if (Input.GetButtonDown("Jump"))
        {
            rigidbody.AddForce(jumpPower, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            playerAttack.FindClosestEnemyAndFire();
        }

        if (Input.GetKeyDown(KeyCode.V))  // V�������ꂽ��
        {
            playerAttack.FindAllEnemiesAndFire();
        }

        //�N���b�N���T�[�`���[�U�[
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("on click");
            StartCoroutine(playerAttack.StartLockOn());
        }
        //�N���b�N���T�[�`�I��
        if (Input.GetKeyUp(KeyCode.F))
        {
            playerAttack.StopLockOn();
        }


            // �����x�^��
            var velocity = rigidbody.velocity;
        velocity.x = speed;
        rigidbody.velocity = velocity;
        //}
    }

    //�S�[�������x�擾�̂��߁A�v���p�e�B���ŕۑ�
    public float CurrentSpeed
    {
        get { return rigidbody.velocity.magnitude; }
    }
}

