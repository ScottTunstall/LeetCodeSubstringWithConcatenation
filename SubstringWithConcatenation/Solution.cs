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

            Dictionary<string, int> usedWords = new();
            HashSet<char> firstLetterOfEachWord = new();

            foreach (var word in words)
            {
                usedWords[word] = 0;
                firstLetterOfEachWord.Add(word[0]);
            }

            foreach (var word in words)
            {
                // There may be a duplicate word in the word list, so count how many instances
                // of each word there are.
                usedWords[word] += 1;
            }
            
            // Clone dictionary
            Dictionary<string, int> reloadCache = new(usedWords);

            var wordLen = words[0].Length;
            var numberOfWords = words.Length;

            var left = 0;

            while (left+wordLen <= stringLen)
            {
                var right = left;

                // Do we have any words beginning with this letter?
                var ch = s[left];
                if (!firstLetterOfEachWord.Contains(ch))
                {
                    // No, so don't waste time with substring call. Bump "left" to look at next char in s.
                    left++;
                    continue;
                }
                
                var countOfWords = 0;
                
                var readWord = s.Substring(right, wordLen);
                
                while (usedWords.TryGetValue(readWord, out var remaining) && remaining > 0)
                {
                    countOfWords++;
                    usedWords[readWord]-=1; 
                    right += wordLen;
                    if (right+wordLen > stringLen)
                        break;

                    readWord = s.Substring(right, wordLen);
                }

                // If all the words in the dictionary have been used, then record the start index of the sequence 
                if (countOfWords == numberOfWords)
                {
                    foundIndices.Add(left);
                }


                // Copy reloadCache dictionary to useWords, "reloading" it for re-use
                foreach (var (key, value) in reloadCache)
                {
                    usedWords[key] = value;
                }
                
                left++;
            }

            return foundIndices;
        }
    }
}
