using System.Collections.Generic;

namespace LearnEXM.Foundation.WhatWeKnowBullets.Interfaces
{
  public interface IBullet
  {
    List<IBullet> ChildBullets { get; set; }
    string Value { get; set; }
    string Title { get; set; }
  }
}