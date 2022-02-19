using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubstringWithConcatenation
{
    public class Solution
    {
        public IList<int> FindSubstring(string s, string[] words)
        {
            var stringLen = s.Length;
            var foundIndices = new List<int>();

            Dictionary<string, short> wordToCountMap = new();

            foreach (var word in words)
            {
                wordToCountMap[word] = 0;
            }

            foreach (var word in words)
            {
                // As words may be duplicated, count how many instances of each word there are.
                wordToCountMap[word] += 1;
            }
            
            // Clone dictionary
            Dictionary<string, short> reloadCache = new(wordToCountMap);

            var wordLen = words[0].Length;
            var numberOfWords = words.Length;

            var left = 0;

            while (left+wordLen <= stringLen)
            {
                // Do we have any words beginning with this letter?
                var countOfWords = 0;

                bool countDictMutated = false;
                var right = left;
                while(true)
                {
                    // Hmm, would computing a hash value rather than using substring speed this up?
                    var readWord = s.Substring(right, wordLen);
                    if (!wordToCountMap.TryGetValue(readWord, out var remaining) || remaining <= 0)
                    {
                        break;
                    }

                    countOfWords++;

                    // If all the words in the dictionary have been used (their counts are 0),
                    // then record the start index of the sequence 
                    if (countOfWords == numberOfWords)
                    {
                        foundIndices.Add(left);
                        break;
                    }

                    wordToCountMap[readWord] -= 1;
                    countDictMutated = true;
                    right += wordLen;
                    if (right + wordLen > stringLen)
                        break;
                }

                // Copy reloadCache dictionary to useWords, "reloading" it for re-use
                if (countDictMutated)
                {
                    foreach (var (key, value) in reloadCache)
                    {
                        wordToCountMap[key] = value;
                    }
                }

                left++;
            }

            return foundIndices;
        }
    }
}
