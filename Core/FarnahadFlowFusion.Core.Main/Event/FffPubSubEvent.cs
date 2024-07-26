using Prism.Events;

namespace FarnahadFlowFusion.Core.Main.Event;

public class FffPubSubEvent : PubSubEvent
{
}

public class FffPubSubEvent<T> : PubSubEvent<T>
{
}