using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    [SerializeField]private GameObject RBone;
    [SerializeField]private float xSensi;
    [SerializeField]private float ySensi;
    [SerializeField]private float zSensi;
    

    private Quaternion riq;
    // Start is called before the first frame update
    void Start()
    {
        riq = RBone.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.ManagerInstance.IsStart) return;

        InitArm();
        RightArmRotate();
    }

    void RightArmRotate()
    {
        // const float SENSI = 0.01f;
        // const float ZSENSI = 0.0005f;
        JoyconInput.instance.joyconGyro = JoyconInput.instance.m_joycons[1].GetGyro();
        Quaternion rot = RBone.transform.rotation * Quaternion.Inverse(riq);
        rot.x += -JoyconInput.instance.joyconGyro[1] * xSensi;
        rot.y += JoyconInput.instance.joyconGyro[2] * ySensi;
        //rot.z += -JoyconInput.instance.joyconGyro[1] * zSensi;
        RBone.transform.rotation = rot * riq;
    }

    void InitArm()
    {
        if (JoyconInput.instance.m_joyconL.GetButtonDown(Joycon.Button.MINUS))
        {
            RBone.transform.rotation = riq;
        }
    }
}
