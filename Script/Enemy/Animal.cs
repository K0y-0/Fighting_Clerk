using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    [SerializeField]private int hp;
    [SerializeField]private string enemyName;
    [SerializeField]private AudioClip deathSound;
    [SerializeField]private AudioClip exploSE;
    [SerializeField]private GameObject deathParticle;
    [SerializeField]private int sendScore;
    private AudioSource audioSource;
    private Enemy enemy = new Enemy();
    //private bool alive;


    // Start is called before the first frame update
    void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    void Start()
    {
        Init();
        enemy.Alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!enemy.Alive) return;

        if(enemy.HP < 1)
        {
            GenerateEffect();
            AudioSource.PlayClipAtPoint(deathSound, this.transform.position);
            AudioSource.PlayClipAtPoint(exploSE, this.transform.position);
            GameManager.ManagerInstance.Score += SendScore();
            enemy.Alive = false;

            Destroy(this.gameObject);
        }
    }

    void Init()
    {
        enemy.HP = hp;
        enemy.Name = enemyName;
        enemy.Alive = true;
        //enemy.AudioClip = deathSound;
        //enemy.Particle = deathParticle;
    }

    void GenerateEffect()
    {
        //エフェクトを生成する
        GameObject effect = Instantiate(deathParticle) as GameObject;
        //エフェクトが発生する場所を決定する(敵オブジェクトの場所)
        effect.transform.position = gameObject.transform.position;
    }

    public int getHP
    {
        get
        {
            return enemy.HP;
        }

        set
        {
            enemy.HP = value;
        }
    }

    public string getName
    {
        get
        {
            return enemy.Name;
        }
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     if(other.CompareTag("Beam"))
    //     {
    //         enemy.HP -= 1;
    //     }
    // }

    // void OnParticleTrigger(ParticleSystem other)
    // {
    //     if(other.gameObject.CompareTag("Beam"))
    //     {
    //         Debug.Log("Damage");
    //         hp -= 1;
    //     }
    // }

    // void OnTriggerEnterStay(Collider other)
    // {
    //     if(other.gameObject.CompareTag("Beam"))
    //     {
    //         Debug.Log("Damage");
    //         hp -= 1;
    //     }
    // }

    public int SendScore()
    {
        return sendScore;
    }
}
