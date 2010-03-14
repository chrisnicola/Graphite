using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphite.Core.EventAggregator{
  public class EventAggregator{
    private IList<object> _subscribers = new List<object>();

    public void SubscribeTo<T>(ISubscriber<T> subscriber) where T : IEvent {
      _subscribers.Add(subscriber);
    }

    public IEnumerable<ISubscriber<T>> SubscribersOf<T>() where T : IEvent {
      return _subscribers.OfType<ISubscriber<T>>();
    }

    public void UnsubscribeFrom<T>(ISubscriber<T> subscriber) where T : IEvent { _subscribers.Remove(subscriber); }

    public void PublishAndWait<T>(T testEvent) where T : IEvent {
      foreach (var subscriber in _subscribers.OfType<ISubscriber<T>>()) 
        subscriber.OnEvent.Invoke(testEvent);
    }

    public void Publish<T>(T testEvent) where T : IEvent {
      foreach (var subscriber in _subscribers.OfType<ISubscriber<T>>()) subscriber.OnEvent.BeginInvoke(testEvent, null, null);
    }
  }
}