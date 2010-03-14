namespace Graphite.Core.EventAggregator{
  public interface ISubscriber <T> where T : IEvent {
    void OnEvent(T message);
  }
}