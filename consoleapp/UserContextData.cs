using Microsoft.SemanticKernel;

namespace SemanticKernelTour;

class UserContextData
{
    [KernelFunction]
    public DateTimeOffset GetCurrentDateAndTime() {
        return DateTimeOffset.Now;
    }
}