using System.Collections;

public static class Recursion
{
    /// <summary>
    /// #############
    /// # Problem 1 #
    /// #############
    /// Using recursion, find the sum of 1^2 + 2^2 + 3^2 + ... + n^2
    /// and return it.  Remember to both express the solution 
    /// in terms of recursive call on a smaller problem and 
    /// to identify a base case (terminating case).  If the value of
    /// n <= 0, just return 0.   A loop should not be used.
    /// </summary>
    public static int SumSquaresRecursive(int n)
    {
        // Base case
        if (n <= 0)
            return 0;

        // Recursive case
        return (n * n) + SumSquaresRecursive(n - 1);
    }

    /// <summary>
    /// #############
    /// # Problem 2 #
    /// #############
    /// Using recursion, insert permutations of length
    /// 'size' from a list of 'letters' into the results list.
    /// </summary>
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        // Base case: the word has reached the requested size
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        // Try each letter that has not already been used
        for (int i = 0; i < letters.Length; i++)
        {
            if (!word.Contains(letters[i]))
            {
                PermutationsChoose(
                    results,
                    letters,
                    size,
                    word + letters[i]
                );
            }
        }
    }

    /// <summary>
    /// #############
    /// # Problem 3 #
    /// #############
    /// Count the number of ways to climb s stairs using
    /// 1, 2, or 3 steps at a time.
    /// </summary>
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        // Base Cases
        if (s == 0)
            return 0;
        if (s == 1)
            return 1;
        if (s == 2)
            return 2;
        if (s == 3)
            return 4;

        // Create the dictionary the first time the function is called
        if (remember == null)
            remember = new Dictionary<int, decimal>();

        // Check if we already calculated this value
        if (remember.ContainsKey(s))
            return remember[s];

        // Solve recursively
        decimal ways =
            CountWaysToClimb(s - 1, remember) +
            CountWaysToClimb(s - 2, remember) +
            CountWaysToClimb(s - 3, remember);

        // Remember the result so we don't calculate it again
        remember[s] = ways;

        return ways;
    }

    /// <summary>
    /// #############
    /// # Problem 4 #
    /// #############
    /// Using recursion, insert all possible binary strings
    /// for a given pattern into the results list.
    /// </summary>
    public static void WildcardBinary(string pattern, List<string> results)
    {
        // Base case: no more characters to process
        if (pattern.Length == 0)
        {
            results.Add("");
            return;
        }

        // Take the first character
        char first = pattern[0];

        // Recursively solve the rest of the pattern
        string rest = pattern[1..];

        if (first == '*')
        {
            // A wildcard can become 0 or 1
            List<string> restResults = new List<string>();
            WildcardBinary(rest, restResults);

            foreach (string result in restResults)
            {
                results.Add("0" + result);
                results.Add("1" + result);
            }
        }
        else
        {
            // Keep normal 0 or 1 characters unchanged
            List<string> restResults = new List<string>();
            WildcardBinary(rest, restResults);

            foreach (string result in restResults)
            {
                results.Add(first + result);
            }
        }
    }

    /// <summary>
    /// Use recursion to insert all paths that start at (0,0) and end at the
    /// 'end' square into the results list.
    /// </summary>
    public static void SolveMaze(
        List<string> results,
        Maze maze,
        int x = 0,
        int y = 0,
        List<ValueTuple<int, int>>? currPath = null)
    {
        // If this is the first time running the function,
        // initialize the current path.
        if (currPath == null)
        {
            currPath = new List<ValueTuple<int, int>>();
        }

        // Check if the current position is valid.
        if (!maze.IsValidMove(currPath, x, y))
            return;

        // Add the current position to the path.
        currPath.Add((x, y));

        // If we reached the end, save this path.
        if (maze.IsEnd(x, y))
        {
            results.Add(currPath.AsString());
            return;
        }

        // Try moving in all four directions.
        SolveMaze(results, maze, x + 1, y, currPath);
        SolveMaze(results, maze, x - 1, y, currPath);
        SolveMaze(results, maze, x, y + 1, currPath);
        SolveMaze(results, maze, x, y - 1, currPath);

        // Remove the current position when going back
        // so other paths can use this square.
        currPath.RemoveAt(currPath.Count - 1);
    }
}