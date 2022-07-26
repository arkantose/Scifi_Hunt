using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("The UI Manager is Null");

            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }

    //Game Over
    [SerializeField]
    GameObject _gameOver;
    [SerializeField]
    GameObject _youWin;
    //Warning Object
    [SerializeField]
    GameObject _warningmenu;
    //Ammo int counter
    private int _ammoLeft = 25;
    //AI Escape Count
    private int _livesLeft = 3;
    //Player Points
    private int _playerPoints = 0;
    //Text UI
    [SerializeField]
    TextMeshProUGUI _score;
    [SerializeField]
    TextMeshProUGUI _lives;
    [SerializeField]
    TextMeshProUGUI _ammo;

    private void Start()
    {
        StartCoroutine(WarningIntroFlash());
    }
    public void LivesLeft()
    {
        if (_livesLeft > 0)
        {
            _livesLeft--;
            _lives.text = $"{_livesLeft}";
        }
        if (_livesLeft == 0)
        {
            _gameOver.SetActive(true);
            SpawnManager.Instance.StopAllCoroutines();
        }
    }
    public void YouWin()
    {
        if(_playerPoints == 2500)
        _youWin.SetActive(true);
    }
    public void AmmoCount()
    {
        _ammoLeft--;
        _ammo.text = $"{_ammoLeft}";
    }

    public void PlayerPointsIncrease()
    {
        _playerPoints += 50;
        _score.text = $"{_playerPoints}";
    }

    private IEnumerator WarningIntroFlash()
    {
        yield return new WaitForSeconds(.5f);
        _warningmenu.SetActive(true);
        yield return new WaitForSeconds(.5f);
        _warningmenu.SetActive(false);
        yield return new WaitForSeconds(.5f);
        _warningmenu.SetActive(true);
        yield return new WaitForSeconds(.5f);
        _warningmenu.SetActive(false);
        yield return new WaitForSeconds(.5f);
        _warningmenu.SetActive(true);
        yield return new WaitForSeconds(.5f);
        _warningmenu.SetActive(false);
    }


}
