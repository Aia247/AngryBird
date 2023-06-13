using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public void Destroying()
    {
        Destroy(gameObject);
    }
}
