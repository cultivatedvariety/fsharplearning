using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsOfProgrammerInterviewsCSharp
{
    public class MiscListPermutations
    {
        /// <summary>
        /// The permutation of any given list is can be calculated as appending the first item in the list
        /// in all possible posititons to the sub list permutations e.g. permutation of {1,2,3} is all permutations
        /// of {2,3} with 1 inserted at each index e.g. {1,2,3}, {2,1,3}, {2,3,1} {1,3,2} {3,1,2}, {3,2,1}
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static List<List<int>> GetPermutationsRecursively(List<int> numbers)
        {
            if (numbers.Count <= 1)
            {
                return new List<List<int>>() {numbers};
            }

            var subListPermutations = GetPermutationsRecursively(numbers.Skip(1).ToList());

            List <List<int>> permutations = new List<List<int>>();
            foreach (var subListPermutation in subListPermutations)
            {
                for (int i = 0; i < numbers.Count; i++)
                {
                    List<int> permutation = new List<int>(subListPermutation);
                    if (i > permutation.Count)
                        permutation.Add(numbers[0]);
                    else
                        permutation.Insert(i, numbers[0]);
                    permutations.Add(permutation);
                }
            }
            return permutations;
        }
    }
}
