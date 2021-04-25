using System.Collections.Generic;

namespace LearnEXM.Feature.WhatWeKnowAboutYou.Interfaces
{
  public interface IBullets { }
  public class Bullets : IBullets
  {
  public  string Title { get; set; }
   public string Value { get; set; }
   public List<IBullets> ChildBullets { get; set; }
  }
}