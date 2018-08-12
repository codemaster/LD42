using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
	public bool IsNapping => _napCounter > 0f;
	
	[SerializeField]
	private Stomach _stomach;

	[SerializeField]
	private PointCounter _pointCounter;

	[SerializeField]
	private float _napTimeSeconds;

	[SerializeField]
	private float _napCounter;

	[SerializeField]
	private AudioSource _audioSource;

	[SerializeField]
	private AudioClip _nomSound;

	[Inject]
	private InputHandler _inputHandler;

	[Inject]
	private SushiPool _sushiPool;

	[Inject]
	private Timer _timer;

	private Vector2 _horizontalRange;
	private bool _eatingAllowed;

	private void OnDestroy()
	{
		_timer.TimerStarted.RemoveListener(OnTimerStarted);
		_timer.TimerElapsed.RemoveListener(OnTimerEnded);
		_inputHandler.Triggered.RemoveListener(Eat);
		_stomach.Overflowed.RemoveListener(Nap);
		_stomach.Digested.RemoveListener(Digested);
	}

	private void Start()
	{
		_horizontalRange = CreateHorizontalRange();
		_napCounter = 0.0f;
		_inputHandler.Triggered.AddListener(Eat);
		_stomach.Overflowed.AddListener(Nap);
		_stomach.Digested.AddListener(Digested);
		_timer.TimerElapsed.AddListener(OnTimerEnded);
		_timer.TimerStarted.AddListener(OnTimerStarted);
	}

	private Vector2 CreateHorizontalRange()
	{
		var corners = new Vector3[4];
		GetComponent<RectTransform>().GetWorldCorners(corners);
		var left = corners[0].x;
		var right = corners[3].x;
		return new Vector2(left, right);
	}

	private void Eat()
	{
		if (!_eatingAllowed)
		{
			return;
		}

		var eatSushi = new HashSet<Sushi>();
		foreach (var sushi in _sushiPool.ActiveSushi)
		{
			if (_horizontalRange.x <= sushi.transform.position.x &&
			    _horizontalRange.y >= sushi.transform.position.x &&
			    !_stomach.IsDigesting(sushi))
			    //!sushi.transform.IsChildOf(_stomach.transform))
			{
				eatSushi.Add(sushi);
			}
		}

		var couldEat = false;
		foreach (var sushi in eatSushi)
		{
			couldEat = couldEat || _stomach.Eat(sushi);
		}

		if (couldEat)
		{
			_audioSource.PlayOneShot(_nomSound);
		}

		eatSushi.Clear();
	}

	private void Nap()
	{
		// TODO: Not going to implement this for now due to time
		_napCounter = _napTimeSeconds;
	}

	private void Digested(Sushi sushi)
	{
		_pointCounter.Points += sushi.Points;
	}

	private void Update()
	{
		_napCounter -= Time.deltaTime;
	}

	private void OnTimerStarted()
	{
		_eatingAllowed = true;
	}

	private void OnTimerEnded()
	{
		_eatingAllowed = false;
	}
}
