using TMPro;
using UnityEngine;
using Zenject;

public class WelcomeText : MonoBehaviour
{
	[Inject]
	private Timer _timer;

	[Inject]
	private InputHandler _input;

	[SerializeField]
	private TMP_Text _text;

	private void Awake()
	{
		_input.Triggered.AddListener(Triggered);
	}

	private void Triggered()
	{
		_text.enabled = false;
		_input.Triggered.RemoveListener(Triggered);
		_timer.StartTimer();
	}
}
