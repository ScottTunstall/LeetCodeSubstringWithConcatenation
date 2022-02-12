using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubstringWithConcatenation
{
    public class Solution
    {
        private UniquePermutationsCalculator _perms = new();

        public IList<int> FindSubstring(string s, string[] words)
        {
            var perms = _perms.GetUniquePermutations(words);

            var positionsFoundAt = new List<int>();

            foreach (var perm in perms)
            {
                var pos = s.IndexOf(perm);
                if (pos>-1)
                    positionsFoundAt.Add(pos);
            }

            return positionsFoundAt;
        }
    }
}
