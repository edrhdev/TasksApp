import { Loader2, RefreshCw } from "lucide-react";
import { Button } from "../../components/ui/button";
import type { TaskModel } from "../types/TaskModel";

interface TasksCounterTabProps {
    tasks: TaskModel[];
    isBusy: boolean;
    refreshTasks: () => Promise<void>;
}

function TasksCounterTab({ tasks, isBusy, refreshTasks }: TasksCounterTabProps) {
    return (
        <div className="flex justify-center items-center text-xs text-muted-foreground px-1 font-medium">
            <span>Total: {tasks.length}</span>
            <span className="mx-2">•</span>
            <span>Pending: {tasks.filter((t) => !t.isCompleted).length}</span>
            <span className="mx-2">•</span>
            <span>Completed: {tasks.filter((t) => t.isCompleted).length}</span>
            <span className="mx-2">•</span>

            <Button
                variant="ghost"
                size="icon"
                className="text-emerald-500"
                disabled={isBusy}
                aria-label={`Refresh task list`}
                onClick={async () => await refreshTasks()}
            >
                {(isBusy) ? (
                    <Loader2 className="w-4 h-4 animate-spin mr-2" />
                ) : (
                    <RefreshCw className="w-4 h-4" />
                )}
            </Button>
        </div>
    );
}

export default TasksCounterTab;
