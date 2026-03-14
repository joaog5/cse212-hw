public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // PLAN:
        // 1. Create an array of doubles with size equal to length.
        // 2. Use a for loop that runs from index 0 up to length - 1.
        // 3. For each index, calculate the multiple of the number.
        //    The first value should be number * 1, the second number * 2, etc.
        // 4. Store the calculated value in the array at the current index.
        // 5. After the loop finishes, return the filled array.

        double[] result = new double[length];

        for (int i = 0; i < length; i++)
        {
            result[i] = number * (i + 1);
        }

        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // PLAN:
        // 1. Determine where to split the list. The split point is data.Count - amount.
        // 2. Get the last "amount" elements of the list using GetRange.
        // 3. Get the first part of the list from index 0 up to the split point.
        // 4. Clear the original list.
        // 5. Add the last elements first (these move to the front).
        // 6. Add the first part of the list after them.
        // 7. The list will now be rotated to the right.

        int splitIndex = data.Count - amount;

        List<int> endPart = data.GetRange(splitIndex, amount);
        List<int> startPart = data.GetRange(0, splitIndex);

        data.Clear();
        data.AddRange(endPart);
        data.AddRange(startPart);
    }
}