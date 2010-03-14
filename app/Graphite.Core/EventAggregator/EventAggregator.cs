using System;
using System.Collections.Generic;

namespace Graphite.Core.EventAggregator{
  public class EventAggregator{

    public void SubscribeTo<T>(ISubscriber<T> subscriber) where T : IEvent
    {
      
    }

    public IEnumerable<ISubscriber<T>> SubscribersOf<T>() where T : IEvent {
      return null;
    }
  }
}