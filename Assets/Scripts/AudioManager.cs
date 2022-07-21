using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("The AudioManager is Null");

            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
}
