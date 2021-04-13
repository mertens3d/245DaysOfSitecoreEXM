using Shared.Interfaces;
using System;

namespace Console.Journey.Steps
{
  public abstract class _journeyStepBase
  {
    protected IFeedback Feedback;
    protected string Identifier;

    protected _journeyStepBase(IFeedback feedback, string identifier)
    {
      Feedback = feedback;
      Identifier = identifier;
    }


    protected void DrawPostInteractionMessage(string message)
    {
      DrawPostInteractionMessage(new string[] { message });
    }

    protected void DrawPostInteractionMessage(string[] messages)
    {
      foreach (var message in messages)
      {
        Feedback.WriteLine(message);
      }

      Feedback.WriteLine("Press any key to continue");
      Feedback.ReadKey();
    }
    protected void DrawStepTitle(string[] arr)
    {
      System.Console.WindowWidth = 160;
      foreach (string line in arr)
      {
        System.Console.WriteLine(line);
      }
      System.Console.WriteLine(); // Extra space


    }
    protected void DrawTriggerMessage(string message)
    {
      Feedback.ForegroundColor(ConsoleColor.Cyan);
      Feedback.WriteLine(message);
      Feedback.ForegroundColor(ConsoleColor.White);
    }
  }
}