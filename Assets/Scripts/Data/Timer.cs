using System;
using System.Timers;

/// <summary>
/// This class is a wrapper for the System.Timer class.
/// To make using it a lot easier as opposed to writing special
/// uses for it.
/// 
/// Exposes a Tick event that happens every second and individual
/// integers for the time.
/// 
/// Can be turned into a string with ToString().
/// 
/// This component must be Disposed properly to prevent
/// memory leaks!
/// </summary>
public class Timer : IDisposable  {

	// The data variables.
	private int hours;
	private int minutes;
	private int seconds;

	/// <summary>
	/// Use this to get data as an array.
	/// </summary>
	/// <param name="index">value between 0 and 2</param>
	/// <returns></returns>
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
					throw new IndexOutOfRangeException();
			}
		}
	}

	/// <summary>
	/// Get the current amount of hours.
	/// Does not include lower values.
	/// </summary>
	public int Hours {
		get {
			return hours;
		}
	}

	/// <summary>
	/// Get the current amount of minutes.
	/// Does not include higher or lower values.
	/// </summary>
	public int Minutes {
		get {
			return minutes;
		}
	}

	/// <summary>
	/// Get the current amount of seconds.
	/// Does not include higher values.
	/// </summary>
	public int Seconds {
		get {
			return seconds;
		}
	}

	/// <summary>
	/// Get the total seconds elapsed.
	/// Includes higher values.
	/// </summary>
	public int TotalSeconds {
		get {
			return hours * 60 * 60 + minutes * 60 + seconds;
		}
	}

	/// <summary>
	/// Use this to time the ticks.
	/// This class is not cleaned up automatically and must
	/// be disposed.
	/// </summary>
	System.Timers.Timer timerObject;

	/// <summary>
	/// Listen to an event for every second.
	/// </summary>
	/// <param name="sender">This class will be sent.</param>
	public delegate void OnTickHandler(Timer sender);

	/// <summary>
	/// Listen to my second tick.
	/// </summary>
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
		// Set up any variables if specified.
		this.hours = hours;
		this.minutes = minutes;
		this.seconds = seconds;

		// Make sure it doesn't start with any odd values like 72 seconds.
		CheckTime();

		// Set up the timer and its variables.
		timerObject = new System.Timers.Timer();
		timerObject.BeginInit();
		timerObject.Interval = 1000; //in milliseconds
		timerObject.Elapsed += Tick; // To up the seconds by one.
	}

	/// <summary>
	/// The event handler
	/// </summary>
	/// <param name="sender">Where did it come from?</param>
	/// <param name="e">Event args</param>
	private void Tick(object sender, ElapsedEventArgs e) {
		// One second passed.
		seconds++;

		// Check if I went up.
		CheckTime();

		// Invoke my event.
		OnTick.Invoke(this);
	}

	/// <summary>
	/// Make sure all values are correct.
	/// </summary>
	private void CheckTime() {
		CheckPassedNumber(60, ref seconds, ref minutes);
		CheckPassedNumber(60, ref minutes, ref hours);
	}

	/// <summary>
	/// Used to count upwards.
	/// </summary>
	/// <param name="passedValue">What do I need to pass to add 1 to big value</param>
	/// <param name="smallValue">The small value to pass the passedValue</param>
	/// <param name="bigValue">The bigger value to add 1 to if small value has passed</param>
	private void CheckPassedNumber(int passedValue, ref int smallValue, ref int bigValue) {
		if(smallValue >= passedValue) {
			smallValue = 0;
			bigValue++;
		}
	}

	/// <summary>
	/// Starts counting.
	/// </summary>
	public void Start() {
		timerObject.Start();
	}

	/// <summary>
	/// Stops counting.
	/// </summary>
	public void Stop() {
		timerObject.Stop();
	}

	/// <summary>
	/// Clean up the timer object.
	/// </summary>
	public void Dispose() {
		timerObject.Dispose();
	}

	/// <summary>
	/// Get the time as a string formatted like HH:MM:SS
	/// </summary>
	/// <returns></returns>
	public override string ToString() {
		return hours.ToString("D2") + ":" + minutes.ToString("D2") + ":" + seconds.ToString("D2");
	}
}
