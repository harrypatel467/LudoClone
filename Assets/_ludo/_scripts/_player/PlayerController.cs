using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public List<TokenController> m_mytokens;
    [Space]
    public List<Transform> m_startpos;
    [Space]
    public List<Transform> m_mypublicwaypoints;
    public List<Transform> m_myprivatewaypoints;
    [Space]
    public List<Transform> m_totalwaypoints;
    [Space]
    public _MyColor m_mycolor;
    [Space]
    public bool m_movemeeentenabl=false;

    public bool m_housebreak;

    private Vector3 m_clickpos;
    private RaycastHit2D m_hit;
    private Camera m_maincamera;

    private TokenController m_currunttokentomove;

    private bool m_turnistakken;


    private int m_totalmovesteps;

    private void Start()
    {
        m_maincamera = Camera.main;
    }

    private void Update()
    {
        if (m_movemeeentenabl)
        {

            if (m_turnistakken)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                m_clickpos = m_maincamera.ScreenToWorldPoint(Input.mousePosition);
                m_clickpos.z = -10f;
                Debug.Log(m_clickpos);

                m_hit = Physics2D.Raycast(m_clickpos, Vector3.forward);

                Debug.DrawRay(m_clickpos, Vector3.forward, Color.cyan, 2f);

                if (m_hit.collider != null)
                {
                    Debug.Log(m_hit.collider.gameObject.name);

                    if (m_hit.collider.gameObject.GetComponent<TokenController>())
                    {
                        if (m_hit.collider.gameObject.GetComponent<TokenController>().m_type == m_mycolor)
                        {
                            m_turnistakken = true;
                            m_movemeeentenabl = false;
                            m_currunttokentomove = m_hit.collider.gameObject.GetComponent<TokenController>();
                            _ClickedOnDice();
                        }
                        return;
                    }

                }
            }


        }
    }


    public void _ItsYourTurnNow()
    {
        Debug.Log(DiceManager.m_diceno);

        if (_CheckIfAllDiceAreInHouse())
        {
            if (DiceManager.m_diceno==6)
            {
                Debug.Log("House will be Broke now");
                m_movemeeentenabl = true;
            }
            else
            {
                Debug.Log("No 6 number move to next person");
                TurnManager.Instance.m_curruntplayerturned++;
                EventManager._NotifyToChangeTurn();
            }
        }
        else
        {
            Debug.Log("House Broke");
            m_movemeeentenabl = true;
        }
    }

    void _DoHouseBreakProcess()
    {
        Debug.Log("Yay 6 on the dice house is breked");
    }


    /// <summary>
    /// On Click Token
    /// </summary>
    void _ClickedOnDice()
    {

        if (m_housebreak==false)
        {
            m_housebreak = true;
            StartCoroutine(_MovePoint());
            return;
        }

        m_currunttokentomove.m_mysteps += DiceManager.m_diceno;
        StartCoroutine(_MovePoint());

        Debug.Log("Ckicked Me Moveme Now");
    }

    IEnumerator _MovePoint()
    {

        m_totalmovesteps = m_currunttokentomove.m_mysteps;

        Debug.Log("HOW MANY STEPS TO MOVE : "+m_totalmovesteps);

        if (m_totalmovesteps==0)
        {
            m_currunttokentomove.m_iaminhouse = false;
            yield return new WaitForSecondsRealtime(0.1f);
            m_currunttokentomove.transform.position = m_mypublicwaypoints[m_totalmovesteps].position;
        }

        for (int i = 0; i < m_totalmovesteps; i++)
        {
            m_currunttokentomove.m_iaminhouse = false;
            m_currunttokentomove.transform.position = m_mypublicwaypoints[m_totalmovesteps].position;
            yield return new WaitForSecondsRealtime(0.5f);
        }


        //Check if it;s collid with other players token

        yield return new WaitForSecondsRealtime(0.1f);

        //CHECK IF IT'S SECOND TURN
        if (DiceManager.m_diceno==6)
        {
            Debug.Log("6 number so again");
            EventManager._NotifyToChangeTurn();
        }
        else
        {
            Debug.Log("Next player move");
            EventManager._NotifyToChangeTurn();
        }
    }

    bool _CheckIfAllDiceAreInHouse()
    {
        for (int i = 0; i < m_mytokens.Count; i++)
        {
            if (m_mytokens[i].m_iaminhouse==true)
            {
                m_housebreak = false;
                return true;
                break;
            }
            else
            {
                Debug.Log("I am Not In House "+m_mytokens[i].gameObject.name);
                return true;
                break;
            }
        }

        m_housebreak = true;
        return false;
    }


    /// <summary>
    /// This method call when player gets genrated 
    /// </summary>
    public void _GenrateMyTokens()
    {
        Debug.Log("Game Has Been Started");
        //Call Every Player and Initilized Start Setup
        for (int i = 0; i < 4; i++)
        {
            m_mytokens.Add(TurnManager.Instance._GetToken(m_mycolor));
            m_mytokens[i].transform.SetParent(transform);
            m_mytokens[i].m_iaminhouse = true;
        }

        StartCoroutine(_AnimateTokensOnStart());
    }


    IEnumerator _AnimateTokensOnStart()
    {
        yield return null;

        for (int i = 0; i < m_mytokens.Count; i++)
        {
            m_mytokens[i].transform.position = m_startpos[i].position;
            yield return null;
        }
    }
}

[Serializable]
public enum _MyColor
{
    m_red,
    m_blue,
    m_yellow,
    m_green
}


