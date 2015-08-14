using System;

namespace MovieStreaming
{
    public static class ColorConsole
    {
        public static void WriteLineGreen(string message)
        {
            var beforeColor = Console.ForegroundColor;
            WriteMessageWithColor(message, beforeColor, ConsoleColor.Green);
            Console.ForegroundColor = beforeColor;
        }

        public static void WriteLineYellow(string message)
        {
            var beforeColor = Console.ForegroundColor;
            WriteMessageWithColor(message, beforeColor, ConsoleColor.Yellow);
        }

        public static void WriteLineRed(string message)
        {
            var beforeColor = Console.ForegroundColor;
            WriteMessageWithColor(message, beforeColor, ConsoleColor.Red);
        }

        public static void WriteLineCyan(string message)
        {
            var beforeColor = Console.ForegroundColor;
            WriteMessageWithColor(message, beforeColor, ConsoleColor.Cyan);
        }
        
        private static void WriteMessageWithColor(string message, ConsoleColor beforeColor, ConsoleColor newColor)
        {
            Console.ForegroundColor = newColor;

            Console.WriteLine(message);

            Console.ForegroundColor = beforeColor;
        }

        public static void WriteLineGray(string message)
        {
            var beforeColor = Console.ForegroundColor;
            WriteMessageWithColor(message, beforeColor, ConsoleColor.Gray);
        }

        public static void WriteLineWhite(string message)
        {
            var beforeColor = Console.ForegroundColor;
            WriteMessageWithColor(message, beforeColor, ConsoleColor.White);
        }
    }
}