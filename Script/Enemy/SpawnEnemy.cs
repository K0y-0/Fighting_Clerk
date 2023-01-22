using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]private List<GameObject> spawnCharaKind;
    private List<GameObject> spawnCharacters;
    //[SerializeField]private GameObject spawnPoint;
    //[SerializeField]private float firstSpawn;
    [SerializeField]private float spawnInterval;
    [SerializeField]private float subInterval;
    //[SerializeField]private int spawnCount = 20;

    [SerializeField]private float xMax;
    [SerializeField]private float xMin;
    [SerializeField]private float yMax;
    [SerializeField]private float yMin;
    [SerializeField]private float zMax;
    [SerializeField]private float zMin;

    [SerializeField]private List<Transform> charasTransform;

    private float count = 0;
    private float subCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("SpawnChara", firstSpawn, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        // ゲームスタートしていならリターン
        if(!GameManager.ManagerInstance.IsStart) return;

        // スポーン時間クールタイム
        count += Time.deltaTime;

        // スポーン実行
        if(count > spawnInterval)
        {
            SpawnMainChara();
            count = 0;
        }

        subCount += Time.deltaTime;
        if(subCount > subInterval)
        {
            SpawnSubChara();
            subCount = 0;
        }


        // if(spawnCount == 0)
        // {
        //     CancelInvoke("SpawnMainChara");
        // }

        // if(Input.GetKey(KeyCode.Space))
        // {
        //     foreach(var g in spawnCharacters)
        //     {
        //         charasTransform.Add(g.transform);
        //     }
        // }
    }

    // キャラスポーン処理
    void SpawnMainChara()
    {
        GameObject chara = Instantiate(spawnCharaKind[0], RandomPosition(), Quaternion.identity);
        chara.name = spawnCharaKind[0].name;
        Destroy(chara, 20f);
        //spawnCharacters.Add(chara);
        //spawnCount -= 1;
    }

    void SpawnSubChara()
    {
        var tmp = Random.Range(1,3);
        GameObject chara = Instantiate(spawnCharaKind[tmp], RandomPosition(), Quaternion.identity);
        chara.name = spawnCharaKind[tmp].name;
        Destroy(chara, 20f);
        //spawnCharacters.Add(chara);
        //spawnCount -= 1;
    }

    // ランダムなスポーンポイントを生成
    Vector3 RandomPosition()
    {
        float x = Random.Range(xMin, xMax);
        float y = Random.Range(yMin, yMax);
        float z = Random.Range(zMin, zMax);

        return new Vector3(x, y, z);
    }
}
