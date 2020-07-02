using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edible : MonoBehaviour
{

	[Tooltip("Min saturation value of this food")]
	public float minSaturation = 0f;

	[Tooltip("Max saturation value of this food")]
	public float maxSaturation = 10f;

	public float Eat() {
		GameObject.Destroy(gameObject);
		// TODO particles
		return Random.Range(minSaturation, maxSaturation);
	}
}
