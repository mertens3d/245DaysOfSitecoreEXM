using LearnEXM.Foundation.WhatWeKnowBullets.Concretions;
using LearnEXM.Foundation.WhatWeKnowBullets.Interfaces;
using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;

namespace LearnEXM.Foundation.WhatWeKnowBullets.Helpers
{
  public class EmailAddressListBulletFactory : IFacetBulletFactory
  {
    public string AssociatedDefaultFacetKey { get; set; } = EmailAddressList.DefaultFacetKey;

    public IBullet GetBullet(Facet facet)
    {
      var toReturn = new Bullet("Email Address List");

      var emailAddressList = facet as EmailAddressList;

      if (emailAddressList != null)
      {
        var preferredEmail = emailAddressList.PreferredEmail;
        if (preferredEmail != null)
        {
          toReturn.ChildBullets.Add(new Bullet("Preferred Email", preferredEmail.SmtpAddress));
        }
      }
      return toReturn;
    }
  }
}