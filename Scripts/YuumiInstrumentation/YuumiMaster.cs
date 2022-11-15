using Gma.System.MouseKeyHook;
using MouseAndKeyboard.InputSimulation;
using MouseAndKeyboard.Network;
using MouseAndKeyboard.Util;

namespace YuumiInstrumentation;

public class YuumiMaster : IMKInput, IDisposable
{
	private UDPSocketShipper socket;

	public UDPSocket Socket => socket;

	private readonly IKeyboardMouseEvents inputEvents;
	private readonly YuumiPacketWrite mkPacket;

	private bool enabled = false;

	public YuumiMaster() : base()
	{
		inputEvents = Hook.GlobalEvents();
		socket = new UDPSocketShipper();
		mkPacket = new YuumiPacketWrite();
	}
	public void Stop() => Dispose();

	#region Events

	public bool Enabled
	{
		get => enabled;
		set {
			if (value != enabled)
			{
				enabled = value;
				if (enabled)
					Subscribe();
				else
					Unsubscribe();
			}
		}
	}

	public void Dispose()
	{
		Enabled = false;
		socket.Stop();
		GC.SuppressFinalize(this);
	}

	private void Subscribe()
	{
		inputEvents.MouseMove += OnMouseMove;
		inputEvents.MouseDown += OnMouseDown;
		inputEvents.MouseUp += OnMouseUp;
		inputEvents.MouseWheel += OnMouseScroll;

		inputEvents.KeyDown += OnKeyDown;
		inputEvents.KeyUp += OnKeyUp;
	}

	private void Unsubscribe()
	{
		inputEvents.MouseMove -= OnMouseMove;
		inputEvents.MouseDown -= OnMouseDown;
		inputEvents.MouseWheel -= OnMouseScroll;

		inputEvents.KeyDown -= OnKeyDown;
		inputEvents.KeyUp -= OnKeyUp;
	}

	#endregion

	#region Network Util

	private void SendPacket()
	{
		socket.Send(mkPacket.GetPacket);
		mkPacket.Reset();
	}

	private void WriteKey(Keys key, PressedState pressedState)
	{
		mkPacket.WriteKey(key, pressedState);
		SendPacket();
	}

	#endregion

	#region MOUSE EVENTS
	private void OnMouseMove(object sender, MouseEventArgs e)
	{
		if (!enabled)
			return;
		LoggerEvents.WriteLine("SEND: MMove" + e.X + " " + e.Y);
		mkPacket.WriteMouseMove(e.X, e.Y);
		SendPacket();
	}

	private void OnMouseScroll(object sender, MouseEventArgs e)
	{
		if (!enabled)
			return;
		LoggerEvents.WriteLine("SEND: MScroll" + e.Delta);
		mkPacket.WriteMouseScroll(e.Delta);
		SendPacket();
	}

	private void OnMouseDown(object sender, MouseEventArgs e) => UnifyMouse(e, PressedState.Down);

	private void OnMouseUp(object sender, MouseEventArgs e) => UnifyMouse(e, PressedState.Up);

	#endregion

	#region KEYS EVENTS
	private void OnKeyDown(object sender, KeyEventArgs e) => UnifyKey(e, PressedState.Down);

	private void OnKeyUp(object sender, KeyEventArgs e) => UnifyKey(e, PressedState.Up);
	#endregion

	#region Unify

	private void UnifyMouse(MouseEventArgs e, PressedState pressed)
	{
		if (!enabled)
			return;

		if (mouseToKey.TryGetValue(e.Button, out var key))
		{
			LoggerEvents.WriteLine($"SEND Mouse: {pressed,-5}: {e.Button} => {key}");
			WriteKey(key, pressed);
		}
		else
		{
			LoggerEvents.WriteLine($"SEND Mouse: {pressed,-5}: {e.Button}");
			mkPacket.WriteMouseClick(e.Button, pressed);
			SendPacket();
		}
	}

	private void UnifyKey(KeyEventArgs e, PressedState pressed)
	{
		if (!enabled)
			return;

		if (e.Shift && mirrorWhenShiftKeys.Contains(e.KeyCode))
		{
			LoggerEvents.WriteLine($"SEND Key: {pressed,-5}: {e.KeyCode} | Shift");
			WriteKey(e.KeyCode, pressed);
		}
		else if (skillUpKeys.TryGetValue(e.KeyCode, out var key))
		{
			LoggerEvents.WriteLine($"SEND Key: {pressed,-5}: {e.KeyCode} => {key} | Control");
			mkPacket.WriteKeyModifier(key, Keys.LControlKey, pressed);
			SendPacket();
		}
	}

	#endregion


	#region Buttons Data

	//Q W E R - Skills 4
	//D F - Spells 2
	//Space D1 D2 D4 - Itens 1..4
	//Y P B - Util 2

	private readonly static Dictionary<MouseButtons, Keys> mouseToKey = new() {
		{ MouseButtons.XButton1, Keys.F },
		{ MouseButtons.XButton2, Keys.D },
	};

	private readonly static Dictionary<Keys, Keys> skillUpKeys = new() {
		{ Keys.D8, Keys.Q },
		{ Keys.D9, Keys.W },
		{ Keys.D0, Keys.E },
		{ Keys.OemMinus, Keys.R },
	};

	private readonly static HashSet<Keys> mirrorWhenShiftKeys = new() {
		Keys.F1, Keys.F2, Keys.F3, Keys.F4, Keys.F5,

		Keys.Q,
		Keys.W,
		Keys.E,
		Keys.R,
		Keys.Space,
		Keys.D1,
		Keys.D2,
		Keys.D3,
		Keys.D4,
		Keys.D,
		Keys.F,

		Keys.Y,
		Keys.B,

		Keys.Escape,
		Keys.P,
	};

	#endregion
}