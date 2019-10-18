using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW
{
    public class InventoryTester : MonoBehaviour
    {
        Transform inventory_box;
        public GameObject inventory_object;
        InventoryController inventory_controller;

        // Start is called before the first frame update
        void Awake()
        {
            inventory_controller = GetComponent<InventoryController>();
            inventory_box = transform.FindDeepChild("InventoryBox");
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
                inventory_controller.AddItem(inventory_object);
        }
    }
}
