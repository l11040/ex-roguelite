using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyManager : MonoBehaviour
{
    public GameObject temp;
    public static MyManager Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void InitGame()
    {

    }

    public void PlayerDead()
    {

    }
}
