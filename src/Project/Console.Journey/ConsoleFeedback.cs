using Shared.Interfaces;
using System;

namespace Console.Journey.Interactions
{
  public class ConsoleFeedback : IFeedback
  {
    public void ForegroundColor(ConsoleColor consoleColor)
    {
      System.Console.ForegroundColor = consoleColor;
    }

    public void ReadKey()
    {
      System.Console.ReadKey();
    }

    public string ReadLine()
    {
      return System.Console.ReadLine();
    }

    public void WindowWidth(int v)
    {
      System.Console.WindowWidth = v;
    }

    public void WriteLine(string message)
    {
      System.Console.WriteLine(message);
    }
  }
}