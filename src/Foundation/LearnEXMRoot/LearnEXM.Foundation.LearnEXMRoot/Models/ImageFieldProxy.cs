using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace LearnEXM.Foundation.LearnEXMRoot
{
  public class ImageFieldProxy : _baseFieldProxy
  {
    public ImageFieldProxy(Item item, ID fieldName) : base(item, fieldName)
    {
    }

    public string ImageURL
    {
      get
      {
        var toReturn = string.Empty;
        if (ImageField != null)
        {
          MediaItem image = new MediaItem(ImageField.MediaItem);
          toReturn = Sitecore.StringUtil.EnsurePrefix('/', Sitecore.Resources.Media.MediaManager.GetMediaUrl(image));
        }

        return toReturn;
      }
    }

    private ImageField ImageField
    {
      get
      {
        return Item != null ? Item.Fields[FieldId] : null;
      }
    }
  }
}