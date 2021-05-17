using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    private bool _isFiring = false;

    private bool[] _weaponStatus = new bool[1];

    private List<WeaponInterface> _inventory = new List<WeaponInterface>();

    private WeaponInterface _equipped = null;

    private AudioSource audioSource;

    private List<GameObject> _bulletHoles = new List<GameObject>();

    private int _maxBulletHoles = 5;

    public GameObject ammoCounter;

    public GameObject totalAmmoCounter;

    public List<AudioClip> audioClips = new List<AudioClip>();

    public GameObject audioManager;

    public GameObject bulletHolePrefab;

    void Start()
    {
        // Initialise Weapons
        _weaponStatus[0] = true;
        WeaponKalashnikov ak = new WeaponKalashnikov();
        WeaponInterface converted = (WeaponInterface) ak;
        _inventory.Add(converted);

        // Initialise Ui 
        updateUi();

        // Initialise Audio
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // if fire key pressed & have weapon
        if (Input.GetMouseButtonDown(0) && haveWeapon())
        {
            _isFiring = true;
            StartCoroutine(fire());
        }
        if (Input.GetMouseButtonUp(0) && haveWeapon())
        {
            _isFiring = false;
        }

        if (Input.GetKeyDown(KeyCode.R) && getEquipped().canReload())
        {
            Debug.Log("Can reload / Currently reloading");
            getEquipped().reload();
            reloadSound();
        }

        updateUi();
    }

    /// <summary>
    /// Will eventually take arguments about which weapon
    /// returns if it is/can be equipped
    /// </summary>
    /// <returns></returns>
    public bool haveWeapon()
    {
        return true;
    }

    /// <summary>
    /// Gets currently equipped weapon
    /// </summary>
    /// <returns></returns>
    public WeaponInterface getEquipped()
    {
        if (_equipped == null)
        {
            _equipped = _inventory[0];
        }
        return _equipped;
    }

    IEnumerator fire()
    {
        WeaponInterface equipped = getEquipped();
        float rate = equipped.getRateOfFire();
        int ammo = equipped.getAmmoInMagezine();
        for (int i = 0; _isFiring == true && ammo > 0; i++)
        {
            fireSound();
            Debug.Log("Firing");
            ammo--;
            equipped.setAmmoInMagezine(ammo);
            hitscan();
            yield return new WaitForSeconds(rate);
        }
    }

    public void hitscan()
    {
        Camera camera = gameObject.GetComponentInChildren<Camera>();

        Vector3 rayOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        // Declare a raycast hit to store information about what our raycast has hit
        RaycastHit hit;

        // 1000.0f represents range of weapon
        // may want to turn this into a const or WeaponInterface method
        if (Physics.Raycast(rayOrigin, camera.transform.forward, out hit, 1000.0f))
        {
            ActorInterface actor = hit.collider.GetComponent<ActorInterface>();

            if (actor != null)
            {
                int damage = getEquipped().getDamage();
                actor.applyDamage(damage);
                return;
            }

            BoxCollider boxCollider = hit.collider.GetComponent<BoxCollider>();
            MeshCollider meshCollider = hit.collider.GetComponent<MeshCollider>();

            if (boxCollider != null || meshCollider != null)
            {
                // Add bullet holes to wall / box etc
                //Vector3 point = hit.point;
                Vector3 point = new Vector3(hit.point.x, hit.point.y, hit.point.z - 0.05f);
                GameObject newBulletHole = Instantiate(bulletHolePrefab, point, Quaternion.LookRotation(hit.normal));

                // todo not working - not sure if not getting added to list or not getting destroyed 
                // If max bullet holes reached - destroy first created
                if (_bulletHoles.Count > _maxBulletHoles)
                {
                    Debug.Log("Greater than max - " + _maxBulletHoles);
                    Debug.Log(_bulletHoles.Count);
                    Debug.Log(_bulletHoles.ToArray().ToString());
                    Destroy(_bulletHoles[0]);
                }
                _bulletHoles.Add(newBulletHole);
            }
        }
    }

    /// <summary>
    /// Todo enhance to contain gun sound depending on equipped weapon
    /// Plays gunshot sound
    /// </summary>
    public void fireSound()
    {
        AudioManager audioManagerSource = audioManager.GetComponent<AudioManager>();
        audioManagerSource.playFireSound(0);
    }

    /// <summary>
    /// Todo enhance to contain reload sound depending on equipped weapon
    /// Plays reload sound
    /// </summary>
    public void reloadSound()
    {
        AudioManager audioManagerSource = audioManager.GetComponent<AudioManager>();
        audioManagerSource.playReloadSound(0);
    }

    private void updateUi()
    {
        ammoCounter.GetComponent<Text>().text = getEquipped().getAmmoInMagezine().ToString();
        totalAmmoCounter.GetComponent<Text>().text = getEquipped().getTotalAmmo().ToString();
    }
}
