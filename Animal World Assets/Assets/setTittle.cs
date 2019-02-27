using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setTittle : MonoBehaviour {

    [SerializeField] GameObject Spanish;
    [SerializeField] GameObject English;

    void Start()
    {
        if (PlayerPrefs.GetInt("Lan") == 1)
        {
            Spanish.SetActive(true);
            English.SetActive(false);
        }
        else {
            English.SetActive(true);
            Spanish.SetActive(false);

        }
            
    }

}
