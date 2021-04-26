using Sitecore.Data;
using Sitecore.Data.Items;
using System;

namespace LearnEXM.Foundation.LearnEXMRoot
{
  public class DateTimeFieldProxy : _baseFieldProxy
  {
    public DateTime? DateTime
    {
      get
      {
        DateTime? toReturn = null;

        if (IsValidItem)
        {
          toReturn = Sitecore.DateUtil.IsoDateToDateTime(Item.Fields[FieldId].Value);
        }

        return toReturn;
      }
    }

    public DateTimeFieldProxy(Item item, ID fieldName) : base(item, fieldName)
    {
    }
  }
}