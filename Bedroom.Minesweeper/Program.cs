using System;
using System.Runtime.InteropServices;

namespace Bedroom.Minesweeper
{
#if WINDOWS
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if DEBUG
            // When compiling with DEBUG set, open the console
            AllocConsole();
#endif

            using (var game = new Core())
                game.Run();
        }

#if DEBUG
        /// <summary>
        /// Opens the windows console and allows writing to it.
        /// It will not receive any input in visual studio, as stdout is redirected to the output window.
        /// </summary>
        /// <returns>Returns 0 when opening failed.</returns>
        [DllImport("kernel32.dll",
            EntryPoint = "AllocConsole",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern int AllocConsole();
#endif
    }
#endif
}