using UnityEngine;

public class SushiTreadmill : MonoBehaviour
{
	public float MovementAmount;

	public Vector3 SpawnPoint => (MovementAmount > 0f ? _leftSpawnPoint : _rightSpawnPoint).position;

	[SerializeField]
	private Transform _leftSpawnPoint;

	[SerializeField]
	private Transform _rightSpawnPoint;
}
