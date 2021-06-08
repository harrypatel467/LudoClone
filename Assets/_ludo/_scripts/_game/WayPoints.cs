using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    [Header("BLUE WAY POINTS")]
    public List<Transform> m_bluepublicpoints;
    [Space]
    public List<Transform> m_blueprivatepoints;

    [Header("RED WAY POINTS")]
    public List<Transform> m_redpublicpoints;
    [Space]
    public List<Transform> m_redprivatepoints;

    [Header("GREEN WAY POINTS")]
    public List<Transform> m_greenpublicpoints;
    [Space]
    public List<Transform> m_grenprivatepoints;

    [Header("YELLOW WAY POINTS")]
    public List<Transform> m_yellowpublicpoints;
    [Space]
    public List<Transform> m_yellowprivatepoints;
}
