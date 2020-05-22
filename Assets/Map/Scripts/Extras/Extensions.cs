using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class StaticExtensions
{
  public static float ToFloat(this Queue queue)
  {
    float number = 0;
    foreach (float f in queue) number += f;
    return number;
  }
}

public class Queue : Queue<float>
{
  public static Queue operator *(Queue queue, float multiplier)
  {
    Queue newQueue = new Queue();
    foreach (float f in queue) newQueue.Enqueue(f * multiplier);
    return newQueue;
  }

  public static Queue operator +(Queue q1, Queue q2)
  {
    Queue newQueue = new Queue();
    int overflow = q1.Count - q2.Count;

    if (overflow > 0) for (int i = 0; i < overflow; i++)
        newQueue.Enqueue(q1.Dequeue());
    else for (int i = 0; i < -overflow; i++)
        newQueue.Enqueue(q2.Dequeue());

    for (int i = 0; i < q1.Count; i++)
      newQueue.Enqueue(q1.Dequeue() + q2.Dequeue());

    return newQueue;
  }

  public static Queue operator -(Queue q1, Queue q2)
  {
    Queue newQueue = new Queue();
    int overflow = q1.Count - q2.Count;

    if (overflow > 0) for (int i = 0; i < overflow; i++)
        newQueue.Enqueue(q1.Dequeue());
    else for (int i = 0; i < -overflow; i++)
        newQueue.Enqueue(q2.Dequeue());

    for (int i = 0; i < q1.Count; i++)
    {
      float value = q1.Dequeue() - q2.Dequeue();
      if (value < 0)
      {
        newQueue.Enqueue(0);
        throw new Exception("Queue almost get a negative value");
      }
      newQueue.Enqueue(value);
    }

    return newQueue;
  }
}

