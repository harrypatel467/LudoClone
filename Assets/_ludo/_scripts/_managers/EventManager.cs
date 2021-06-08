using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public  delegate void _EventWithoutParameater();
    public delegate void _EventWithParameater(int m_dice);

    public static event _EventWithoutParameater e_ongamestart ,e_diceanimcomplete,e_changeturn;
    public static event _EventWithParameater e_dicerolledinformplayers;

    public static void _NotifyOnGameStart()
    {
        if (e_ongamestart!=null)
        {
            e_ongamestart();
        }
    }

    public static void _NotifyOnDiceRolled( int m_dicenumber) 
    {
        if (e_dicerolledinformplayers!=null)
        {
            e_dicerolledinformplayers(m_dicenumber);
        }
    }


    public static void _NotyfyonDiceAnimComplete()
    {
        if (e_diceanimcomplete != null)
        {
            e_diceanimcomplete();
        }
    }

    public static void _NotifyToChangeTurn()
    {
        if (e_changeturn != null)
        {
            e_changeturn();
        }
    }


}
