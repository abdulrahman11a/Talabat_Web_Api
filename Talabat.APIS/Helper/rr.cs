using System;
using System.Collections.Generic;
using System.Linq;

public class RandomizedSet
{
    private Dictionary<int, int> keyValues; // Stores values as both keys and values
    private Random random; // Random number generator for GetRandom()

    public RandomizedSet()
    {
        keyValues = new Dictionary<int, int>();
        random = new Random();
    }

    public bool Insert(int val)
    {
        // If the value already exists, return false
        if (keyValues.ContainsKey(val))
            return false;

        // Insert the value in the dictionary
        keyValues[val] = val;
        return true;
    }

    public bool Remove(int val)
    {
        // If the value does not exist, return false
        if (!keyValues.ContainsKey(val))
            return false;

        // Remove the value from the dictionary
        keyValues.Remove(val);
        return true;
    }

    public int GetRandom()
    {
        // Select a random key from the dictionary
        int randomIndex = random.Next(keyValues.Count);
        return keyValues.ElementAt(randomIndex).Key;
    }
}

/**
 * Your RandomizedSet object will be instantiated and called as such:
 * RandomizedSet obj = new RandomizedSet();
 * bool param_1 = obj.Insert(val);
 * bool param_2 = obj.Remove(val);
 * int param_3 = obj.GetRandom();
 */