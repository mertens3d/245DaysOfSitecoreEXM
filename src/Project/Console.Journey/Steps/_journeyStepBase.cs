using Shared.Interfaces;
using System;

namespace Console.Journey.Interactions
{
  public abstract class _journeyStepBase
  {
    protected IFeedback Feedback;
    protected string Identifier;

    protected _journeyStepBase(IFeedback feedback, string identifier)
    {
      this.Feedback = feedback;
      this.Identifier = identifier;
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
    protected void DrawTriggerMessage(string message)
    {
      Feedback.ForegroundColor(ConsoleColor.Cyan);
      Feedback.WriteLine(message);
      Feedback.ForegroundColor(ConsoleColor.White);
    }
  }
}