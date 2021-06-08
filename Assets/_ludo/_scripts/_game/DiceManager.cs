using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public GameObject m_dice;

    [Header("DICE SPRITE DATA")]
    [Space]
    public DiceData m_dicescriptableobject;

    private SpriteRenderer m_sp;
    private Animator m_anim;

    private _MyColor m_myid;

    public static int m_diceno;

    private int m_spritelistno;

    private Sprite m_default;

    public static DiceManager Instance;
    private void Start()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    public static int _GetRandomDiceNumber()
    {
        m_diceno = Random.Range(1, 7);
        Debug.Log( "DICE NO IS : "+m_diceno);
        return m_diceno;
    }

    public void _RollDiceAndAnimate(_MyColor m_whichone)
    {
        m_myid = m_whichone;
        StartCoroutine(_AnimateDice());
    }

    IEnumerator _AnimateDice()
    {

        m_sp = m_dice.GetComponent<SpriteRenderer>();
        m_anim = m_dice.GetComponent<Animator>();
        m_default = m_sp.sprite;
        yield return null;
        m_anim.enabled = true;
        yield return new WaitForSecondsRealtime(1f);
        m_anim.enabled = false;
        int a= m_dicescriptableobject.m_dicedata.FindIndex(asd=>asd.m_id== m_myid);
        Debug.Log("m_diec no "+m_diceno);
        m_sp.sprite = m_dicescriptableobject.m_dicedata[a].m_dicesprites[m_diceno-1];
        EventManager._NotyfyonDiceAnimComplete();
    }

    public void _ChangeOldDiceToOne()
    {
        if (m_dice==null)
        {
            return;
        }

        m_dice.GetComponent<SpriteRenderer>().sprite = m_default;
    }

}
