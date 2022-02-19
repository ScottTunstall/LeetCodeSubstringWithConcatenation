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

            Dictionary<string, int> _usedWords = new();

            foreach (var word in words)
            {
                _usedWords[word] = 0;
            }

            var wordLen = words[0].Length;
            var numberOfWords = words.Length;

            var left = 0;

            while (left < s.Length)
            {
                var right = left;

                var countOfWords = 0;

                foreach (var word in words)
                {
                    _usedWords[word] += 1;
                }

                var readWord = s.Substring(right, wordLen);
                while (right+wordLen < s.Length && _usedWords.ContainsKey(readWord) && _usedWords[readWord]>0)
                {
                    countOfWords++;
                    _usedWords[readWord]=-1;
                    right += wordLen;
                    readWord = s.Substring(right, wordLen);
                }

                if (countOfWords == numberOfWords)
                {
                    foundIndices.Add(left);
                }

                left += wordLen;
            }

            return foundIndices;
        }
    }
}
