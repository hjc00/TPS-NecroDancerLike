using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectSphere : MonoBehaviour
{

    private EnemyController enemyController;

    void Start()
    {
        enemyController = this.transform.parent.GetComponent<EnemyController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            enemyController.isFindPlayer = true;
            enemyController.SetPlayer(other.transform.gameObject);
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        enemyController.isHitPlayer = false;
    //    }
    //}
}
