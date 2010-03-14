using System;
using System.Threading;
using Graphite.Core.EventAggregator;
using NUnit.Framework;
using Rhino.Mocks;

namespace Tests.Graphite.Core{
  public class EventAggregatorTestBase : ISubscriber<TestEvent> {
    protected readonly EventAggregator Ea;
    protected readonly TestEvent TestEvent = new TestEvent { Message = "This is a test" };
    protected bool ActionWasCalled;
    protected TestEvent ActionParameter;

    protected EventAggregatorTestBase() {
      Ea = new EventAggregator();
      Ea.SubscribeTo(this); 
    }

    protected virtual void TestAction(TestEvent obj) {
      ActionWasCalled = true;
      ActionParameter = obj;
    }

    public virtual Action<TestEvent> OnEvent { get { return TestAction; } }
  }

  [TestFixture]
  public class WhenSubscriberSubscribesToEvent : EventAggregatorTestBase {   
    [Test] public void SubscriberIsSubscribedToEvent() { Assert.That(Ea.SubscribersOf<TestEvent>(), Has.Member(this)); }
  }

  [TestFixture]
  public class WhenSubscriberIsUnsubscribedFromEvent : EventAggregatorTestBase {
    [SetUp] public void SetUp() { Ea.UnsubscribeFrom<TestEvent>(this); }
    [Test] public void SubscriberIsNoLongerSubscribedToEvent() { Assert.That(Ea.SubscribersOf<TestEvent>(), Has.No.Member(this)); }
  }

  [TestFixture]
  public class WhenEventIsPublishedSyncronously : EventAggregatorTestBase{
    [SetUp]
    public void SetUp() { Ea.PublishAndWait(TestEvent); }

    [Test]
    public void SubscriberOnEventMethodIsCalled() { Assert.That(ActionWasCalled, Is.True); }

    [Test]
    public void SubscriberOnEventMethodIsCalledWithEventParameter() { Assert.That(ActionParameter, Is.EqualTo(TestEvent));}
  }

  [TestFixture]
  public class WhenEventIsPublishedAsyncronously : EventAggregatorTestBase, ISubscriber<TestEvent>{
    private readonly object _notify = new object();

    [SetUp]
    public void SetUp() {
      Ea.SubscribeTo(this);
      Ea.Publish(TestEvent);
    }

    [Test]
    public void SubscribersOnEventMethodIsCalledWithin100Ms() {
      lock(_notify) Monitor.Wait(_notify, 100);
      Assert.That(ActionWasCalled, Is.True);
    }

    protected override void TestAction(TestEvent obj) {
      Thread.Sleep(50);
      base.TestAction(obj);
      lock(_notify) Monitor.Pulse(_notify);
    }
  }

  public class TestEvent : IEvent {
    public string Message { get; set; }
  }
}