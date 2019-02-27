using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour {
		public float scrollSpeed; 
		public float tileSizeZ; // 30

		private Vector3 startPosition;

		void Start ()
		{
			//Get the position of the First Object 
			startPosition = transform.position;
		}

		void Update ()
		{
			
			float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
			transform.position = startPosition + Vector3.forward * newPosition;
		}
}