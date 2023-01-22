using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class JoyconInput: MonoBehaviour
{
    public static JoyconInput instance;

    private static readonly Joycon.Button[] m_buttons =
        Enum.GetValues( typeof( Joycon.Button ) ) as Joycon.Button[];

    public List<Joycon>    m_joycons;
    public Joycon          m_joyconL;
    public Joycon          m_joyconR;
    public Joycon.Button?  m_pressedButtonL;
    public Joycon.Button?  m_pressedButtonR;

    //インスタンス取得用変数
    public Vector3 joyconGyro;
    public List<Joycon> joyconInstance;
    public Joycon joyconL;
    public Joycon joyconR;

    void Awake()
    {
        m_joycons = JoyconManager.Instance.j;

        //接続されていないならリターン
        if ( m_joycons == null || m_joycons.Count <= 0 ) return;

        //joyconの左右判定
        m_joyconL = m_joycons.Find( c =>  c.isLeft );
        m_joyconR = m_joycons.Find( c => !c.isLeft );

        m_pressedButtonL = null;
        m_pressedButtonR = null;

        //各種ボタン情報取り出し
        foreach ( var button in m_buttons )
        {
            if ( m_joyconL.GetButton( button ) )
            {
                m_pressedButtonL = button;
            }
            if ( m_joyconR.GetButton( button ) )
            {
                m_pressedButtonR = button;
            }
        }

        joyconInstance = JoyconManager.Instance.j;
        joyconL = m_joycons.Find( c =>  c.isLeft ); // Joy-Con (L)
        joyconR = m_joycons.Find( c => !c.isLeft ); // Joy-Con (R)

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            if(instance == null)
            {
                Debug.Log("JoyconInput Instance Error");
            }
        }
    }

    // public JoyconInput()
    // {
    //     m_joycons = JoyconManager.Instance.j;

    //     //接続されていないならリターン
    //     if ( m_joycons == null || m_joycons.Count <= 0 ) return;

    //     //joyconの左右判定
    //     m_joyconL = m_joycons.Find( c =>  c.isLeft );
    //     m_joyconR = m_joycons.Find( c => !c.isLeft );

    //     m_pressedButtonL = null;
    //     m_pressedButtonR = null;

    //     //各種ボタン情報取り出し
    //     foreach ( var button in m_buttons )
    //     {
    //         if ( m_joyconL.GetButton( button ) )
    //         {
    //             m_pressedButtonL = button;
    //         }
    //         if ( m_joyconR.GetButton( button ) )
    //         {
    //             m_pressedButtonR = button;
    //         }
    //     }

    //     joyconInstance = JoyconManager.Instance.j;
    //     joyconL = m_joycons.Find( c =>  c.isLeft ); // Joy-Con (L)
    //     joyconR = m_joycons.Find( c => !c.isLeft ); // Joy-Con (R)
    // }
}

// 参考 https://baba-s.hatenablog.com/entry/2017/11/12/090000