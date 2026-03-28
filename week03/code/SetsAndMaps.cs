using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character
    /// words (lower case, no duplicates). Using sets, find an O(n)
    /// solution for returning all symmetric pairs of words.
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return:
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order
    /// of the specific words in each string in the array.
    ///
    /// "at" would not be returned because "ta" is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: "aa") then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        var seen = new HashSet<string>();
        var result = new List<string>();

        foreach (var word in words)
        {
            if (word[0] == word[1])
            {
                continue;
            }

            var reverse = $"{word[1]}{word[0]}";

            if (seen.Contains(reverse))
            {
                result.Add($"{reverse} & {word}");
            }

            seen.Add(word);
        }

        return result.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.
    ///
    /// The summary should be stored in a dictionary where the key
    /// is the degree earned and the value is the number of people
    /// that have earned that degree.
    ///
    /// The degree information is in the 4th column of the file.
    /// There is no header row in the file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();

        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            var degree = fields[3];

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
    ///
    /// An anagram occurs when the same letters in a word are
    /// rearranged into another word.
    ///
    /// A dictionary is used to solve the problem.
    ///
    /// Examples:
    /// is_anagram("CAT", "ACT") → true
    /// is_anagram("DOG", "GOOD") → false (GOOD has two O's)
    ///
    /// Important Notes:
    /// - Ignore spaces
    /// - Ignore letter case
    ///
    /// Example:
    /// 'Ab' and 'Ba' should be considered anagrams.
    ///
    /// Reminder: You can access a letter by index in a string using [].
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        word1 = word1.Replace(" ", "").ToLower();
        word2 = word2.Replace(" ", "").ToLower();

        if (word1.Length != word2.Length)
        {
            return false;
        }

        var letters = new Dictionary<char, int>();

        foreach (char c in word1)
        {
            if (letters.ContainsKey(c))
            {
                letters[c]++;
            }
            else
            {
                letters[c] = 1;
            }
        }

        foreach (char c in word2)
        {
            if (!letters.ContainsKey(c))
            {
                return false;
            }

            letters[c]--;

            if (letters[c] < 0)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// This function reads JSON (JavaScript Object Notation) data from the
    /// United States Geological Service (USGS) consisting of earthquake data.
    ///
    /// The data includes all earthquakes that occurred during the current day.
    ///
    /// JSON data is organized into objects (similar to dictionaries).
    /// After reading the data using the HTTP client library, this function
    /// should return a list of all earthquake locations ('place' attribute)
    /// and magnitudes ('mag' attribute).
    ///
    /// Additional information about the JSON data format can be found here:
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri =
            "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";

        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);

        var json = reader.ReadToEnd();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var featureCollection =
            JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // Problem 5 (Extra Credit)
        // 1. Describe the JSON structure in FeatureCollection.cs
        // 2. Extract earthquake place and magnitude
        // 3. Return formatted strings

        return [];
    }
}