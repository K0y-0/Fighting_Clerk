using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    private int Hp;
    private string eName;
    private bool isAlive;
    private AudioClip audioClip;
    private ParticleSystem particle;

    public string Name
    {
        get 
        {
            return eName;
        }

        set
        {
            eName = value;
        }
    }

    public int HP
    {
        get
        {
            return Hp;
        }

        set
        {
            Hp = value;
        }
    }

    public bool Alive
    {
        get
        {
            return isAlive;
        }

        set
        {
            isAlive = value;
        }
    }

    public AudioClip AudioClip
    {
        get
        {
            return audioClip;
        }

        set
        {
            audioClip = value;
        }
    }

    public ParticleSystem Particle
    {
        get
        {
            return particle;
        }

        set
        {
            particle = value;
        }
    }
}
