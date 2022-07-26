using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject _fireGun;
    [SerializeField]
    Camera _camera;
    [SerializeField]
    GameObject _bulletHole;
    [SerializeField]
    private float _cooldown = 0;
    private int _ammoCount = 25;
    private bool _gameOver;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IncreaseAmmo());
    }

    // Update is called once per frame
    void Update()
    {

        ShootTarget();
    }

    private void ShootTarget()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _cooldown < Time.time && _ammoCount >= 1)
        {

            Ray rayOrigin = _camera.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
            RaycastHit hitInfo;
            _ammoCount--;
            _cooldown = Time.time + 1;
            StartCoroutine(WeaponEffectsForEnemy());
            UIManager.Instance.AmmoCount();

            if (Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity, 1 << 3))
            {
                UIManager.Instance.PlayerPointsIncrease();
                hitInfo.transform.SendMessage("EnemyIsHit");
                return;
            }

            if (Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity, 1 << 6))
            {
                Vector3 bulletHole = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z + .1f);
                StartCoroutine(WeaponEffectsForBarrier());
                Instantiate(_bulletHole, bulletHole, Quaternion.LookRotation(hitInfo.normal));
                return;
            }

            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                Vector3 bulletHole = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z + .1f);
                Instantiate(_bulletHole, bulletHole, Quaternion.LookRotation(hitInfo.normal));
                return;
            }
        }
    }
    IEnumerator WeaponEffectsForEnemy()
    {
        AudioManager.Instance.ShootingAudio();
        _fireGun.SetActive(true);
        yield return new WaitForSeconds(.1f);
        _fireGun.SetActive(false);

    }
    IEnumerator WeaponEffectsForBarrier()
    {
        AudioManager.Instance.ShootingAudio();
        _fireGun.SetActive(true);
        yield return new WaitForSeconds(.1f);
        _fireGun.SetActive(false);
        AudioManager.Instance.BarrierShotAudio();

    }

    IEnumerator IncreaseAmmo()
    {
        while (_gameOver == false)
        {
            yield return new WaitForSeconds(60f);
            _ammoCount += 11;
            UIManager.Instance.AmmoCount();

        }
    }
}
