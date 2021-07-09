using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCol : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D colInfo)
    {
        if(colInfo.gameObject.tag=="Player"){
            Debug.Log("working");
            Destroy(gameObject);
        }

        if(colInfo.relativeVelocity.magnitude>5.0f){
            Destroy(gameObject);
        }
       
    }
}
