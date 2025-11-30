using UnityEngine;

[CreateAssetMenu(fileName = "ResourceData", menuName = "Scriptable Objects/ResourceData")]
public class ResourceData : ScriptableObject
{
        public string resourceName;
    public Sprite icon;
    public Rarity rarity; // Enum: Common, Rare, Epic
    public int minDrop;
    public int maxDrop;
}
public enum Rarity { Common, Rare, Epic }