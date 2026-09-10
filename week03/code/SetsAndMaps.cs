using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.
    /// </summary>
    public static string[] FindPairs(string[] words)
    {
        var wordSet = new HashSet<string>(words);
        var pairs = new List<string>();

        foreach (var word in words)
        {
            // Don't match words like "aa" with themselves
            if (word[0] == word[1])
                continue;

            string reverse = $"{word[1]}{word[0]}";

            if (wordSet.Contains(reverse))
            {
                pairs.Add($"{reverse} & {word}");
                wordSet.Remove(word);
                wordSet.Remove(reverse);
            }
        }

        return pairs.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.
    /// </summary>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();

        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");

            string degree = fields[3].Trim();

            if (degrees.ContainsKey(degree))
            {
                degrees[degree]++;
            }
            else
            {
                degrees[degree] = 1;
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        var letters = new Dictionary<char, int>();

        foreach (char c in word1)
        {
            if (c == ' ')
                continue;

            char letter = char.ToLower(c);

            if (letters.ContainsKey(letter))
                letters[letter]++;
            else
                letters[letter] = 1;
        }

        foreach (char c in word2)
        {
            if (c == ' ')
                continue;

            char letter = char.ToLower(c);

            if (!letters.ContainsKey(letter))
                return false;

            letters[letter]--;

            if (letters[letter] < 0)
                return false;
        }

        foreach (var count in letters.Values)
        {
            if (count != 0)
                return false;
        }

        return true;
    }

    /// <summary>
    /// This function will read JSON data from the United States Geological Service.
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        var earthquakes = new List<string>();

        foreach (var feature in featureCollection.Features)
        {
            earthquakes.Add($"{feature.Properties.Place} - Mag {feature.Properties.Mag}");
        }

        return earthquakes.ToArray();
    }
}