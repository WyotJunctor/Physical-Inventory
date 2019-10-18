using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW
{
    [RequireComponent(typeof(Rigidbody))]
    public class InventoryAnchor : MonoBehaviour
    {
        [HideInInspector]
        public Rigidbody rb;
        Vector3 velocity;
        float smooth_time = 0.4f, rise_distance = 4f;
        Transform inventory_box;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            inventory_box = GetComponentInParent<Inventory>().transform;
        }

        public void Select()
        {
            rb.isKinematic = true;
            StartCoroutine(Rise());
        }

        public void Deselect()
        {
            rb.isKinematic = false;
            StopAllCoroutines();
        }

        IEnumerator Rise()
        {
            while (rb.isKinematic == true)
            {
                Vector3 target = new Vector3(transform.position.x,
                    (inventory_box.transform.position.y + rise_distance),
                    transform.position.z);
                transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smooth_time);
                yield return null;
            }
        }

        public void Follow(Vector3 target)
        {
            target = new Vector3(target.x, transform.position.y, target.z);
            transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smooth_time);
        }
    }
}