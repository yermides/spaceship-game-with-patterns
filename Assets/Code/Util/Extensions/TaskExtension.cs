using System.Threading.Tasks;

namespace Code.Util.Extensions
{
    public static class TaskExtensions
    {
        // Unity specific, we need to await the task to know which errors it invoked
        public static async void WrapErrors(this Task task)
        {
            await task;
        }
    }
}