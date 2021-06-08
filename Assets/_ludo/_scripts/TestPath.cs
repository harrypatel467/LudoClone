using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPath : MonoBehaviour
{
    public Transform m_Dot;

    public SelectType m_type;
    [Space]
    public List<Transform> m_mywaypoints;

    [Space]
    public WayPoints m_waypoints;

    public enum SelectType
    {
        m_red,
        m_blue,
        m_yellow,
        m_green
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        switch (m_type)
        {
            case SelectType.m_blue:

                for (int i = 0; i < m_waypoints.m_bluepublicpoints.Count; i++)
                {
                    m_mywaypoints.Add(m_waypoints.m_bluepublicpoints[i]);
                }
                yield return null;

                for (int i = 0; i < m_waypoints.m_blueprivatepoints.Count; i++)
                {
                    m_mywaypoints.Add(m_waypoints.m_blueprivatepoints[i]);
                }

                break;

            case SelectType.m_red:

                for (int i = 0; i < m_waypoints.m_redpublicpoints.Count; i++)
                {
                    m_mywaypoints.Add(m_waypoints.m_redpublicpoints[i]);
                }
                yield return null;

                for (int i = 0; i < m_waypoints.m_redprivatepoints.Count; i++)
                {
                    m_mywaypoints.Add(m_waypoints.m_redprivatepoints[i]);
                }

                break;

            case SelectType.m_green:

                for (int i = 0; i < m_waypoints.m_greenpublicpoints.Count; i++)
                {
                    m_mywaypoints.Add(m_waypoints.m_greenpublicpoints[i]);
                }
                yield return null;

                for (int i = 0; i < m_waypoints.m_grenprivatepoints.Count; i++)
                {
                    m_mywaypoints.Add(m_waypoints.m_grenprivatepoints[i]);
                }

                break;

            case SelectType.m_yellow:

                for (int i = 0; i < m_waypoints.m_yellowpublicpoints.Count; i++)
                {
                    m_mywaypoints.Add(m_waypoints.m_yellowpublicpoints[i]);
                }
                yield return null;

                for (int i = 0; i < m_waypoints.m_yellowprivatepoints.Count; i++)
                {
                    m_mywaypoints.Add(m_waypoints.m_yellowprivatepoints[i]);
                }

                break;
        }

        yield return null;

        StartCoroutine(_StartTesting());
    }

    IEnumerator _StartTesting()
    {
        yield return null;

        for (int i = 0; i < m_mywaypoints.Count; i++)
        {
            m_Dot.transform.position = m_mywaypoints[i].transform.position;
            yield return new WaitForSecondsRealtime(0.5f);
        }

    }
}
