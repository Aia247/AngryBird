using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class returnMap : MonoBehaviour
{
    public GameObject Panel;
    public GameObject Map;

    public void Return()
    {
        Panel.SetActive(false);
        Map.SetActive(true);
    }
}
