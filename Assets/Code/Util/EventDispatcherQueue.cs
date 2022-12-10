// using System;
// using Code.Common;
// using UnityEngine;
//
// namespace Code.Util
// {
//     public class EventDispatcherQueue : MonoBehaviour, IEventDispatcher
//     {
//         public void Subscribe<T>(Action<T> callback) where T : struct
//         {
//             throw new NotImplementedException();
//         }
//
//         public void Unsubscribe<T>(Action<T> callback) where T : struct
//         {
//             throw new NotImplementedException();
//         }
//
//         // Dispatch here will enqueue the event
//         public void Dispatch<T>(T signal) where T : struct
//         {
//             throw new NotImplementedException();
//         }
//     }
// }