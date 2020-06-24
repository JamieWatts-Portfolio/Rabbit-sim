using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotator : MonoBehaviour
{

    public float speed = 0.5f;

    // Update is called once per frame
    void Update() => gameObject.transform.Rotate(0, Time.deltaTime * speed, 0);
}
