package Blackjack21.Game.Helper;

import java.util.ArrayList;
import java.util.Date;
import java.util.Random;

/**
 * A class used to shuffle a list's objects The object to use for the list
 *
 * @param <T> Object parameter
 */
public class ShuffleList<T> extends ArrayList<T>
{

    /**
     * Initialize the shuffle list object
     */
    public ShuffleList()
    {
    }

    /**
     * Add a new item to the list
     *
     * @param item Item to add to the list
     * @param isRandom Add the item to a random location
     */
    public void add(T item, boolean isRandom)
    {
        if (super.isEmpty() || isRandom == false)
        {
            super.add(item);
        }
        else
        {
            int index = next(0, size() + 1);
            if (index == size())
            {
                super.add(item);
            }
            else
            {
                T x = this.get(index);
                this.set(index, item);

                super.add(x);
            }
        }
    }

    /**
     * Shuffle items in the list by removing each item randomly and adding it to
     * another list before rebuilding the final list
     */
    public void shuffleShift()
    {
        ArrayList<T> tmp = new ArrayList<>();
        while (super.size() > 0)
        {
            int i = next(0, super.size());
            tmp.add(super.get(i));
            super.remove(i);

        }
        super.addAll(tmp);
        tmp.clear();
    }

    /**
     * Shuffle items in place by swapping locations randomly
     */
    public void shuffleInplace()
    {
        for (int i = super.size() - 1; i >= 0; i--)
        {
            T tmp = super.get(i);
            int randomIndex = next(0, i + 1);
            //Swap elements
            super.set(i, super.get(randomIndex));
            super.set(randomIndex, tmp);
        }
    }

    /**
     * Removes the first item on top of the list and returns it
     *
     * @return Returns the removed item
     */
    public T pop()
    {
        T t = super.get(0);
        super.remove(0);
        return t;
    }

    /**
     * Removes a random item from the list and returns it
     *
     * @return Returns the removed list item
     */
    public T popRandom()
    {
        int i = next(0, super.size());
        T t = super.get(i);
        super.remove(i);
        return t;
    }

    /**
     * Selects a random item from the list without removing it
     *
     * @return Returns the list item
     */
    public T selectRandom()
    {
        int i = next(0, super.size());
        T t = super.get(i);
        return t;
    }

    /**
     * Select the next random number
     *
     * @param min Minimum number
     * @param max Maximum number
     * @return returns a random number between the minimum and maximum value
     */
    private int next(int min, int max)
    {
        // java random code copied from https://www.educative.io/edpresso/how-to-generate-random-numbers-in-java
        return (int) Math.floor(Math.random() * (max - min) + min);
    }

}
