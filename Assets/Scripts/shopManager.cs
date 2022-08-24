using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class shopManager : MonoBehaviour
{
    public Weapon[] weapons;
    public int current_index = 0;

    public Text name;
    public Text bullets;
    public Text reloading_time;
    public Text wait_time;
    public Text price;
    public Text coins;

    public Sprite[] guns;
    public bool isBought = false;

    public GameObject gameObject;

    [HideInInspector]
    public Image sr;

    bool isEquipped;

    string weapons_owned;
    string[] weapons_owned_array;
    

    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<Image>();
        weapons = new Weapon[] { WeaponData.weapon1, WeaponData.weapon2, WeaponData.weapon3, WeaponData.weapon4, WeaponData.weapon5};
        
    }

    // Update is called once per frame
    void Update()
    {
        coins.text = PlayerPrefs.GetInt("Coins").ToString();
        sr.sprite = guns[current_index];
        name.text = weapons[current_index].name;
        bullets.text = "Bullets: " + weapons[current_index].number_of_bullets.ToString();
        reloading_time.text = "Reloading Time: " + weapons[current_index].reloading_time.ToString() +" Seconds";
        wait_time.text = "Waiting Time: " + weapons[current_index].waiting_time.ToString() + " Seconds";
        weapons_owned = PlayerPrefs.GetString("Weapons_Owned");
        weapons_owned_array = weapons_owned.Split(',');


        if (isEquipped)
        {
            price.text = "Equipped";
            Debug.Log("Done");
        }

        checkItem(false);

    }

    public void checkItem (bool toEquip)
    {

        isEquipped = checkIfEquipped();

        foreach (var item in weapons_owned_array)
        {
            if (item == weapons[current_index].name && isEquipped == false)
            {
                isBought = true;
                price.text = "Equip";
            }
            else if (isBought == false && isEquipped == false)
            {
                price.text = "Price: " + weapons[current_index].price.ToString();
            } else if (isEquipped == true)
            {
                price.text = "Equipped";
            }
        }



        isBought = false;


        if (toEquip == true)
        {
            string equipped = PlayerPrefs.GetString("Equipped");
            equipped = weapons[current_index].name;
            PlayerPrefs.SetString("Equipped", equipped);
            Debug.Log("Equipped" + equipped);
            PlayerPrefs.Save();
        }
    }

    public bool checkIfEquipped ()
    {
        string equipped = PlayerPrefs.GetString("Equipped");

        if (weapons[current_index].name == equipped)
        {
            price.text = "Equipped";
            Debug.Log(weapons[current_index].name + "is equipped");
            return true;
        }
        return false;
    }

    public bool checkEquipped ()
    {
        foreach (var item in weapons_owned_array)
        {
            if (item == weapons[current_index].name)
            {
                return true;
            }
        }

        return false;
    }

    public void moveForward()
    {
        if (current_index != (weapons.Length - 1))
        {
            current_index += 1;
        } else
        {
            current_index = current_index;
        }
    }

    public void moveBackward ()
    {
        if (current_index != 0)
        {
            current_index -= 1;
        } else
        {
            current_index = current_index;
        }
    }

    public void buy ()
    {
        bool isAlreadyBought = false;

        foreach (var item in weapons_owned_array)
        {
            if (weapons[current_index].name == item)
            {
                isAlreadyBought = true;
            }
        }


        bool isEquipped = checkEquipped();
        int price = weapons[current_index].price;
        int coins = PlayerPrefs.GetInt("Coins");
        if (coins > price && isAlreadyBought != true)
        {
            int new_amount_of_coins = coins - price;
            PlayerPrefs.SetInt("Coins", new_amount_of_coins);
            string weapons_owned = PlayerPrefs.GetString("Weapons_Owned");
            weapons_owned += weapons[current_index].name + ",";
            PlayerPrefs.SetString("Weapons_Owned", weapons_owned);
            PlayerPrefs.Save();
            checkItem(true);
            Debug.Log(weapons_owned);
        }

        if (isEquipped)
        {
            string equipped = PlayerPrefs.GetString("Equipped");
            equipped = weapons[current_index].name;
            PlayerPrefs.SetString("Equipped", equipped);
            Debug.Log("Equipped" + equipped);
            PlayerPrefs.Save();
        }

        isAlreadyBought = false;

        Debug.Log(isAlreadyBought);
        
    }

    public void returnHome ()
    {
        SceneManager.LoadScene(0);
    }

}
