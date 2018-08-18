using UnityEngine;
using Zenject;

public class Sushi : MonoBehaviour
{
	public SushiEvent Digested = new SushiEvent();
	public SushiEvent Offscreen = new SushiEvent();

	public int Size { get; private set; }
	public SushiType Type { get; private set; }
	public int Points { get; private set; }
	public bool Digesting { get; set; }

	[SerializeField]
	private float _remainingDigestTime;

	private SushiTreadmill _treadmill;
	private bool _initialized;
	private GameObject _visuals;
	private RectTransform _uiArea;

	public void Initialize(SushiInfo info, SushiTreadmill treadmill, RectTransform uiArea)
	{
		_treadmill = treadmill;
		_uiArea = uiArea;
		Digesting = false;
		
		Size = info.Size;
		Type = info.Type;
		Points = info.Points;
		_remainingDigestTime = info.DigestTime;

		if (_visuals != null)
		{
			Destroy(_visuals);
		}

		_visuals = Instantiate(info.Prefab);
		_visuals.transform.SetParent(transform, false);

		_initialized = true;
	}

	public float Digest(float amount)
	{
		_remainingDigestTime -= amount;
		return _remainingDigestTime;
	}

	private void Update()
	{
		if (Digesting)
		{
			if (_remainingDigestTime <= 0f)
			{
				Digested.Invoke(this);
				Digesting = false;
				_initialized = false;
			}
			return;
		}
		
		if (!_initialized)
		{
			return;
		}

		Vector3 pos = transform.position;
		pos.x += Time.deltaTime * _treadmill.MovementAmount;
		transform.position = pos;

		var rectTransform = _visuals.GetComponent<RectTransform>();
		if (!_uiArea.Overlaps(rectTransform))
		{
			Offscreen.Invoke(this);
		}
	}

	private void OnDisable()
	{
		if (_visuals != null)
		{
			Destroy(_visuals);
		}
	}

	private void Awake()
	{
		this.gameObject.AddComponent<RectTransform>();
	}

	public class Factory : PlaceholderFactory<SushiTreadmill, Sushi>
	{
	}
}
