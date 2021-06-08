using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    [Header("START POSITIONS OF ALL PLAYERS")]
    public List<_HouseDescription> m_playersdata;
    [Space]
    [Space]
    [Header("PLAYER TOKENS PREFAB")]
    public GameObject m_token;
    [Header("Player Prefab")]
    public GameObject m_playerpreab;
    [Space] 
    public PlayerController m_curruntplayercontroller;  
    [Space]
    public int m_curruntplayerturned=0;

    [Space]
    public bool m_dicemovementenable=false;

    [Space]
    public List<_MyColor> m_selectedcolors;
    [Space]
    public List<PlayerController> m_allplayers;

    [Header("DICE NUMBER")]
    public int m_curruntrolleddicenumber;
    [Space]

    [Header("SCRIPT REFRENCE")]
    public WayPoints m_waypoints;

    public static TurnManager Instance;


    private _HouseDescription m_curruntplayerdata;

    private Vector3 m_clickpos;
    private RaycastHit2D m_hit;
    private Camera m_maincamera;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }

        m_maincamera = Camera.main;

    }


    private void OnEnable()
    {
        EventManager.e_ongamestart += _OnGameStarts;
        EventManager.e_diceanimcomplete += _NowPlayerCanMovePices;
        EventManager.e_changeturn += _TurnEndedChangePlayer;
    }

    private void OnDisable()
    {
        EventManager.e_ongamestart -= _OnGameStarts;
        EventManager.e_diceanimcomplete -= _NowPlayerCanMovePices;
        EventManager.e_changeturn -= _TurnEndedChangePlayer;
    }


    private void Update()
    {
        if (m_dicemovementenable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_clickpos = m_maincamera.ScreenToWorldPoint(Input.mousePosition);
                m_clickpos.z = -10f;
                Debug.Log(m_clickpos);


                m_hit = Physics2D.Raycast(m_clickpos, Vector3.forward);

                Debug.DrawRay(m_clickpos, Vector3.forward, Color.cyan, 2f);

                if (m_hit.collider != null)
                {

                    GameObject obj = m_hit.collider.gameObject;

                    if (obj.GetComponent<DiceId>().m_myid==m_curruntplayercontroller.m_mycolor)
                    {
                        Debug.Log(obj.transform.parent.gameObject.name);

                        m_dicemovementenable = false;
                        Debug.Log(m_hit.collider.gameObject.name);
                        _OnDiceClicled();
                        return;
                    }

                }

            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(m_clickpos, Vector3.down);
    }


    /// <summary>
    /// This Method will call after dice is rolled
    /// </summary>
    void _OnDiceClicled()
    {
        m_curruntrolleddicenumber = DiceManager._GetRandomDiceNumber();
        DiceManager.Instance._RollDiceAndAnimate(m_curruntplayercontroller.m_mycolor);
    }

    /// <summary>
    /// Enable Player to moe his pices here
    /// </summary>
    private void _NowPlayerCanMovePices()
    {
        Debug.Log("Now Player Can move ahed");
        m_curruntplayercontroller._ItsYourTurnNow();
    }


    /// <summary>
    /// BY EVENT
    /// </summary>
    private void _DiceAnimationComplete()
    {
       
    }


    /// <summary>
    /// BY EVENT
    /// </summary>
    private void _OnGameStarts()
    {
        _SetupHowManyPlayersInGame(m_selectedcolors);

        for (int i = 0; i < m_selectedcolors.Count; i++)
        {
            _GenratePlayer(m_selectedcolors[i]);
        }

        _TurnEndedChangePlayer();
    }


    void _SetupHowManyPlayersInGame(List<_MyColor> m_type)
    {

    }


    #region PLAYER AND TOKEN GENRATION
    /// <summary>
    /// Genrating Player passing int 
    /// </summary>
    /// <param name="m_i"></param>
    void _GenratePlayer(_MyColor m_myid)
    {
        GameObject m_obj = Instantiate(m_playerpreab,transform);

        PlayerController m_pc = m_obj.GetComponent<PlayerController>();

        m_allplayers.Add(m_pc);

        switch (m_myid)
        {
            case _MyColor.m_red:

                for (int i = 0; i < m_playersdata.Count; i++)
                {
                    if (m_playersdata[i].m_mycolor==_MyColor.m_red)
                    {
                        m_playersdata[i].m_playercontroller = m_obj.GetComponent<PlayerController>();
                        m_curruntplayerdata = m_playersdata[i];
                        break;
                    }
                }

                m_pc.m_mycolor = _MyColor.m_red;
                m_pc.m_startpos =m_curruntplayerdata.m_startpositions;
                m_pc._GenrateMyTokens();
                m_pc.m_mypublicwaypoints = m_waypoints.m_redpublicpoints;
                m_pc.m_myprivatewaypoints = m_waypoints.m_redprivatepoints;
                break;


            case _MyColor.m_green:

                for (int i = 0; i < m_playersdata.Count; i++)
                {
                    if (m_playersdata[i].m_mycolor == _MyColor.m_green)
                    {
                        m_playersdata[i].m_playercontroller = m_obj.GetComponent<PlayerController>();
                        m_curruntplayerdata = m_playersdata[i];
                        break;
                    }
                }

                m_pc.m_mycolor = _MyColor.m_green;
                m_pc.m_startpos = m_curruntplayerdata.m_startpositions;
                m_pc._GenrateMyTokens();
                m_pc.m_mypublicwaypoints = m_waypoints.m_greenpublicpoints;
                m_pc.m_myprivatewaypoints = m_waypoints.m_grenprivatepoints;
                break;


            case _MyColor.m_yellow:

                for (int i = 0; i < m_playersdata.Count; i++)
                {
                    if (m_playersdata[i].m_mycolor == _MyColor.m_yellow)
                    {
                        m_playersdata[i].m_playercontroller = m_obj.GetComponent<PlayerController>();
                        m_curruntplayerdata = m_playersdata[i];
                        break;
                    }
                }

                m_pc.m_mycolor = _MyColor.m_yellow;
                m_pc.m_startpos = m_curruntplayerdata.m_startpositions;
                m_pc._GenrateMyTokens();
                m_pc.m_mypublicwaypoints = m_waypoints.m_yellowpublicpoints;
                m_pc.m_myprivatewaypoints = m_waypoints.m_yellowprivatepoints;
                break;


            case _MyColor.m_blue:

                for (int i = 0; i < m_playersdata.Count; i++)
                {
                    if (m_playersdata[i].m_mycolor == _MyColor.m_blue)
                    {
                        m_playersdata[i].m_playercontroller = m_obj.GetComponent<PlayerController>();
                        m_curruntplayerdata = m_playersdata[i];
                        break;
                    }
                }

                m_pc.m_mycolor = _MyColor.m_blue;
                m_pc.m_startpos = m_curruntplayerdata.m_startpositions;
                m_pc._GenrateMyTokens();
                m_pc.m_mypublicwaypoints = m_waypoints.m_bluepublicpoints;
                m_pc.m_myprivatewaypoints = m_waypoints.m_blueprivatepoints;
                break;

        }
    }

    #endregion



    /// <summary>
    /// DECIDES WHICH PLAYER HAS TURN
    /// </summary>
    public void _TurnEndedChangePlayer()
    {

        //Changing Old Player dice to one
        DiceManager.Instance._ChangeOldDiceToOne();

        Debug.Log(m_allplayers.Count);

        //CHANGE CODE IF PLAYERS ARE 3 or 3 
        if (m_curruntplayerturned>m_allplayers.Count)
        {
            m_curruntplayerturned = 0;
        }

        Debug.Log("CURRUNT TURN NO : " + m_curruntplayerturned);

        m_curruntplayercontroller = m_allplayers[m_curruntplayerturned];

        int a = m_playersdata.FindIndex(asd => asd.m_mycolor == m_curruntplayercontroller.m_mycolor);

        Debug.Log(a);

        DiceManager.Instance.m_dice =m_playersdata[a].m_dicesprite.gameObject;

        m_dicemovementenable = true;
    }



    /// <summary>
    /// Player Will Acess This On Enable
    /// </summary>
    /// <param name="m_type"></param>
    /// <returns></returns>
    public TokenController _GetToken(_MyColor m_type)
    {

        GameObject m_obj = null;
        m_obj = Instantiate(m_token);
        m_obj.transform.position = new Vector2(100f, 100f);
        TokenController m_tc = m_obj.GetComponent<TokenController>();

        _HouseDescription m_hd = new _HouseDescription();

        switch (m_type)
        {
            case _MyColor.m_red:

                for (int i = 0; i < m_playersdata.Count; i++)
                {
                    if (m_playersdata[i].m_mycolor==_MyColor.m_red)
                    {
                        m_hd = m_playersdata[i];
                        break;
                    }
                }

                m_tc.GetComponent<SpriteRenderer>().sprite = m_hd.m_mytokensprite;
                m_tc.m_type = m_type;
                break;

            case _MyColor.m_green:

                for (int i = 0; i < m_playersdata.Count; i++)
                {
                    if (m_playersdata[i].m_mycolor == _MyColor.m_green)
                    {
                        m_hd = m_playersdata[i];
                        break;
                    }
                }
                m_tc.GetComponent<SpriteRenderer>().sprite = m_hd.m_mytokensprite;
                m_tc.m_type = m_type;
                break;

            case _MyColor.m_yellow:

                for (int i = 0; i < m_playersdata.Count; i++)
                {
                    if (m_playersdata[i].m_mycolor == _MyColor.m_yellow)
                    {
                        m_hd = m_playersdata[i];
                        break;
                    }
                }
                m_tc.GetComponent<SpriteRenderer>().sprite = m_hd.m_mytokensprite;
                m_tc.m_type = m_type;

                break;

            case _MyColor.m_blue:
                for (int i = 0; i < m_playersdata.Count; i++)
                {
                    if (m_playersdata[i].m_mycolor == _MyColor.m_blue)
                    {
                        m_hd = m_playersdata[i];
                        break;
                    }
                }
                m_tc.GetComponent<SpriteRenderer>().sprite = m_hd.m_mytokensprite;
                m_tc.m_type = m_type;
                break;
        }
        return m_obj.GetComponent<TokenController>(); ;
    }



    public static int _GetRandomNumber()
    {
        return Random.Range(1, 7);
    }
}

[System.Serializable]
public class _HouseDescription
{
    public List<Transform> m_startpositions;
    [Space]
    public GameObject m_dicecolider;
    public SpriteRenderer m_dicesprite;
    [Space]
    public PlayerController m_playercontroller;
    [Space]
    public Sprite m_mytokensprite;
    [Space]
    public GameObject m_myhouse;
    [Space]
    public _MyColor m_mycolor;

}
