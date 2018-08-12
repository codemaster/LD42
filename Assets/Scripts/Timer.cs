using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
	public UnityEvent TimerStarted = new UnityEvent();
	public UnityEvent TimerElapsed = new UnityEvent();
	public uint StartTimeSeconds;
	public float CurrentTimeRemaining { get; private set; }

	[SerializeField]
	private bool _enabled;

	[SerializeField]
	private TMP_Text _text;

	public void StartTimer()
	{
		CurrentTimeRemaining = StartTimeSeconds;
		_enabled = true;
		TimerStarted.Invoke();
	}

	public void StopTimer()
	{
		_enabled = false;
		CurrentTimeRemaining = 0.0f;
	}

	public void Reset()
	{
		CurrentTimeRemaining = StartTimeSeconds;
	}

	private void Update()
	{
		if (!_enabled)
		{
			return;
		}

		CurrentTimeRemaining -= Time.deltaTime;
		if (CurrentTimeRemaining <= 0f)
		{
			StopTimer();
			TimerElapsed.Invoke();
		}

		int minLeft = (int)CurrentTimeRemaining / 60;
		int secLeft = (int)CurrentTimeRemaining - (minLeft * 60);
		_text.text = $"{minLeft:00}:{secLeft:00}";
	}
}
