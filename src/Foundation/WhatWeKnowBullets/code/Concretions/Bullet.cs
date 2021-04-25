using LearnEXM.Foundation.WhatWeKnowBullets.Interfaces;
using System.Collections.Generic;

namespace LearnEXM.Foundation.WhatWeKnowBullets.Concretions
{
  public class Bullet : IBullet
  {

    public Bullet(string title)
    {
      Title = title;
    }

    public Bullet(string title, string value)
    {
      Title = title;
      Value = value;
    }

    public string Title { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public List<IBullet> ChildBullets { get; set; } = new List<IBullet>();
  }
}