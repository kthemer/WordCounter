using System;
using System.Collections.Generic;
using System.Text;

using System.Collections.Concurrent;  // for using ConcurrentDictionary
using System.Text.RegularExpressions; // for using Regex
using System.Net;                     // for using WebClient
using System.IO;                      // for using File

namespace WordCounter
{
    public class WordCounter
    {
        /// <summary>A class to hold information about a URL</summary>
        private class URLInfo
        {
            internal List<string> keywords; // the keywords collection last sought in this URL
            internal int keywordCount;      // the count of occurrences of keywords in this URL
            internal DateTime lastUpdated;  // the time this instance was last updated

            internal URLInfo(int count, DateTime updated, List<string> words)
            {
                keywordCount = count;
                lastUpdated = updated;

                keywords = new List<string>();
                if (words != null && words.Count > 0)
                {
                    foreach (string word in words)
                    {
                        keywords.Add(word);
                    }
                }
            }

            /// <summary>Indicates whether this instance has current data which can be reused</summary>
            /// <returns>True if this instance was last updated in the last hour AND has the same set of keywords, false otherwise</returns>
            internal bool IsCurrent(List<string> newWords)
            {
                return DateTime.Compare(lastUpdated.AddHours(1), DateTime.Now) > 0 && CompareKeywords(newWords);
            }

            /// <summary>Compares a list of words with this instance's list of keywords</summary>
            /// <param name="words">The list of words to compare with this URL's keywords</param>
            /// <returns>True if the lists are the same, false otherwise</returns>
            private bool CompareKeywords(List<string> words)
            {
                if (words == null)
                {
                    throw new ArgumentNullException(nameof(words));
                }

                if (words.Count != keywords.Count)
                {
                    return false;
                }
                
                foreach (string word in words)
                {
                    if (!keywords.Contains(word))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        // The ConcurrentDictionary type supports multi-threaded operations.
        ConcurrentDictionary<string, URLInfo> _urlData = new ConcurrentDictionary<string, URLInfo>();
        private Object lockObj = new Object();

        /// <summary>Gets the count of keyword occurrences in a local or web resource</summary>
        /// <param name="keywords">The collection of keywords in List format</param>
        /// <param name="url">The URL of a local file or a web resource</param>
        /// <returns>Either a cached or recalculated count of the keywords in the URL</returns>
        public int GetKeywordCount(List<String> keywords, String url)
        {
            try
            {
                if (keywords == null)
                {
                    throw new ArgumentNullException(nameof(keywords));
                }
                if (string.IsNullOrWhiteSpace(url))
                {
                    throw new ArgumentNullException(nameof(url));
                }
                if (keywords.Count == 0)
                {
                    return 0;
                }

                lock (lockObj)
                {
                    // Check if this URL + keywords combination is already in the cache.
                    URLInfo urlInfo;
                    if (_urlData.ContainsKey(url))
                    {
                        if (_urlData.TryGetValue(url, out urlInfo))
                        {
                            if (urlInfo.IsCurrent(keywords))
                            {// The cache holds a recent (< 1 hour old) instance of this URL + keywords combination.
                                return urlInfo.keywordCount;
                            }
                        }
                    }

                    // No recent or matching instance found in the cache, so conduct a new search.
                    int count = 0;
                    string file = (IsLocalPath(url) ? GetFileResourceContents(url) : GetWebResourceContents(url));
                    string pattern = BuildRegexPatternFromList(keywords);

                    count = GetRegexMatchCount(file, pattern);

                    // The GetOrAdd method will add the new instance, unless another thread has just added it.
                    urlInfo = _urlData.GetOrAdd(url, new URLInfo(count, DateTime.Now, keywords));

                    return count;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>Builds a Regex Pattern from a List of keywords</summary>
        /// <param name="keywords">A List of keywords to include in the pattern</param>
        /// <returns>A pipe-delimited Regex pattern which matches whole words only</returns>
        private string BuildRegexPatternFromList(List<string> keywords)
        {
            try
            {
                if (keywords == null || keywords.Count == 0)
                {
                    return string.Empty;
                }

                // Enclose the pattern in word boundary tags, i.e. "\b".
                // Note that the pattern is case-sensitive by default.
                StringBuilder pattern = new StringBuilder(@"\b(");

                pattern.Append(keywords[0]);

                for (int k = 1; k < keywords.Count; k++)
                {
                    pattern.Append("|");
                    pattern.Append(keywords[k]);
                }

                pattern.Append(@")\b");

                return pattern.ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>Gets the count of matches of a pattern in a string</summary>
        /// <param name="file">The string to search, such as the contents of a file or webpage</param>
        /// <param name="pattern">The Regex pattern to use in the search</param>
        /// <returns>A count of matches of the pattern in the file</returns>
        private int GetRegexMatchCount(string file, string pattern)
        {
            try
            {
                return Regex.Matches(file, pattern).Count;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>Gets the contents of a web resource as a string</summary>
        /// <param name="url">The URL of the web resource</param>
        /// <returns>The web resource contents in string format</returns>
        private string GetWebResourceContents(string url)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(url))
                {
                    return string.Empty;
                }

                string content;
                using (WebClient client = new WebClient())
                {
                    // Read the entire web resource and return the contents as a string.
                    content = client.DownloadString(url);
                }

                return content;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>Gets the contents of a local file resource as a string</summary>
        /// <param name="url">The URL of the local file</param>
        /// <returns>The file contents in string format</returns>
        private string GetFileResourceContents(string url)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(url))
                {
                    return string.Empty;
                }

                // Read the entire file contents and return the result as a string.
                return File.ReadAllText(url);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>Indicates whether a URL points to a local file or a web resource</summary>
        /// <param name="url">The URL to examine</param>
        /// <returns>True if the URL references a local file; false otherwise</returns>
        private bool IsLocalPath(string url)
        {
            try
            {
                return new Uri(url).IsFile;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
