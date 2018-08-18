using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Stomach : MonoBehaviour
{
	public UnityEvent Overflowed = new UnityEvent();
	public SushiEvent Digested = new SushiEvent();

	[SerializeField]
	private float _digestAmount;
	[SerializeField]
	private int _stomachSize;
	[SerializeField]
	private TMP_Text _countdownText;

	[Inject]
	private SushiPool _sushiPool;

	[Inject]
	private Timer _timer;

	private readonly Queue<Sushi> _sushiQueue = new Queue<Sushi>();

	public bool IsDigesting(Sushi sushi) => _sushiQueue.Contains(sushi);

	public bool Eat(Sushi sushi)
	{
		if (_sushiQueue.Count >= _stomachSize)
		{
			Overflowed.Invoke();
			// Disable overflow despawning for now...
			//_sushiPool.Despawn(sushi);
			return false;
		}
		else
		{
			sushi.Digested.AddListener(FinishSushi);
			sushi.Digesting = true;
			sushi.transform.SetParent(transform, false);
			sushi.transform.SetAsFirstSibling();
			_sushiQueue.Enqueue(sushi);
			return true;
		}
	}

	private void Awake()
	{
		_timer.TimerElapsed.AddListener(Clear);
	}

	private void OnDestroy()
	{
		_timer.TimerElapsed.RemoveListener(Clear);
	}

	private void Clear()
	{
		while (_sushiQueue.Count > 0)
		{
			var sushi = _sushiQueue.Dequeue();
			_sushiPool.Despawn(sushi);
		}
	}

	private void Update()
	{
		// Digest the bottom sushi only
		_countdownText.enabled = _sushiQueue.Count > 0;
		if (_sushiQueue.Count > 0)
		{
			var timeLeft = _sushiQueue.Peek().Digest(_digestAmount * Time.deltaTime);
			_countdownText.text = $"{timeLeft:0.#}";
		}
	}

	private void FinishSushi(Sushi sushi)
	{
		if (!sushi.enabled)
		{
			return;
		}
		
		Digested.Invoke(sushi);
		_sushiPool.Despawn(_sushiQueue.Dequeue());
		//_sushiPool.Despawn(sushi);
	}
}
