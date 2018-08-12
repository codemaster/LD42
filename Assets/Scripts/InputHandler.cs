using UnityEngine;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour
{
	public UnityEvent Triggered = new UnityEvent();
	
	private void Update()
	{
		if (Input.GetButtonUp("Fire1") || Input.GetButtonUp("Jump"))
		{
			Triggered.Invoke();
		}
	}
}
