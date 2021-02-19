using Shared.Interfaces;
using System;
using System.Collections.Generic;

namespace LearnEXMProject.Controllers
{
  public class WebpageFeedback : IFeedback
  {
    public List<string> MessageQueue { get; set; } = new List<string>();

    public void ForegroundColor(ConsoleColor white)
    {
      // do nothing
    }

    public void ReadKey()
    {
      // do nothing
    }

    public string ReadLine()
    {
      return string.Empty;
    }

    public void ReportErrors(List<string> errors)
    {
      throw new NotImplementedException();
    }

    public void WindowWidth(int v)
    {
      // do nothing
    }

    public void WriteLine(string message)
    {
      this.MessageQueue.Add(message);
    }
  }
}