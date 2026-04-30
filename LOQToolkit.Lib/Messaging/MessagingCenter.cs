using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using LOQToolkit.Lib.Messaging.Messages;

namespace LOQToolkit.Lib.Messaging;

public static class MessagingCenter
{
    private static readonly ConcurrentDictionary<object, List<Subscription>> _subscriptions = new();

    public static void Publish<T>(T data) where T : IMessage
    {
        Cleanup();
        foreach (var subs in _subscriptions.Values.ToArray())
        {
            foreach (var sub in subs.ToArray())
            {
                if (sub.MessageType == typeof(T))
                    sub.Invoke(data);
            }
        }
    }

    public static void Subscribe<T>(object subscriber, Action<T> handler) where T : IMessage
    {
        Cleanup();
        var subs = _subscriptions.GetOrAdd(subscriber, _ => new List<Subscription>());
        subs.Add(new Subscription(typeof(T), o => handler((T)o!)));
    }

    public static void Subscribe<T>(object subscriber, Action handler) where T : IMessage
    {
        Subscribe<T>(subscriber, _ => handler());
    }

    private static void Cleanup()
    {
        var dead = _subscriptions.Keys.Where(k => k is null).ToArray();
        foreach (var key in dead)
            _subscriptions.TryRemove(key, out _);
    }

    private class Subscription(Type messageType, Action<object> action)
    {
        public Type MessageType { get; } = messageType;
        public void Invoke(object data) => action(data);
    }
}
