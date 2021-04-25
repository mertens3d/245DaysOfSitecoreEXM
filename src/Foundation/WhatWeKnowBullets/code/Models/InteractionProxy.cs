using Sitecore.XConnect;
using System;
using System.Collections.Generic;

namespace LearnEXM.Foundation.WhatWeKnowBullets.Models
{
  public class InteractionProxy
  {
    public Guid ChannelId { get; internal set; }
    public string SerializedAsJson { get; set; }
    public string ChannelName { get; internal set; }
    public Interaction RawInteraction { get; internal set; }
    public EventCollection Events { get; internal set; }
    public IEntityReference<DeviceProfile> DeviceProfile { get; internal set; }
    public DateTime StartDateTime { get; internal set; }
    public DateTime EndDateTime { get; internal set; }
    public string InitiatorStr { get; internal set; }
    public Guid? Id { get; internal set; }
    public TimeSpan Duration { get; internal set; }
    public Guid? CampaignId { get; internal set; }
    public List<EventRecordProxy> EventsB { get; internal set; }
  }
}