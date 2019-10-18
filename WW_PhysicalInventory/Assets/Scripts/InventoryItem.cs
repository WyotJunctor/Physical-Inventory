using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW
{
    [RequireComponent(typeof(Rigidbody))]
    public class InventoryItem : MonoBehaviour
    {
        bool selected;
        InventoryAnchor anchor;
        float shrink_speed = 5f;
        Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            anchor = transform.parent.GetComponentInChildren<InventoryAnchor>();
        }

        public void Deselect()
        {
            anchor.Deselect();
        }

        public void Select()
        {
            anchor.Select();
        }

        public void Move(Vector3 target)
        {
            anchor.Follow(target);
        }

        public void Drop()
        {
            StartCoroutine(Shrink());
        }

        IEnumerator Shrink()
        {
            while (transform.localScale.x > 0.01f)
            {;
                transform.localScale -= Vector3.one * shrink_speed * Time.deltaTime;
                yield return null;
            }
            Destroy(transform.parent.gameObject);
        }
    }
}