using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFire : MonoBehaviour
{
    Vector3 acceleration;
    Vector3 velocity;
    Vector3 position;
    Transform target; // ���[�U�[�Ώ�

    [SerializeField][Tooltip("���e����")] float period = 1f;                  
    [SerializeField][Tooltip("���e����")] float deltaPeriod = 0.5f;
    Vector3 randomPos; // �^�[�Q�b�g�����݂��Ȃ����̋󌂂��pposition

    [SerializeField][Tooltip("���e�p�[�e�B�N��")] GameObject explosionPrefab;
    GameObject explosionInstance; // �����G�t�F�N�g�̃C���X�^���X

    bool exploded = false; // �����G�t�F�N�g���Đ����ꂽ���ǂ����̃t���O

    // ���[�U�[�̃^�[�Q�b�g��ݒ肷��֐�
    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    void Start()
    {
        position = transform.position;
        randomPos = new Vector3(Camera.main.transform.position.x + 20f,
                                Random.Range(0f, 10f), Random.Range(0f, 5f));
        velocity = new Vector3(Random.Range(-5.0f, -2.5f), Random.Range(-6.0f, 6.0f), Random.Range(-6.0f, 6.0f));

        period += Random.Range(-deltaPeriod, deltaPeriod);
    }

    void Update()
    {
        acceleration = Vector3.zero; // ���������x��0

        // �^�[�Q�b�g�����݂��Ȃ��ꍇ�ɂ�randomPos�𗘗p
        Vector3 diff;
        if (target != null)
        {
            diff = target.position - position;
        }
        else
        {
            diff = randomPos - position;
        }

        //�^���������Ft�b�Ԃɐi�ދ���(diff) = (�����x(v) * t) �{ (1/2 *�����x(a) * t^2)
        //�ό`�����
        //�^���������F�����x(a) = 2*(diff - (v * t)) / t^2 
        //�Ȃ̂ŁA�u���xv�̕��̂�t�b���diff�i�ނ��߂̉����xa�v���Z�o�ł���
        //GameObject��v�͎擾�ł��邵�At���擾�ł���
        //�Ȃ�A���[�U�[��period�b��ɒ��e�idiff��0�j���邽�߂ɕK�v��a���Z�o�ł����ˁI

        acceleration += (diff - velocity * period) * 2f / (period * period);

        period -= Time.deltaTime;
        if (period < 0f && !exploded)
        {
            TriggerExplosion(); // ���e���ɔ����G�t�F�N�g���Đ�
            Destroy(gameObject);
            return;
        }

        velocity += acceleration * Time.deltaTime; // ���ݑ��x�������x*����
        position += velocity * Time.deltaTime; // ���݈ʒu�����x*����
        transform.position = position;
    }

    // �����G�t�F�N�g���Đ�����֐�
    void TriggerExplosion()
    {
        // �����G�t�F�N�g���C���X�^���X�����āA�ʒu��ݒ�
        explosionInstance = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        exploded = true; // �������Đ����ꂽ���Ƃ��t���O�ŊǗ�

        // �����G�t�F�N�g���Đ�������ɁA�����I�ɂ���GameObject��j��
        Destroy(explosionInstance, explosionPrefab.GetComponent<ParticleSystem>().main.duration);
    }
}
