using System;
using Godot;

public partial class RandomTimer : Timer
{
	[Export(PropertyHint.Range, "0,1000")]
	protected float minTime { get; set; }

	[Export(PropertyHint.Range, "0,50")]
	protected float maxTime { get; set; }

	public void SetUp()
	{
		WaitTime = GetRandomNumber(minTime, maxTime);
	}

	protected double GetRandomNumber(double minimum, double maximum)
	{
		Random random = new Random();
		return random.NextDouble() * (maximum - minimum) + minimum;
	}
}
