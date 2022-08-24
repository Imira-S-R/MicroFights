public class Weapon
{
    public string name;
    public int number_of_bullets;
    public float waiting_time;
    public float reloading_time;
    public float bulletForce;
    public int price;


    public Weapon(string name, int number_of_bullets, float waiting_time, float reloading_time, int price, float bulletForce)
    {
        this.name = name;
        this.number_of_bullets = number_of_bullets;
        this.waiting_time = waiting_time;
        this.reloading_time = reloading_time;
        this.price = price;
        this.bulletForce = bulletForce;
    }

}

public class WeaponData
{
    public static Weapon weapon1 = new Weapon("Pistol", 20, 0.2f, 2f, 0, 20f);
    public static Weapon weapon2 = new Weapon("Pistol MX23", 30, 0.2f, 1.6f, 5000, 25f);
    public static Weapon weapon3 = new Weapon("Pistol Combat II", 40, 0.16f, 1.3f, 10000, 29f);
    public static Weapon weapon4 = new Weapon("Shooter PRO", 60, 0.13f, 1f, 20000, 30f);
    public static Weapon weapon5 = new Weapon("Shooter MAX", 80, 0.1f, 0f, 50000, 33f);

}