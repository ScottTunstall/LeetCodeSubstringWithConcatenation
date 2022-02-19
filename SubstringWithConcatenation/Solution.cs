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
            var foundIndices = new List<int>();

            Dictionary<string, int> usedWords = new();
            Dictionary<string, int> reloadCache = new();

            foreach (var word in words)
            {
                usedWords[word] = 0;
            }

            foreach (var word in words)
            {
                usedWords[word] += 1;
            }

            foreach (var kvp in usedWords)
            {
                reloadCache[kvp.Key] = kvp.Value;
            }

            var wordLen = words[0].Length;
            var numberOfWords = words.Length;

            var left = 0;

            while (left+wordLen <= s.Length)
            {
                var right = left;

                var countOfWords = 0;

                var readWord = s.Substring(right, wordLen);
                while (usedWords.ContainsKey(readWord) && usedWords[readWord]>0)
                {
                    countOfWords++;
                    usedWords[readWord]-=1;
                    right += wordLen;
                    if (right+wordLen > s.Length)
                        break;

                    readWord = s.Substring(right, wordLen);
                }

                if (countOfWords == numberOfWords)
                {
                    foundIndices.Add(left);
                }

                foreach (var kvp in reloadCache)
                {
                    usedWords[kvp.Key] = kvp.Value;
                }
                
                left++;
            }

            return foundIndices;
        }
    }
}
