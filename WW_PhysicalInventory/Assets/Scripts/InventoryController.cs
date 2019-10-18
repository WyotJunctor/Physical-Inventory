using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WW
{
    public class InventoryController : MonoBehaviour
    {
        [HideInInspector]
        public bool can_input = true;
        bool in_inventory;
        
        Transform inventory_holder;
        Inventory inventory_box;
        InventoryItem selected_item;
        Camera inventory_camera, main;
        LayerMask inventory_mask, inventory_box_mask;

        private void Awake()
        {
            inventory_holder = transform.FindDeepChild("Inventory");
            inventory_box = transform.FindDeepChild("InventoryBox").GetComponent<Inventory>();
            inventory_camera = transform.FindDeepChild("InventoryCamera").GetComponent<Camera>();
            main = Camera.main;
            inventory_holder.gameObject.SetActive(false);
            inventory_mask = LayerMask.GetMask("Inventory");
            inventory_box_mask = LayerMask.GetMask("InventoryBox");
        }

        // Update is called once per frame
        void Update()
        {
            if (can_input)
            {
                bool inventory_input = Input.GetButtonDown("Inventory");
                if (inventory_input)
                {
                    if (selected_item && in_inventory)
                        Deselect();
                    in_inventory = !in_inventory;
                    inventory_holder.gameObject.SetActive(!inventory_holder.gameObject.activeSelf);
                }
            }
            if (in_inventory)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 mouse_dir = main.ScreenToViewportPoint(Input.mousePosition);
                    Ray ray = inventory_camera.ViewportPointToRay(mouse_dir);
                    if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, inventory_mask))
                    {
                        Transform objectHit = hit.transform;
                        if (selected_item != objectHit.GetComponent<InventoryItem>())
                        {
                            selected_item = objectHit.GetComponent<InventoryItem>();
                            selected_item.Select();
                        }
                        else
                            Deselect();
                    }
                    else
                        Deselect();
                }
            }
            if (selected_item)
            {
                Vector3 mouse_dir = main.ScreenToViewportPoint(Input.mousePosition);
                Ray ray = inventory_camera.ViewportPointToRay(mouse_dir);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, inventory_box_mask))
                {
                    Debug.DrawLine(hit.point, hit.normal);
                    selected_item.Move(hit.point);
                }
            }
        }

        public void Deselect()
        {
            if (selected_item)
                selected_item.Deselect();
            selected_item = null;
        }

        public void AddItem(GameObject item)
        {
            if (item)
            {
                GameObject obj = Instantiate(item, inventory_box.transform);
                obj.transform.parent = inventory_box.transform;
                obj.transform.position = transform.position;
                obj.transform.GetChild(0).up = Random.insideUnitSphere;
            }
        }
    }
}
