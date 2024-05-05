using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] List<Gun> guns = new List<Gun>();
    [SerializeField] bool isPlayerControlled = false;
    InputManager inputManager;
    int equippedGunIndex = 0;

    void Start()
    {
        inputManager = InputManager.instance;
        SetUpGuns();
    }

    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if (!isPlayerControlled)
        {
            return;
        }

        if (Time.timeScale == 0)
        {
            return;
        }

        if (guns.Count > 0)
        {
            if (inputManager.CycleWeaponInput() != 0)
            {
                CycleEquippedGun();
            }
            if (inputManager.NextWeaponPressed())
            {
                GoToNextWeapon();
            }
            if (inputManager.PreviousWeaponPressed())
            {
                GoToPreviousWeapon();
            }
        }
    }

    public void GoToNextWeapon()
    {
        int maximumAvailableGunIndex = guns.Count - 1;
        int equippedAvailableGunIndex = guns.IndexOf(guns[equippedGunIndex]);

        equippedAvailableGunIndex += 1;
        if (equippedAvailableGunIndex > maximumAvailableGunIndex)
        {
            equippedAvailableGunIndex = 0;
        }

        EquipGun(guns.IndexOf(guns[equippedAvailableGunIndex]));
    }

    public void GoToPreviousWeapon()
    {
        int maximumAvailableGunIndex = guns.Count - 1;
        int equippedAvailableGunIndex = guns.IndexOf(guns[equippedGunIndex]);

        equippedAvailableGunIndex -= 1;
        if (equippedAvailableGunIndex < 0)
        {
            equippedAvailableGunIndex = maximumAvailableGunIndex;
        }

        EquipGun(guns.IndexOf(guns[equippedAvailableGunIndex]));
    }

    void CycleEquippedGun()
    {
        float cycleInput = inputManager.CycleWeaponInput();
        int maximumAvailableGunIndex = guns.Count - 1;
        int equippedAvailableGunIndex = guns.IndexOf(guns[equippedGunIndex]);
        if (cycleInput < 0)
        {
            equippedAvailableGunIndex += 1;
            if (equippedAvailableGunIndex > maximumAvailableGunIndex)
            {
                equippedAvailableGunIndex = 0;
            }
        }
        else if (cycleInput > 0)
        {
            equippedAvailableGunIndex -= 1;
            if (equippedAvailableGunIndex < 0)
            {
                equippedAvailableGunIndex = maximumAvailableGunIndex;
            }
        }

        EquipGun(guns.IndexOf(guns[equippedAvailableGunIndex]));
    }

    public void EquipGun(int gunIndex)
    {
        equippedGunIndex = gunIndex;
        guns[equippedGunIndex].gameObject.SetActive(true);
        for (int i = 0; i < guns.Count; i++)
        {
            if (equippedGunIndex != i)
            {
                guns[i].gameObject.SetActive(false);
            }
        }
    }

    void SetUpGuns()
    {
        foreach(Gun gun in guns)
        {
            if (gun != null)
            {
                if (guns[equippedGunIndex] == gun)
                {
                    gun.gameObject.SetActive(true);
                }
                else
                {
                    gun.gameObject.SetActive(false);
                }
            }
        }
    }

    public void MakeGunAvailable(int gunIndex)
    {
        if (gunIndex < guns.Count && guns[gunIndex] != null)
        {
            EquipGun(gunIndex);
        }
    }
}
