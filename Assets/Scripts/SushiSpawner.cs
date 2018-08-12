using UnityEngine;
using Zenject;

public class SushiSpawner : MonoBehaviour
{
	[SerializeField]
	private float _spawnTime;

	[SerializeField]
	private float _timeSinceLastSpawn;

	[Inject]
	private Timer _timer;

	[Inject]
	private SushiTreadmill _treadmill;

	[Inject]
	private Sushi.Factory _factory;

	private bool _spawning;

	private void Awake()
	{
		_timer.TimerElapsed.AddListener(StopSpawning);
		_timer.TimerStarted.AddListener(StartSpawning);
	}

	private void OnDestroy()
	{
		_timer.TimerElapsed.RemoveListener(StopSpawning);
		_timer.TimerStarted.RemoveListener(StartSpawning);
	}

	private void Update()
	{
		if (!_spawning)
		{
			return;
		}

		_timeSinceLastSpawn += Time.deltaTime;

		if (_timeSinceLastSpawn >= _spawnTime)
		{
			// Spawn a sushi here
			var sushi = _factory.Create(_treadmill);

			_timeSinceLastSpawn -= _spawnTime;
		}
	}

	private void StartSpawning()
	{
		_spawning = true;
	}

	private void StopSpawning()
	{
		_spawning = false;
	}
}
