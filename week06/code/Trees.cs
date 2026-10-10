
public static class Trees
{
    /// <summary>
    /// Given a sorted list, create a balanced BST.
    /// </summary>
    public static BinarySearchTree CreateTreeFromSortedList(int[] sortedNumbers)
    {
        var bst = new BinarySearchTree();

        InsertMiddle(sortedNumbers, 0, sortedNumbers.Length - 1, bst);

        return bst;
    }

    /// <summary>
    /// Insert the middle value of the current range, then
    /// recursively insert the values in the left and right halves.
    /// </summary>
    /// <param name="sortedNumbers">Input numbers that are already sorted</param>
    /// <param name="first">The first index to consider</param>
    /// <param name="last">The last index to consider</param>
    /// <param name="bst">The tree in which to insert the values</param>
    private static void InsertMiddle(
        int[] sortedNumbers,
        int first,
        int last,
        BinarySearchTree bst)
    {
        if (first > last)
            return;

        int middle = first + (last - first) / 2;

        bst.Insert(sortedNumbers[middle]);

        InsertMiddle(sortedNumbers, first, middle - 1, bst);
        InsertMiddle(sortedNumbers, middle + 1, last, bst);
    }
}
