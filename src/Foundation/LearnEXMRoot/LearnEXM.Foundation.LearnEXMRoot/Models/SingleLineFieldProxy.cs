using Sitecore.Data;
using Sitecore.Data.Items;

namespace LearnEXM.Foundation.LearnEXMRoot
{
  public class NumberFieldProxy : _baseFieldProxy
  {
    public NumberFieldProxy(Item item, ID fieldName) : base(item, fieldName)
    {
    }

    public decimal? DecimalValue
    {
      get
      {
        decimal? toReturn = null;

        if (IsValidItem)
        {
          var success = decimal.TryParse(Item.Fields[FieldId].Value, out decimal parsedValue);
          if (success)
          {
            toReturn = parsedValue;
          }
        }

        return toReturn;
      }
    }
  }

  public class SingleLineFieldProxy : _baseFieldProxy
  {
    public SingleLineFieldProxy(Item item, ID fieldName) : base(item, fieldName)
    {
    }

    public string Value
    {
      get
      {
        return Item != null ? Item[FieldId] : null;
      }
    }
  }
}