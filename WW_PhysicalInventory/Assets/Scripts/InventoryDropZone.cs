using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW
{
    public class InventoryDropZone : MonoBehaviour
    {
        Collider coll;
        private void Awake()
        {
            coll = GetComponent<Collider>();
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.GetComponent<InventoryItem>())
            {
                collision.collider.GetComponent<InventoryItem>().Drop();
                Physics.IgnoreCollision(collision.collider, coll);
            }
        }
    }
}
