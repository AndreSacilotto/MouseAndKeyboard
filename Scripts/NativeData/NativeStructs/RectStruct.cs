using System.Runtime.InteropServices;

namespace MouseAndKeyboard.Native;

/// <summary>
/// The RECT structure defines the coordinates of the upper-left and lower-right corners of a rectangle.
/// </summary>
/// <remarks>
/// By convention, the right and bottom edges of the rectangle are normally considered exclusive.
/// In other words, the pixel whose coordinates are ( right, bottom ) lies immediately outside of the the rectangle.
/// For example, when RECT is passed to the FillRect function, the rectangle is filled up to, but not including,
/// the right column and bottom row of pixels. This structure is identical to the RECTL structure.
/// </remarks>
/// <param name="Left">The x-coordinate of the upper-left corner of the rectangle.</param>
/// <param name="Top">The y-coordinate of the upper-left corner of the rectangle.</param>
/// <param name="Right">The x-coordinate of the lower-right corner of the rectangle.</param>
/// <param name="Bottom">The y-coordinate of the lower-right corner of the rectangle.</param>
// https://learn.microsoft.com/en-us/dotnet/api/system.windows.rect
[StructLayout(LayoutKind.Sequential)]
public readonly record struct NativeRect(int Left, int Top, int Right, int Bottom)
{
}
