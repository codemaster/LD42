using TMPro;
using UnityEngine;
using Zenject;

public class PointCounter : MonoBehaviour
{
	public IntEvent PointsChanged = new IntEvent();

	[SerializeField]
	private TMP_Text _text;

	[Inject]
	private Timer _timer;
	
	public int Points
	{
		get { return _points; }
		set {
			_points = value;
			_text.text = $"{_points}";
			PointsChanged.Invoke(_points);
		}
	}

	[SerializeField]
	private int _points;

	private void Awake()
	{
		_timer.TimerStarted.AddListener(OnTimerStarted);
	}

	private void OnDestroy()
	{
		_timer.TimerStarted.RemoveListener(OnTimerStarted);
	}

	private void OnTimerStarted()
	{
		Points = 0;
	}
}
