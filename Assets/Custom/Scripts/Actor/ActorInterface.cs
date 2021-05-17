using System;

public interface ActorInterface
{
    public int health
    {
        get;
        set;
    }

    public bool canDamage();

    public bool isDead();

    public void applyDamage(int damage);

    public int getHealth();

    public void setHealth(int health);
}
