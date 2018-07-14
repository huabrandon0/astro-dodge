using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Utility
{
    public static List<int> GenerateRandom(int count, int min, int max)
    {
        if (max <= min || count < 0 || (count > max - min && max - min > 0))
        {
            throw new ArgumentOutOfRangeException("Range " + min + " to " + max +
                    " (" + ((Int64)max - (Int64)min) + " values), or count " + count + " is illegal");
        }
 
        HashSet<int> candidates = new HashSet<int>();

        System.Random random = new System.Random();

        for (int top = max - count; top < max; top++)
        {
            if (!candidates.Add(random.Next(min, top + 1)))
            {
                candidates.Add(top);
            }
        }
 
        List<int> result = candidates.ToList();
 
        for (int i = result.Count - 1; i > 0; i--)
        {
            int k = random.Next(i + 1);
            int tmp = result[k];
            result[k] = result[i];
            result[i] = tmp;
        }
        return result;
    }
}
