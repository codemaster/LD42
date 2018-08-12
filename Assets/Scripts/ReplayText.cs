using TMPro;
using UnityEngine;
using Zenject;

public class ReplayText : MonoBehaviour
{
	[Inject]
	private Timer _timer;

	[Inject]
	private InputHandler _input;

	[SerializeField]
	private TMP_Text _text;

	private void Awake()
	{
		_timer.TimerStarted.AddListener(OnTimerStarted);
		_timer.TimerElapsed.AddListener(OnTimerEnded);
	}

	private void OnTimerStarted()
	{
		_text.enabled = false;
		_input.Triggered.RemoveListener(OnInput);
	}

	private void OnTimerEnded()
	{
		_text.enabled = true;
		_input.Triggered.AddListener(OnInput);
	}

	private void OnDestroy()
	{
		_timer.TimerStarted.RemoveListener(OnTimerStarted);
		_timer.TimerElapsed.RemoveListener(OnTimerEnded);
		_input.Triggered.RemoveListener(OnInput);
	}

	private void OnInput()
	{
		_timer.StartTimer();
	}
}
