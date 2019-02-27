using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObject : MonoBehaviour {

	public static GetObject Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    public void DisplayChild()
    {
        StartCoroutine(WaitAlittle());
        this.transform.GetChild(0).gameObject.SetActive(true);

    }

    IEnumerator WaitAlittle()
    {
        yield return new WaitForSeconds(0.7f);

    }

}
