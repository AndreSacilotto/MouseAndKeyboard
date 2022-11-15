using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MouseAndKeyboard.Util;

namespace MouseAndKeyboard.InputSimulation;

public static class Mouse
{
	#region Get Mouse Position

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool GetCursorPos(out MousePoint lpMousePoint);

	[StructLayout(LayoutKind.Sequential)]
	private struct MousePoint
	{
		internal int x;
		internal int y;
	}
	public static void GetCursorPosition(out int x, out int y)
	{
		GetCursorPos(out var point);
		x = point.x;
		y = point.y;
	}

	#endregion

	private static InputStruct MouseInput => new InputStruct(InputType.Mouse);

	#region Movement
	private const int ABSOLUTE_MAX = ushort.MaxValue + 1;

	public static int PositionToAbsolute(int coord, int widthOrHeight)
	{
		return coord * ABSOLUTE_MAX / widthOrHeight + (coord < 0 ? -1 : 1);
	}

	public static string PositionToAbsolutePrint(int x, int y)
	{
		var screen = ScreenUtil.GetPrimaryScreenSize;
		return $"{PositionToAbsolute(x, screen.width)} : {PositionToAbsolute(y, screen.height)}";
	}

	/// <summary>Move based on current mouse position</summary>
	public static InputStruct MoveRelativeInput(int x, int y)
	{
		var input = MouseInput;
		input.union.mi.dx = x;
		input.union.mi.dy = y;
		input.union.mi.dwFlags = MouseEventF.Move;
		return input;
	}

	private static InputStruct MoveAbsoluteInput(int x, int y)
	{
		var screen = ScreenUtil.GetPrimaryScreenSize;
		var input = MouseInput;
		input.union.mi.dx = x * ABSOLUTE_MAX / screen.width + 1;
		input.union.mi.dy = y * ABSOLUTE_MAX / screen.height + 1;
		input.union.mi.dwFlags = MouseEventF.Move | MouseEventF.Absolute;
		//MouseEventF.VirtualDesk - Dont worth the trouble
		return input;
	}

	public static void MoveRelative(int x, int y) => InputSender.SendInput(MoveRelativeInput(x, y));
	public static void MoveAbsolute(int x, int y) => InputSender.SendInput(MoveAbsoluteInput(x, y));
	public static void MoveAbsolute(float x, float y) => InputSender.SendInput(MoveAbsoluteInput((int)x, (int)y));

	#endregion

	#region Gradual Movement

	public static Task GradualMoveLinear(int x1, int y1, int steps = 32, int delay = 10)
	{
		GetCursorPosition(out var x0, out var y0);
		return Task.Run(async () => {
			float N = steps;
			for (int i = 0; i < steps; i++)
			{
				var v = i / N;
				var x = x1 * v + x0 * (1f - v);
				var y = y1 * v + y0 * (1f - v);
				MoveAbsolute(x, y);
				await Task.Delay(delay);
			}
		});
	}

	public static Task GradualMoveLerp(int x1, int y1, int steps = 32, int delay = 10)
	{
		GetCursorPosition(out var x0, out var y0);
		return Task.Run(async () => {
			float N = steps;
			for (int i = 0; i < steps; i++)
			{
				var v = Interpolation.Lerp(0, 1, i / N);
				var x = x1 * v + x0 * (1f - v);
				var y = y1 * v + y0 * (1f - v);
				MoveAbsolute(x, y);
				await Task.Delay(delay);
			}
		});
	}

	public static Task GradualMoveSmoothStep(int x1, int y1, int steps = 32, int delay = 10)
	{
		GetCursorPosition(out var x0, out var y0);
		return Task.Run(async () => {
			float N = steps;
			for (int i = 0; i < steps; i++)
			{
				var v = Interpolation.SmoothStep(0, 1, i / N);
				var x = x1 * v + x0 * (1f - v);
				var y = y1 * v + y0 * (1f - v);
				MoveAbsolute(x, y);
				await Task.Delay(delay);
			}
		});
	}

	public static Task GradualMoveSlerp(int x1, int y1, int steps = 32, int delay = 10)
	{
		GetCursorPosition(out var x0, out var y0);
		return Task.Run(async () => {
			float N = steps;
			for (int i = 0; i < steps; i++)
			{
				var v = Interpolation.Slerp(0, 1, i / N);
				var x = x1 * v + x0 * (1f - v);
				var y = y1 * v + y0 * (1f - v);
				MoveAbsolute(x, y);
				await Task.Delay(delay);
			}
		});
	}

	#endregion

	#region MButton (Click)

	private static InputStruct ClickInput(MouseEventF dwFlags)
	{
		var input = MouseInput;
		input.union.mi.dwFlags = dwFlags;
		return input;
	}
	/// <param name="dwFlags">Need to be XDown or XUp</param>
	private static InputStruct ClickInput(MouseEventF dwFlags, MouseDataXButton mouseData)
	{
		var input = MouseInput;
		input.union.mi.mouseData = (int)mouseData;
		input.union.mi.dwFlags = dwFlags;
		return input;
	}

	internal static void Click(MouseEventF dwFlags) => InputSender.SendInput(ClickInput(dwFlags));
	internal static void Click(MouseEventF dwFlags, int numberOfClicks = 2)
	{
		var inputs = new InputStruct[numberOfClicks];
		var input = ClickInput(dwFlags);
		for (int i = 0; i < numberOfClicks; i++)
			inputs[i] = input;
		InputSender.SendInput(inputs);
	}
	/// <param name="dwFlags">Need to be XDown or XUp</param>
	internal static void Click(MouseEventF dwFlags, MouseDataXButton mouseData) => InputSender.SendInput(ClickInput(dwFlags, mouseData));
	/// <param name="dwFlags">Need to be XDown or XUp</param>
	internal static void Click(MouseEventF dwFlags, MouseDataXButton mouseData, int numberOfClicks = 2)
	{
		var inputs = new InputStruct[numberOfClicks];
		var input = ClickInput(dwFlags, mouseData);
		for (int i = 0; i < numberOfClicks; i++)
			inputs[i] = input;
		InputSender.SendInput(inputs);
	}

	#endregion

	#region Wheel

	private static InputStruct ScrollWheelInput(int wheelDelta = 120)
	{
		var input = MouseInput;
		input.union.mi.dwFlags = MouseEventF.Wheel;
		input.union.mi.mouseData = wheelDelta;
		return input;
	}

	/// <param name="wheelDelta">Scroll quantity. 120 is the Windows default</param>
	public static void ScrollWheel(int wheelDelta = 120) =>
		InputSender.SendInput(ScrollWheelInput(wheelDelta));

	#endregion

	#region Combinations
	public static void DragAndDrop(int endX, int endY)
	{
		Task.Run(async () => {
			Click(MouseEventF.LeftDown);
			await Task.Delay(15);
			MoveAbsolute(endX, endY);
			await Task.Delay(15);
			Click(MouseEventF.LeftUp);
		});
	}

	internal static void MoveAndClick(MouseEventF dwFlags, int x, int y)
	{
		var input = MoveAbsoluteInput(x, y);
		input.union.mi.dwFlags |= dwFlags;
		InputSender.SendInput(input);
	}
	internal static void MoveAndClick(MouseEventF dwFlags, int x, int y, int numberOfClicks = 2)
	{
		var inputs = new InputStruct[numberOfClicks + 1];
		inputs[0] = MoveAbsoluteInput(x, y);
		var input = ClickInput(dwFlags);
		for (int i = numberOfClicks; i > 0; i--)
			inputs[i] = input;
		InputSender.SendInput(inputs);
	}

	#endregion

	#region Click by Enum

	public static void Click(PressedState pressState, MouseButtons mouseButton)
	{
		switch (mouseButton)
		{
			case MouseButtons.Left:
			if (pressState == PressedState.Click)
				Click(MouseEventF.LeftClick);
			else if (pressState == PressedState.Down)
				Click(MouseEventF.LeftDown);
			else if (pressState == PressedState.Up)
				Click(MouseEventF.LeftUp);
			break;
			case MouseButtons.Middle:
			if (pressState == PressedState.Click)
				Click(MouseEventF.MiddleClick);
			else if (pressState == PressedState.Down)
				Click(MouseEventF.MiddleDown);
			else if (pressState == PressedState.Up)
				Click(MouseEventF.MiddleUp);
			break;
			case MouseButtons.Right:
			if (pressState == PressedState.Click)
				Click(MouseEventF.RightClick);
			else if (pressState == PressedState.Down)
				Click(MouseEventF.RightDown);
			else if (pressState == PressedState.Up)
				Click(MouseEventF.RightUp);
			break;
			case MouseButtons.XButton1:
			if (pressState == PressedState.Click)
				Click(MouseEventF.XClick, MouseDataXButton.XButton1);
			else if (pressState == PressedState.Down)
				Click(MouseEventF.XDown, MouseDataXButton.XButton1);
			else if (pressState == PressedState.Up)
				Click(MouseEventF.XUp, MouseDataXButton.XButton1);
			break;
			case MouseButtons.XButton2:
			if (pressState == PressedState.Click)
				Click(MouseEventF.XClick, MouseDataXButton.XButton2);
			else if (pressState == PressedState.Down)
				Click(MouseEventF.XDown, MouseDataXButton.XButton2);
			else if (pressState == PressedState.Up)
				Click(MouseEventF.XUp, MouseDataXButton.XButton2);
			break;
		}
	}

	public static void Click(PressedState pressState, MouseButtons mouseButton, int numberOfClicks = 2)
	{
		switch (mouseButton)
		{
			case MouseButtons.Left:
			if (pressState == PressedState.Click)
				Click(MouseEventF.LeftClick, numberOfClicks);
			else if (pressState == PressedState.Down)
				Click(MouseEventF.LeftDown, numberOfClicks);
			else if (pressState == PressedState.Up)
				Click(MouseEventF.LeftUp, numberOfClicks);
			break;
			case MouseButtons.Middle:
			if (pressState == PressedState.Click)
				Click(MouseEventF.MiddleClick, numberOfClicks);
			else if (pressState == PressedState.Down)
				Click(MouseEventF.MiddleDown, numberOfClicks);
			else if (pressState == PressedState.Up)
				Click(MouseEventF.MiddleUp, numberOfClicks);
			break;
			case MouseButtons.Right:
			if (pressState == PressedState.Click)
				Click(MouseEventF.RightClick, numberOfClicks);
			else if (pressState == PressedState.Down)
				Click(MouseEventF.RightDown, numberOfClicks);
			else if (pressState == PressedState.Up)
				Click(MouseEventF.RightUp, numberOfClicks);
			break;
			case MouseButtons.XButton1:
			if (pressState == PressedState.Click)
				Click(MouseEventF.XClick, MouseDataXButton.XButton1, numberOfClicks);
			else if (pressState == PressedState.Down)
				Click(MouseEventF.XDown, MouseDataXButton.XButton1, numberOfClicks);
			else if (pressState == PressedState.Up)
				Click(MouseEventF.XUp, MouseDataXButton.XButton1, numberOfClicks);
			break;
			case MouseButtons.XButton2:
			if (pressState == PressedState.Click)
				Click(MouseEventF.XClick, MouseDataXButton.XButton2, numberOfClicks);
			else if (pressState == PressedState.Down)
				Click(MouseEventF.XDown, MouseDataXButton.XButton2, numberOfClicks);
			else if (pressState == PressedState.Up)
				Click(MouseEventF.XUp, MouseDataXButton.XButton2, numberOfClicks);
			break;
		}
	}

	#endregion

}