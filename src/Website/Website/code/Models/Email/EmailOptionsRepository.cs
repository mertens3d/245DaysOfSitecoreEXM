using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.EmailCampaign.SampleNewsletter;
using Sitecore.EmailCampaign.SampleNewsletter.Extensions;
using Sitecore.EmailCampaign.SampleNewsletter.Models;
using Sitecore.EmailCampaign.SampleNewsletter.Services;
using System;
using System.Linq;

namespace LearnEXMProject.Models.Email
{
  public class EmailOptionsRepository
  {
    private const int DefaultMaxWidth = 800;
    private readonly FindNewsletterRootService _findNewsletterRootService;

    public EmailOptionsRepository() : this(new FindNewsletterRootService())
    {
    }

    public EmailOptionsRepository(FindNewsletterRootService findNewsletterRootService)
    {
      Assert.ArgumentNotNull(findNewsletterRootService, "findNewsletterRootService");
      _findNewsletterRootService = findNewsletterRootService;
    }

    public NewsletterOptions Get(Item contextItem)
    {
      Assert.ArgumentNotNull(contextItem, "contextItem");
      var newsletterRoot = _findNewsletterRootService.FindNewsletterRoot(contextItem);
      var obj = newsletterRoot.Children.FirstOrDefault(c => c.IsDerived(Templates.NewsletterOptions.ID));

      if (obj == null)
      {
        throw new ArgumentException($"Cannot find EmailOptionsRepository below '{newsletterRoot.Paths.FullPath}'");
      }


      if(! int.TryParse(obj[Templates.NewsletterOptions.Fields.MaxWidth], out var result))
      {
        result = DefaultMaxWidth;
      }

      return new NewsletterOptions
      {
        ContentFontSize = obj[Templates.NewsletterOptions.Fields.ContentFontSize],
        FontFamily = obj[Templates.NewsletterOptions.Fields.FontFamily],
        HeadingFontSize = obj[Templates.NewsletterOptions.Fields.HeadingFontSize],
        MaxWidth = result,
        BeforeBodyHtml = obj[Templates.NewsletterOptions.Fields.BeforeBodyHtml],
        AfterBodyHtml = obj[Templates.NewsletterOptions.Fields.AfterBodyHtml]
      };
    }
  }
}