using System;
using System.Collections.Generic;
using Zenject;

public class SushiFactory : IFactory<SushiTreadmill, Sushi>
{
	public IReadOnlyList<Sushi> ActiveSushi => _activeSushi.AsReadOnly();
	
	private static Array _sushiTypes = Enum.GetValues(typeof(SushiType));
	private static Random _random = new Random();
	private List<Sushi> _activeSushi = new List<Sushi>();
	private IDictionary<SushiType, SushiInfo> _sushiInfos =
		new Dictionary<SushiType, SushiInfo>();

	[Inject]
	private SushiPool _pool;
	
	public SushiFactory(SushiInfo[] sushiInfos)
	{
		// TODO
		foreach (var info in sushiInfos)
		{
			_sushiInfos[info.Type] = info;
		}
	}
	
	public Sushi Create(SushiTreadmill treadmill)
	{
		// Determine what kind of sushi to create
		var sushiType = RandomSushiType();
		// Create the sushi
		_pool.Spawn(_sushiInfos[sushiType], treadmill);
		// Set the treadmill reference
		return null;
	}

	private SushiType RandomSushiType() => (SushiType) _sushiTypes.GetValue(
		_random.Next(_sushiTypes.Length));
}
