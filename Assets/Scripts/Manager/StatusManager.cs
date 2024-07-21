using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    [SerializeField] GameObject MainObject; //���̃X�N���v�g���A�^�b�`����I�u�W�F�N�g
    [SerializeField] int HP =1;             //HP���ݒl
    [SerializeField] int MaxHP =1;          //������MaxHP���p����ۂɎg�p

    [SerializeField] GameObject destroyEffect;  //���j�G�t�F�N�g

    [SerializeField] string TagName;            //�����蔻��ƂȂ�^�O

    // Update is called once per frame
    void Update()
    {
        //HP��0�ȉ��Ȃ�A���j�G�t�F�N�g�𐶐�����Main��j��
        if (HP <= 0)
        {
            HP = 0;
            var effect = Instantiate(destroyEffect);
            effect.transform.position = transform.position;
            Destroy(effect,5);
            Destroy(MainObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //onTriggerEnter��e�ۂƑ��ݗ��p����ۂɂ�
        //���Ȃ��Ƃ��ǂ��炩�Е���Rigidbody��K�p�����邱�Ɓi�����ƃo�O��܂��j

        //�����蔻��̃^�O�l�[���ƈ�v���Ă���΃_���[�W�����Ăяo��
        if (other.CompareTag(TagName))
        {
            Debug.Log("Hit");
            Damage();

        }
    }
    private void Damage()
    {
        HP--;
    }

}
