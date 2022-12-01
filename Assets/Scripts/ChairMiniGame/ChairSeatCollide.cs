using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChairSeatCollide : MonoBehaviour
{

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Chair"))
            collision.gameObject.GetComponent<FallingChair>().CollectChair();
    }
}
