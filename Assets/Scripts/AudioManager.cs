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
    private void Start()
    {
        GameStartWarning();
        Invoke("GameStartWarning", 1.5f);
    }

    [SerializeField]
    AudioSource _audioSource;
    [SerializeField]
    AudioClip _shoot;
    [SerializeField]
    AudioClip _aiDeath;
    [SerializeField]
    AudioClip _barrierShot;
    [SerializeField]
    AudioClip _enemyReachedEnd;
    [SerializeField]
    AudioClip _warning;

    public void ShootingAudio()
    {
        AudioSource.PlayClipAtPoint(_shoot, _audioSource.transform.position);
    }

    public void AiDeathAudio()
    {
        AudioSource.PlayClipAtPoint(_aiDeath, _audioSource.transform.position);
    }

    public void BarrierShotAudio()
    {
        AudioSource.PlayClipAtPoint(_barrierShot, _audioSource.transform.position);
    }

    public void EnemyReachedTheEndAudio()
    {
        AudioSource.PlayClipAtPoint(_enemyReachedEnd, _audioSource.transform.position);
    }

    public void GameStartWarning()
    {
        AudioSource.PlayClipAtPoint(_warning, _audioSource.transform.position);
    }
}
