using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Interactable/Object")]
public class ObjectData : ScriptableObject
{

    public string lable = "New Item";
    public string action = "Pick Up";
    public string key = "F";
    public bool isFlammable = false;
    public bool isMetal = false;


}