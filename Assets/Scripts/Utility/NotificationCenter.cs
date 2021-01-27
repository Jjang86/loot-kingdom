using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

public static class NotificationCenter {
    public static bool KeepActionValue { get; set; }
    private static Collection<Tuple<object, string, Action>> Subscribers { get; set; }
    private static Collection<Tuple<object, string, Action<object>>> SubscribersWithData { get; set; }
    private static List<Tuple<string, object>> KeepActionData { get; set; }
    private static bool EnableLogs = true;

    static NotificationCenter() {
        Subscribers = new Collection<Tuple<object, string, Action>>();
        SubscribersWithData = new Collection<Tuple<object, string, Action<object>>>();
        KeepActionData = new List<Tuple<string, object>>();
    }

    public static void Subscribe(object receiver, string key, Action action) {
        if (key == null || receiver == null) {
            throw new ArgumentNullException(nameof(key), "KEY and RECEIVER argument should have value");
        }

        var listOfSubs = Subscribers?.Where(k => k.Item1 == receiver && k.Item2 == key).FirstOrDefault();
        if (listOfSubs != null) {
            throw new Exception("reciever already subscribed");
        }

        Subscribers?.Add(new Tuple<object, string, Action>(receiver, key, action));
    }

    public static void Subscribe(object receiver, string key, Action<object> action) {
        if (key == null) {
            throw new ArgumentNullException(nameof(key), "KEY argument should have value");
        }

        var listOfSubs = Subscribers?.Where(k => k.Item1 == receiver && k.Item2 == key).FirstOrDefault();
        if (listOfSubs != null) {
            throw new Exception("reciever already subscribed");
        }

        SubscribersWithData?.Add(new Tuple<object, string, Action<object>>(receiver, key, action));
    }

    public static void Unsubscribe(object receiver, string key) {
        if (key == null || receiver == null) {
            throw new ArgumentNullException(nameof(key), "KEY and RECEIVER argument should have value");
        }

        var listOfSubs = Subscribers?.Where(k => k.Item1 == receiver && k.Item2 == key).FirstOrDefault();
        if (listOfSubs != null) {
            Subscribers?.Remove(listOfSubs);
        }

        var listOfSubsWithOutActionObject = SubscribersWithData?.Where(k => k.Item1 == receiver && k.Item2 == key).SingleOrDefault();
        if (listOfSubsWithOutActionObject != null) {
            SubscribersWithData?.Remove(listOfSubsWithOutActionObject);
        }
    }

    public static void UnsubscribeAll() {
        Subscribers?.Clear();
        SubscribersWithData?.Clear();
    }

    public static void Notify(string key) {
        if (key == null) {
            throw new ArgumentNullException(nameof(key), "KEY argument should have value");
        }

        var subscriptions = Subscribers.Where(s => s.Item2 == key);

        subscriptions.ToList().ForEach((actions) => {
            actions.Item3.Invoke();
        });
    }

    public static void Notify(string key, object data) {
        if (key == null) {
            throw new ArgumentNullException(nameof(key), "KEY argument should have value");
        }

        var subscription = SubscribersWithData.Where(s => s.Item2 == key).ToList();
        if (KeepActionValue) {
            if (subscription.Count == 0) {
                KeepActionData.Add(new Tuple<string, object>(key, data));
            }
        }

        var actions = KeepActionData.Where(k => k.Item1 == key).ToArray();
        if (subscription.Count > 0) {
            subscription.ToList().ForEach((Tuple<object, string, Action<object>> action) => {
                if (actions.Length > 0) {
                    var datas = actions.Select(o => o.Item2).ToList();
                    if (datas.Count > 0) {
                        action.Item3.Invoke(datas);
                    }
                }
                action.Item3.Invoke(data);
            });
        }
        KeepActionData.Remove(actions.FirstOrDefault());
    }
}
