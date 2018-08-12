using UnityEngine;

[CreateAssetMenu]
public class SushiInfo : ScriptableObject
{
	public SushiType Type;
	public int Points;
	public float DigestTime;
	public int Size;
	public GameObject Prefab;
}
