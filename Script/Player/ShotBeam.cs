using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBeam : MonoBehaviour
{
    [SerializeField]private ParticleSystem beam;
    [SerializeField]private AudioClip audioClip;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // スタートしていないならリターン
        if(!GameManager.ManagerInstance.IsStart)
        {
            beam.Stop();
            return;
        }

        // L2ボタンでビーム（パーティクル）を押している間だしちゃう
        if(JoyconInput.instance.joyconR.GetButton(Joycon.Button.SHOULDER_2))
        {
            if(!beam.isEmitting)
            {
                beam.Play();
                audioSource.PlayOneShot(audioClip);
            }
        }
        else
        {
            if(beam.isEmitting)
            {
                beam.Stop();
            }
        }
    }
}
