namespace SubstringWithConcatenation
{
    public class Solution
    {
        public IList<int> FindSubstring(string s, string[] words)
        {
            var stringLen = s.Length;
            var wordLen = words[0].Length;          // Note: each word is the same length
            var numberOfWords = words.Length;
            var foundIndices = new List<int>();

            // Dictionary to count how many instances of a given word there are.
            Dictionary<string, short> wordCountMap = new();

            HashSet<char> firstLetterOfEachWordSet = new();

            foreach (var word in words)
            {
                // Initialise each entry in dictionary
                wordCountMap[word] = 0;
                firstLetterOfEachWordSet.Add(word[0]);
            }

            //count how many instances of each word there are.
            // For example, if the words[] array contains "good" twice then 
            // wordCountMap["good"] will be set to 2.
            foreach (var word in words)
            {
                wordCountMap[word] += 1;
            }
            
            // We've now counted how many times each word appears in wordCountMap.
            // Clone wordCountMap dictionary into wordCountCache.
            // We use this cache to *quickly* reset wordCountMap after successfully matching 1 or more words - *even if we don't match all words*.
            // This is *much* faster than rebuilding the wordCountMap dictionary from scratch (like we did above) as
            // the words[] array might have hundreds of words to process and map.
            // Each word = CPU time to process = takes longer = Leetcode timeout 
            Dictionary<string, short> wordCountCache = new(wordCountMap);
            

            // Left points to the start index of a possible word sequence in string s.
            var left = 0;

            while (left+wordLen <= stringLen)
            {
                // Check to see if the character at s[left] is the first letter of a word in words[].
                // If not, then we know left isn't pointing at the start of a word we want to match,
                // so bump left to next character in s.
                if (!firstLetterOfEachWordSet.Contains(s[left]))
                {
                    left++;
                    continue;
                }

                // OK, left *might* point at the start of a word we can match.
                // Left might even point at the start of a word sequence!
                // In this case, we need to keep count of how many words out of the words[] array we've matched. 
                // When countOfWords == numberOfWords, we've successfully matched all the words in the words[] array.
                // We then record "left" as a match position.
                var matchedWordCount = 0;

                // Flag that determines if we found any words and wordCountMap was mutated (updated)
                bool countDictMutated = false;

                // "right" is going to be used to scan ahead for words in the string, starting from position "left"
                // so think of left..right defining a window in substring s to check for a sequence of words.
                var right = left;
                do
                {
                    var readWord = s.Substring(right, wordLen);

                    // If our word count map doesn't contain the word OR the associated word count is 0,
                    // then exit this scan ahead loop. 
                    if (!wordCountMap.TryGetValue(readWord, out var remaining) || remaining <= 0)
                    {
                        break;
                    }

                    // Decrement number of times we're allowed to use this word again, on this pass.
                    wordCountMap[readWord] -= 1;

                    countDictMutated = true;

                    matchedWordCount++;

                    // If all the words have been matched (their counts are 0),
                    // then record the start index of the word sequence 
                    if (matchedWordCount == numberOfWords)
                    {
                        foundIndices.Add(left);
                        break;
                    }

                    right += wordLen;
                } while (right + wordLen <= stringLen);

                // If wordCountMap was mutated in the while loop, then it needs "reloaded" 
                // That is to say, the word counts need reset to what they were just before entering the outer while loop.
                // Use the cache to reload the WordCountMap.
                if (countDictMutated)
                {
                    foreach (var (key, value) in wordCountCache)
                    {
                        wordCountMap[key] = value;
                    }
                }

                left++;
            }

            return foundIndices;
        }
    }
}
