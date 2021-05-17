using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour, ActorInterface
{
    private int _health = 100;
    public int health {
        get => _health;
        set => _health = value;
    }

    public void applyDamage(int damage)
    {
        int health = getHealth();
        health -= damage;
        setHealth(health);
    }

    public bool canDamage()
    {
        return true;
    }

    public int getHealth()
    {
        return _health;
    }

    public bool isDead()
    {
        if (getHealth() <= 0)
        {
            return true;
        }
        return false;
    }

    public void setHealth(int value)
    {
        health = value;
        string gameObjectName = gameObject.name;
        Debug.Log("Damage caused to " + gameObjectName + ". Health now " + value + ".");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead() == true)
        {
            Debug.Log("Killed " + gameObject.name);
            Destroy(gameObject);
        }
    }
}
