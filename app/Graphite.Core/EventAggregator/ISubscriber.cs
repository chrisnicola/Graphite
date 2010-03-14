using System;

namespace Graphite.Core.EventAggregator{
  public interface ISubscriber<T> where T : IEvent {
    Action<T> OnEvent { get; }
  }
}