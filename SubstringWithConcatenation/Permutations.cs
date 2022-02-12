namespace SubstringWithConcatenation
{
    public class UniquePermutationsCalculator
    {
        private readonly HashSet<string> _permutations = new(); 

        public IList<string> GetUniquePermutations(string[] words)
        {
            _permutations.Clear();
            HeapPermutation(words, words.Length);
            return _permutations.ToList();
        }

        private void HeapPermutation(string[] words, int size)
        {
            // if size becomes 1 then print the obtained
            // permutation
            if (size == 1)
            {
                var wordsCombined = string.Concat(words);
                if (!_permutations.Contains(wordsCombined))
                    _permutations.Add(wordsCombined);
                return;
            }

            for (int i = 0; i < size; i++)
            {
                HeapPermutation(words, size - 1);

                // if size is odd, swap 0th i.e (first) and
                // (size-1)th i.e (last) element
                if ((size & 1) == 1)
                {
                    Swap(ref words[0], ref words[size - 1]);
                }

                // If size is even, swap ith and
                // (size-1)th i.e (last) element
                else
                {
                    Swap(ref words[i], ref words[size - 1]);
                }
            }
        }

        private static void Swap(ref string a, ref string b)
        {
            (a, b) = (b, a);
        }
    }
}
