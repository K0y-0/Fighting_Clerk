using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //エフェクトが生成されて2秒後にオブジェクトを削除する
        Invoke("Break",2.0f);
    }
 
    //エフェクト(自分自身)を削除する
    void Break()
    {
        Destroy(gameObject);
    }
}
