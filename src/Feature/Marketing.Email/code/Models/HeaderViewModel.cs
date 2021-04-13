using Marketing.Email.Models;

namespace Feature.Marketing.Email.Models
{
  public class HeaderViewModel : _baseMarketingEmailViewModel
  {
    public LinkData LinkData { get; set; }
    public string FormattedDate { get; set; }
  }
}
