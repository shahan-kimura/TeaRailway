using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyRoutine : MonoBehaviour
{
    [SerializeField] GameObject lockOnCircle;   // ���b�N�I���T�[�N���̃I�u�W�F�N�g
    [SerializeField] string TagName;            //�����蔻��ƂȂ�^�O
    bool isLockOn = false;                      //���b�N�I������pbool

    //���[�U�[�ł��g�����ǔ��p�t�B�[���h�ϐ��Q
    Vector3 acceleration;
    Vector3 velocity;
    Vector3 position;
    Transform target; // �����Player��ǔ�����

    float lifeTime = 0;
    [SerializeField] float amplitude = 2f;      // �U��
    [SerializeField] float frequency = 1f;      // ���g��
    [SerializeField] float sinValue_x = 0f;      // Sin�֐��̒l
    [SerializeField] float sinValue_y = 0f;      // Sin�֐��̒l
    [SerializeField] float sinValue_z = 0f;      // Sin�֐��̒l
    [SerializeField] float offsetPosX = -10f;          // X�������̃I�t�Z�b�g
    [SerializeField] float offsetPosY = 5f;          // Y�������̃I�t�Z�b�g
    [SerializeField] float offsetPosZ = 0f;          // Z�������̃I�t�Z�b�g

    float cycleDelta;                            // �����̂���

    [SerializeField][Tooltip("��������")] float period = 1f;
    [SerializeField][Tooltip("��������")] float deltaPeriod = 0.5f;



    void Start()
    {
        // ���b�N�I���T�[�N�����q�I�u�W�F�N�g���猟�����Ĕ�\���ɂ���
        lockOnCircle = transform.Find("LockOnCircle").gameObject;
        lockOnCircle.SetActive(false);

        //�ǔ��p�F�����ʒu�Ƒ��x�̐ݒ�
        position = transform.position;
        velocity = new Vector3(Random.Range(-5.0f, 5.0f), 0, Random.Range(-6.0f, 6.0f));
        period += Random.Range(-deltaPeriod, deltaPeriod);

        // �ǔ��Ώۂ̎擾�i�����Player��z��j
        target = GameObject.Find("Player").GetComponent<Transform>();

        cycleDelta = Random.Range(-0.5f, 0.5f);
    }

    private void Update()
    {
        //�^���������Ft�b�Ԃɐi�ދ���(diff) = (�����x(v) * t) �{ (1/2 *�����x(a) * t^2)
        //�ό`�����
        //�^���������F�����x(a) = 2*(diff - (v * t)) / t^2 
        //�Ȃ̂ŁA�u���xv�̕��̂�t�b���diff�i�ނ��߂̉����xa�v���Z�o�ł���
        //GameObject��v�͎擾�ł��邵�At���擾�ł���
        //�Ȃ�AObject��period�b��ɓ����idiff��0�j���邽�߂ɕK�v��a���Z�o�ł����ˁI

        // ���Ԍo�߂ɂ��Sin�֐��̒l�̍X�V
        lifeTime += Time.deltaTime;
        sinValue_x = amplitude * Mathf.Sin((lifeTime * frequency) + cycleDelta); // lifeTime���g���đS�̂̎��Ԃ�Sin�֐����v�Z����
        sinValue_y = amplitude * Mathf.Sin((lifeTime * frequency) + cycleDelta); // lifeTime���g���đS�̂̎��Ԃ�Sin�֐����v�Z����
        sinValue_z = amplitude *2f * Mathf.Sin((lifeTime *0.5f * frequency) + cycleDelta); // lifeTime���g���đS�̂̎��Ԃ�Sin�֐����v�Z����

        acceleration = Vector3.zero; // ���������x��0

        // �ǔ��ΏۂƂ̋������v�Z���A���̋����ɑ΂�������x�����߂�
        Vector3 diff = target.position - position;
        acceleration.y = 0f;
        acceleration.z = 0f;
        acceleration += (diff - velocity * period) * 2f / (period * period);

        // �c��̓������Ԃ̍X�V
        period -= Time.deltaTime;

        // �������Ԃ����l�ȉ��ɂȂ�����Đݒ肷�遨�����ƒǏ]����
        if (period < 0.2f) 
        {
            period += Time.deltaTime;
        }

        velocity += acceleration * Time.deltaTime;      // ���ݑ��x�������x*����
        position += velocity * Time.deltaTime ;          // ���݈ʒu�����x*����

        //position��x�������X�V�Ay,z�͏�Lacceleration.y����0�w��
        //sinValue��player���s�����������AdeltaPos�œGprefab���ɖڕW�n�_�̃Y���𐶐�
        transform.position = new Vector3(position.x + sinValue_x + offsetPosX,
                                         position.y + sinValue_y + offsetPosY, 
                                         position.z + sinValue_z + offsetPosZ );
    }



    // ���b�N�I�����J�n����
    public void LockOnCheck()
    {
        if (isLockOn)
        {
            lockOnCircle.SetActive(true);    // ���b�N�I���T�[�N����\������
        }
        else
        {
            lockOnCircle.SetActive(false);    // ���b�N�I���T�[�N�����\������
        }
    }

    // ���b�N�I�����~����
    public void ActivateLockOn()
    {
        isLockOn = true;
        LockOnCheck();      //bool�̍X�V�ɍ��킹�ăZ�b�g��LockOnCheck��������
    }

    private void OnTriggerEnter(Collider other)
    {
        //�����蔻��̃^�O�l�[���ƈ�v���Ă���Ώ����̌Ăяo��
        if (other.CompareTag(TagName))
        {
            isLockOn = false;
            LockOnCheck();
        }
    }
}
