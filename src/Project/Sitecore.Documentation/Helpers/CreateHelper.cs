using Sitecore.XConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Documentation
{
  public class CreateHelper
  {
    public Contact CreateNewContact(ContactModel newContact)
    {
      // Identifier for a 'known' contact
      var identifier = new ContactIdentifier[]
      {
            new ContactIdentifier(Const.XConnect.ContactIdentifiers.Sources.Twitter , newContact.ContactIdentifier, ContactIdentifierType.Known)
      };

      // Print out identifier that is going to be used
      Console.WriteLine("Identifier: " + identifier[0].Identifier);

      // Create a new contact with the identifier
      Contact knownContact = new Contact(identifier);

      return knownContact;
    }
  }
}
