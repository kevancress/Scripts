using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ObjectData{

    public Sprite icon = null;
    public string description = "This is a generic new item description";
    public GameObject model;
    internal GameObject player;
    internal GameObject holdPosition;
   
}
