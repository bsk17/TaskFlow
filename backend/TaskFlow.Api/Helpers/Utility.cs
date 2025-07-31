namespace TaskFlow.Api.Helpers
{
    public static class Utility
    {
        public static bool IsValidStatusTransition(TaskStatus current, TaskStatus next)
        {
            // Simple example, customize as your workflow needs
            if (current == TaskStatus.Todo && (next == TaskStatus.InProgress || next == TaskStatus.Cancelled)) return true;
            if (current == TaskStatus.InProgress && (next == TaskStatus.Done || next == TaskStatus.Blocked)) return true;
            if (current == TaskStatus.Blocked && next == TaskStatus.InProgress) return true;
            return current == next;
        }
    }
}