using Microsoft.SemanticKernel;

namespace SemanticKernelTour
{
    public class StringUtilities
    {
        [KernelFunction]
        public int CountLettersInInput(string input, char letter)
        {
            var s = letter.ToString().ToLowerInvariant();
            return input.ToLowerInvariant().Count(c => c.ToString() == s);
        }
    }
}