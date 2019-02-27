using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowManyDogs : MonoBehaviour {

    void OnTriggerEnter(Collider other){
        if (other.tag == "Boundary" || other.tag == "Enemy")
        {
            return;
        }

        PlayerPrefs.SetInt("Dogs", PlayerPrefs.GetInt("Dogs") + 1);
    }

}
