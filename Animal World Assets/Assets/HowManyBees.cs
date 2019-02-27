using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowManyBees : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary" || other.tag == "Enemy")
        {
            return;
        }

        PlayerPrefs.SetInt("Bees", PlayerPrefs.GetInt("Bees") + 1);
    }

}
