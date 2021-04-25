using LearnEXM.Foundation.WhatWeKnowBullets.Concretions;
using LearnEXM.Foundation.WhatWeKnowBullets.Interfaces;
using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;

namespace LearnEXM.Foundation.WhatWeKnowBullets.Helpers
{
  public class PersonalInformationBulletFactory : IFacetBulletFactory
  {
    public string AssociatedDefaultFacetKey { get; set; } = PersonalInformation.DefaultFacetKey;

    public IBullet GetBullet(Facet facet)
    {
      var toReturn = new Bullet("Personal Information Details");
      var personalInformationDetails = facet as PersonalInformation;
      if (personalInformationDetails != null)
      {
        toReturn.ChildBullets.Add(new Bullet("Name", personalInformationDetails.FirstName + " " + personalInformationDetails.LastName));
        toReturn.ChildBullets.Add(new Bullet("Birthdate", personalInformationDetails.Birthdate.ToString()));
        toReturn.ChildBullets.Add(new Bullet("Gender", personalInformationDetails.Gender));
      }
      return toReturn;
    }
  }
}