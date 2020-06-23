using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit_Burrow : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Rabbit;
    void Start()
    {
        var newRabbit = Instantiate(Rabbit, transform.position , Quaternion.identity);
        newRabbit.tag = Literals.TAG_RABBIT;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
