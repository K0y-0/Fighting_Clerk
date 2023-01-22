using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamCollision : MonoBehaviour
{
    void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.GetComponent<Animal>())
        {
            Animal a = other.gameObject.GetComponent<Animal>();
            a.getHP -= 1;
            Debug.Log("Hit");

            if(other.CompareTag("Wrong")) { GameManager.ManagerInstance.SetWrongText(a.getName); }
        }
    }

}
