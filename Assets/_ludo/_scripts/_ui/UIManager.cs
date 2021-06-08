using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject m_startmenu;


    public Button m_start;

    [Header("PLAYER SELECTION BUTTONS")]
    public Button m_2p;
    public Button m_3p;
    public Button m_4p;


    private void Start()
    {
        m_start.onClick.AddListener(_StartGame);
        m_2p.onClick.AddListener(() => { _SelectCountOfPlayer(2);});
        m_3p.onClick.AddListener(() => { _SelectCountOfPlayer(3); });
        m_4p.onClick.AddListener(() => { _SelectCountOfPlayer(4); });


    }

    private void _StartGame()
    {
        m_startmenu.SetActive(false);
        EventManager._NotifyOnGameStart();
    }

    void _SelectCountOfPlayer(int m_numberofplayer)
    {

    }

}
