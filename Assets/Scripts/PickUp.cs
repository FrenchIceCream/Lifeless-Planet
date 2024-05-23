using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public enum PickUpType {Ammo, Health, Key};
    [SerializeField] PickUpType pickUpType;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (pickUpType)
            {
                case PickUpType.Ammo:
                if (AmmoTracker.instance.GetAmmoCount() != AmmoTracker.instance.GetMaxAmmo())
                {
                    AmmoTracker.instance.AddAmmo(10);
                    Destroy(gameObject);
                }
                break;
                case PickUpType.Health:
                if (other.GetComponent<Health>().GetHealth() != other.GetComponent<Health>().GetMaxHealth())
                {
                    Destroy(gameObject);
                    other.GetComponent<Health>().Heal(20);
                }
                break;
                case PickUpType.Key:
                    Destroy(gameObject);
                    other.GetComponent<PlayerController>().SetKey(true);
                    FindFirstObjectByType<Portal>().ActivatePortal();
                break;
            }
        }
    }
}
