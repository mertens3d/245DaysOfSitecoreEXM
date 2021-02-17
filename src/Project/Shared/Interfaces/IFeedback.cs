using System;

namespace Shared.Interfaces
{
  public interface IFeedback
  {
    void ForegroundColor(ConsoleColor white);

    void ReadKey();

    string ReadLine();

    void WindowWidth(int v);

    void WriteLine(string message);
  }
}