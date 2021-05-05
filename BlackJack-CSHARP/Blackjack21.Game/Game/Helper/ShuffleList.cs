using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Blackjack21.Game.Helper
{
    /// <summary>
    /// A class used to shuffle a list's objects
    /// </summary>
    /// <typeparam name="T">The object to use for the list</typeparam>
    public class ShuffleList<T> : List<T>
    {
        private Random _random;

        /// <summary>
        /// Initialize the shuffle list object
        /// </summary>
        public ShuffleList()
        {
            var cryptoResult = new byte[4];
            new RNGCryptoServiceProvider().GetBytes(cryptoResult);
            int seed = BitConverter.ToInt32(cryptoResult, 0);
            _random = new Random(seed);
        }

        /// <summary>
        /// Add a new item to the list
        /// </summary>
        /// <param name="item">Item to add to the list</param>
        /// <param name="isRandom">Add the item to a random location</param>
        public void Add(T item, bool isRandom = false)
        {
            if (Count == 0 || isRandom == false)
            {
                base.Add(item);
            }
            else
            {
                int index = Next(0, Count + 1);
                if (index == Count)
                {
                    base.Add(item);
                }
                else
                {
                    T x = this[index];
                    this[index] = item;
                    base.Add(x);
                }
            }
        }

        /// <summary>
        /// Shuffle items in the list by removing each item randomly and adding it to a seperate list before rebuilding the final list
        /// </summary>
        public void ShuffleShift()
        {
            List<T> tmp = new List<T>();

            while (this.Count > 0)
            {
                int i = Next(0, base.Count);
                tmp.Add(base[i]);
                base.RemoveAt(i);
            }

            base.AddRange(tmp.ToArray());

            tmp.Clear();
        }

        /// <summary>
        /// Shuffle items inplace by swapping locations randomly
        /// </summary>
        public void ShuffleInplace()
        {
            _random = new Random();
            for (int i = base.Count - 1; i >= 0; i--)
            {
                T tmp = base[i];
                int randomIndex = Next(0, i + 1);

                //Swap elements
                base[i] = base[randomIndex];
                base[randomIndex] = tmp;
            }
        }

        /// <summary>
        /// Removes the first item on top of the list and returns it
        /// </summary>
        /// <returns>Returns the retmoved item</returns>
        public T Pop()
        {
            T t = base[0];
            base.RemoveAt(0);
            return t;
        }

        /// <summary>
        /// Removes a random item from the list and returns it
        /// </summary>
        /// <returns>Returns the removed list item</returns>
        public T PopRandom()
        {
            int i = Next(0, base.Count);
            T t = base[i];
            base.RemoveAt(i);
            return t;
        }

        /// <summary>
        /// Selects a random item from the list without removing it
        /// </summary>
        /// <returns>Returns the list item</returns>
        public T SelectRandom()
        {
            int i = Next(0, base.Count);
            T t = base[i];
            return t;
        }

        /// <summary>
        /// Select the next random number
        /// </summary>
        /// <param name="min">Minimum number</param>
        /// <param name="max">Maximum number</param>
        /// <returns>returns a random number between the minimum and maximum value</returns>
        private int Next(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}
