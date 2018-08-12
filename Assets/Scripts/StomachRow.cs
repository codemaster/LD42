using System.Collections.Generic;

public class StomachRow
{
	public int RowFill
	{
		get
		{
			int fill = 0;
			foreach (var sushi in _sushi)
			{
				fill += sushi.Size;
			}
			return fill;
		}
	}
	
	private readonly int _width;
	private readonly IList<Sushi> _sushi = new List<Sushi>();
	
	public StomachRow(int width)
	{
		_width = width;
	}

	public void Add(Sushi sushi)
	{
		_sushi.Add(sushi);
	}

	public void Remove(Sushi sushi)
	{
		_sushi.Remove(sushi);
	}

	public bool HasRoom(Sushi sushi)
	{
		return (sushi.Size + RowFill) <= _width;
	}

	public void Digest(float digestAmount)
	{
		foreach (var sushi in _sushi)
		{
			sushi.Digest(digestAmount);
		}
	}
}
