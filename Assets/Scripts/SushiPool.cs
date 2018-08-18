using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SushiPool : MemoryPool<SushiInfo, SushiTreadmill, Sushi>
{
	public IReadOnlyList<Sushi> ActiveSushi => _activeSushi.AsReadOnly();
	
	private Transform _container;
	private RectTransform _uiArea;
	private readonly List<Sushi> _activeSushi = new List<Sushi>();
	
	public SushiPool(Transform container, RectTransform uiArea)
	{
		_container = container;
		_uiArea = uiArea;
	}
	
	protected override void Reinitialize(SushiInfo p1, SushiTreadmill p2, Sushi item)
	{
		base.Reinitialize(p1, p2, item);
		item.transform.SetParent(_container, false);
		item.transform.position = p2.SpawnPoint;
		item.Initialize(p1, p2, _uiArea);
		item.Offscreen.AddListener(Despawn);
		item.enabled = true;
		_activeSushi.Add(item);
	}

	protected override void OnDespawned(Sushi item)
	{
		base.OnDespawned(item);
		item.transform.SetParent(_container, false);
		item.Offscreen.RemoveListener(Despawn);
		_activeSushi.Remove(item);
		item.enabled = false;
	}
}
