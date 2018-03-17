using System;
using System.Collections;
using System.Collections.Generic;

namespace SinglyLinkedList
{
    public class SinglyLinkedList<T> : IList<T>
    {
        Node<T> firstNode;
        Node<T> currentNode;
        Node<T> lastNode;

        public int Count
        {
            get; private set;
        }
        public bool IsReadOnly
        {
            get;
        }

        public SinglyLinkedList() { }

        public void Add(T value)
        {
            AddLast(value);
        }
        public void AddFirst(T value)
        {
            Node<T> newNode = new Node<T>(value);

            if (firstNode == null)
                firstNode = lastNode = newNode;
            else
            {
                newNode.Next = firstNode;
                firstNode = newNode;
            }

            Count++;
        }
        public void AddLast(T value)
        {
            Node<T> newNode = new Node<T>(value);

            if (firstNode == null)
                firstNode = lastNode = newNode;
            else
            {
                lastNode.Next = newNode;
                lastNode = newNode;
            }

            Count++;
        }

        public int IndexOf(T value)
        {
            int index = 0;

            currentNode = firstNode;

            while (currentNode != null)
            {
                if (value.Equals(currentNode.Value))
                    return index;

                currentNode = currentNode.Next;
                index++;
            }

            return -1;
        }
        public void Insert(int index, T value)
        {
            if (index < 0 || index > Count)
                throw new ArgumentOutOfRangeException();
            else if (index == 0)
                AddFirst(value);
            else if (index == Count)
                AddLast(value);
            else
            {
                Node<T> newNode = new Node<T>(value);
                int count = 0;

                currentNode = firstNode;

                while (currentNode != null && count != (index - 1))
                {
                    currentNode = currentNode.Next;
                    count++;
                }

                newNode.Next = currentNode.Next;
                currentNode.Next = newNode;

                Count++;
            }
        }

        public bool Contains(T value)
        {
            currentNode = firstNode;

            while (currentNode != null)
            {
                if (value.Equals(currentNode.Value))
                    return true;

                currentNode = currentNode.Next;
            }

            return false;
        }
        public void CopyTo(T[] array, int index)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            if (index < 0 || index > array.Length)
                throw new ArgumentOutOfRangeException("index");

            if ((array.Length - index) < Count)
                throw new ArgumentException();

            currentNode = firstNode;

            while (currentNode != null)
            {
                array[index] = currentNode.Value;
                currentNode = currentNode.Next;
                index++;
            }
        }
        public void Clear()
        {
            firstNode = null;
            currentNode = null;
            lastNode = null;

            Count = 0;
        }

        public bool Remove(T value)
        {
            if (Contains(value))
            {
                RemoveAt(IndexOf(value));
                return true;
            }
            else
                return false;
        }
        public void RemoveAt(int index)
        {
            if (index < 0 || index > Count - 1)
                throw new ArgumentOutOfRangeException();
            else if (index == 0)
                RemoveFirst();
            else if (index == (Count - 1))
                RemoveLast();
            else
            {
                int count = 0;

                currentNode = firstNode;

                while (currentNode != null && count != (index - 1))
                {
                    currentNode = currentNode.Next;
                    count++;
                }

                currentNode.Next = currentNode.Next.Next;

                Count--;
            }
        }
        public void RemoveFirst()
        {
            if (firstNode != null)
            {
                firstNode = firstNode.Next;
                Count--;
            }
        }
        public void RemoveLast()
        {
            if (lastNode != null)
            {
                int count = 0;

                currentNode = firstNode;

                while (currentNode != null && count != (Count - 2))
                {
                    currentNode = currentNode.Next;
                    count++;
                }

                currentNode.Next = null;
                lastNode = currentNode;

                Count--;
            }
        }

        public void Print()
        {
            if (firstNode != null)
            {
                currentNode = firstNode;

                while (currentNode != null)
                {
                    Console.WriteLine(currentNode.Value);
                    currentNode = currentNode.Next;
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index > Count - 1)
                    throw new IndexOutOfRangeException();
                else if (index == 0)
                    return firstNode.Value;
                else if (index == Count - 1)
                    return lastNode.Value;
                else
                {
                    int count = 0;

                    currentNode = firstNode;

                    while (currentNode != null && count != index)
                    {
                        currentNode = currentNode.Next;
                        count++;
                    }

                    return currentNode.Value;
                }
            }
            set
            {
                if (index < 0 || index > Count - 1)
                    throw new IndexOutOfRangeException();
                else if (index == 0)
                    AddFirst(value);
                else if (index == Count - 1)
                    AddLast(value);
                else
                {
                    Node<T> newNode = new Node<T>(value);
                    int count = 0;

                    currentNode = firstNode;

                    while (currentNode != null && count != index)
                    {
                        currentNode = currentNode.Next;
                        count++;
                    }

                    newNode.Next = currentNode.Next;
                    currentNode = newNode;
                }
            }
        }

        public class Enumerator : IEnumerator<T>, IDisposable
        {
            SinglyLinkedList<T> list;

            int index;

            public Enumerator(SinglyLinkedList<T> list)
            {
                this.list = list;
                index = -1;
            }

            public bool MoveNext()
            {
                return index++ < list.Count - 1;
            }

            public T Current
            {
                get
                {
                    return list[index];
                }
            }
            object IEnumerator.Current
            {
                get
                {
                    return list[index];
                }
            }

            public void Reset()
            {
                index = -1;
            }
            public void Dispose()
            {
                list = null;
                index = -1;
            }
        }
    }
    public class Node<T>
    {
        Node<T> next;
        T data;

        public Node<T> Next
        {
            get { return next; }
            set { next = value; }
        }
        public T Value
        {
            get { return data; }
            set { data = value; }
        }

        public Node() { }
        public Node(T data)
        {
            this.data = data;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
