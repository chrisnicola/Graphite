using Graphite.Core.EventAggregator;
using NUnit.Framework;
using Rhino.Mocks;

namespace Tests.Graphite.Core{
  [TestFixture]
  public class WhenSubscriberSubscribesToEvent{
    private EventAggregator ea;

    [SetUp]
    public void SetUp() {
      ea = new EventAggregator();
      var subscriber = MockRepository.GenerateStub<ISubscriber<TestEvent>>();
      ea.SubscribeTo<TestEvent>();
    }

    [Test]
    public void CanConfirmSubscription() {
      Assert.That(ea.Subscribers)
    }
  }

  public class TestEvent : IEvent {
    public string Message { get; set; }
  }
}