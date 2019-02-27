using UnityEngine;
using System.Collections;

public class Done_WeaponController : MonoBehaviour
{
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float delay;
    [SerializeField] bool fire;
	void Start ()
	{
       // if (fire)
		//InvokeRepeating ("Fire", delay, fireRate);
	}

	void Fire ()
	{
		Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		GetComponent<AudioSource>().Play();
	}
}
