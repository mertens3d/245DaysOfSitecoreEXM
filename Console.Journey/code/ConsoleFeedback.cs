using Shared.Interfaces;
using System;
using System.Collections.Generic;

namespace Console.Journey
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

    public void ReportErrors(List<string> errors)
    {
      if (errors != null)
      {
        var previousColor = System.Console.ForegroundColor;
        System.Console.ForegroundColor = ConsoleColor.Red;
        foreach (var error in errors)
        {
          System.Console.WriteLine("*error* \t" + error);
        }

        System.Console.ForegroundColor = previousColor;
      }
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