using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Leaderboard : MonoBehaviour
{
	private static string Key = "NomLeaderboard";
	private static string Default = JsonUtility.ToJson(new LeaderboardData());

	[Inject]
	private Timer _timer;

	[SerializeField]
	private PointCounter _pointCounter;

	[SerializeField]
	private Image _panel;

	[SerializeField]
	private TMP_Text _text;

	[SerializeField]
	private int _maxScores = 10;
	
	public static LeaderboardData Load()
	{
		var jsonData = PlayerPrefs.GetString(Key, Default);
		var data = JsonUtility.FromJson<LeaderboardData>(jsonData);
		return data ?? new LeaderboardData();
	}

	public static void Save(LeaderboardData data)
	{
		PlayerPrefs.SetString(Key, JsonUtility.ToJson(data));
	}

	private void Awake()
	{
		_timer.TimerElapsed.AddListener(OnTimerElapsed);
		_timer.TimerStarted.AddListener(OnTimerStarted);
	}

	private void OnDestroy()
	{
		_timer.TimerElapsed.RemoveListener(OnTimerElapsed);
		_timer.TimerStarted.RemoveListener(OnTimerStarted);
	}

	private void OnTimerElapsed()
	{
		_panel.enabled = true;
		_text.enabled = true;

		var data = Load();
		data.Scores.Add(_pointCounter.Points);
		data.Scores.Sort();
		data.Scores.Reverse();

		while(data.Scores.Count > _maxScores)
		{
			data.Scores.RemoveAt(data.Scores.Count - 1);
		}

		Save(data);

		_text.text = "";
		for (int i = 0; i < data.Scores.Count; ++i)
		{
			_text.text += $"{i+1}. {data.Scores[i]}\n";
		}
	}

	private void OnTimerStarted()
	{
		_panel.enabled = false;
		_text.enabled = false;
	}
}
