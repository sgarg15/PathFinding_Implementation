﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heap<T> where T : IHeapItem<T> {
  T[] items;
  int currentItemCount;

  public Heap(int maxHeapSize){
    items = new T[maxHeapSize];
  }

  public void Add(T item){
    item.HeapIndex = currentItemCount;
    items[currentItemCount] = item;
    SortUp(item);
    currentItemCount++;
  }

  public T RemoveFirst(){
    T firstItem = items[0];
    currentItemCount--;
    itmes[0] = items[currentItemCount];
    items[0].HeapIndex = 0;
    SortDown(items[0]);
    return firstItem;
  }

  void SortDown(T item){
    while(true){
      int childIndexLeft = item.HeapIndex * 2 + 1;
    }
  }

  void SortUp(T item){
    int parentIndex = (item.HeapIndex-1)/2;

    while (true){
      T parentItem = items[parentIndex];
      if(item.CompareTo(parentItem) > 0) {
        Swap(item, parentItem);
      } else {
        break;
      }
      parentIndex = (item.HeapIndex - 1) / 2;
    }
  }

  void Swap(T itemA, T itemB){
    items[itemA.HeapIndex] = itemB;
    items[itemB.HeapIndex] = itemA;
    int itemAIndex = itemA.HeapIndex;
    itemA.HeapIndex = itemB.HeapIndex;
    itemB.HeapIndex = itemAIndex;
  }
}

public interface IheapItem<T> : IComparable<T> {
  int HeapIndex {
    get;
    set;
  }
}
