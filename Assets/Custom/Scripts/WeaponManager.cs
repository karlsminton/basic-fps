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

    public GameObject ammoCounter;

    public GameObject totalAmmoCounter;

    public List<AudioClip> audioClips = new List<AudioClip>();

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

    public void updateUi()
    {
        ammoCounter.GetComponent<Text>().text = getEquipped().getAmmoInMagezine().ToString();
        totalAmmoCounter.GetComponent<Text>().text = getEquipped().getTotalAmmo().ToString();
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
            yield return new WaitForSeconds(rate);
        }
    }

    /// <summary>
    /// Todo enhance to contain gun sounds depending on equipped weapon
    /// Plays gunshot sounds
    /// </summary>
    public void fireSound()
    {
        if (audioSource.isPlaying == true)
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            newSource.clip = audioClips[0];
            newSource.Play();
        }
        else
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
        }
    }
}
