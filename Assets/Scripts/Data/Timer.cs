using System;
using System.Timers;

public class Timer : IDisposable  {

	private int hours;
	private int minutes;
	private int seconds;

	public int this[int index] {
		get {
			switch(index) {
				case 0:
					return seconds;
				case 1:
					return minutes;
				case 2:
					return hours;
				default:
					throw new IndexOutOfRangeException(	);
			}
		}
	}

	public int Hours {
		get {
			return hours;
		}
	}

	public int Minutes {
		get {
			return minutes;
		}
	}

	public int Seconds {
		get {
			return seconds;
		}
	}

	System.Timers.Timer timerObject = new System.Timers.Timer();

	public delegate void OnTickHandler(Timer sender);

	public event OnTickHandler OnTick;
	
	/// <summary>
	/// Component to be count time.
	/// Uses the Timer class, so be sure to call Dispose()
	/// when you are done to prevent memory leaks.
	/// </summary>
	/// <param name="seconds"></param>
	/// <param name="minutes"></param>
	/// <param name="hours"></param>
	public Timer(int seconds = 0, int minutes = 0, int hours = 0) {
		this.hours = hours;
		this.minutes = minutes;
		this.seconds = seconds;

		timerObject = new System.Timers.Timer();
		timerObject.BeginInit();
		timerObject.Interval = 1000; //in MS
		timerObject.Elapsed += Tick;
	}

	private void Tick(object sender, ElapsedEventArgs e) {
		seconds++;
		CheckTime();
		OnTick.Invoke(this);
	}

	private void CheckTime() {
		CheckPassedNumber(60, ref seconds, ref minutes);
		CheckPassedNumber(60, ref minutes, ref hours);

		CheckLowerNumber(ref seconds, ref minutes, 60);
		CheckLowerNumber(ref minutes, ref hours, 60);
	}

	private void CheckPassedNumber(int passedValue, ref int smallValue, ref int bigValue) {
		if(smallValue >= passedValue) {
			smallValue = 0;
			bigValue++;
		}
	}

	private void CheckLowerNumber(ref int smallValue, ref int bigValue, int maxValue) {
		if(smallValue < 0) {
			bigValue--;
			smallValue = maxValue + smallValue;
		}
	}

	public void Start() {
		timerObject.Start();
	}

	public void Stop() {
		timerObject.Stop();
	}

	public void Dispose() {
		timerObject.Dispose();
	}

	public override string ToString() {
		return hours.ToString("D2") + ":" + minutes.ToString("D2") + ":" + seconds.ToString("D2");
	}
}
