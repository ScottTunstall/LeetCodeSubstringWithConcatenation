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

            var wordLen = words[0].Length;
            var numberOfWords = words.Length;

            var left = 0;

            while (left+wordLen <= s.Length)
            {
                var right = left;

                var countOfWords = 0;

                foreach (var word in words)
                {
                    usedWords[word] = 0;
                }

                foreach (var word in words)
                {
                    usedWords[word] += 1;
                }

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

                left ++;
            }

            return foundIndices;
        }
    }
}
