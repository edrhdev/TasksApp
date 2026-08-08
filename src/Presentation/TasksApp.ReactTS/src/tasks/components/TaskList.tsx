import { Skeleton } from "@/components/ui/skeleton";
import { CheckCircle2 } from "lucide-react";
import type { TaskModel } from "../types/TaskModel";
import { TaskItemRow } from "./TaskItemRow";

interface TaskListProps {
    tasks: TaskModel[];
    onToggle: (id: string) => void;
    onDelete: (id: string) => void;
    isLoading: boolean;
    isToggling: boolean;
    isDeleting: boolean;
}

export function TaskList({ tasks, isLoading, onToggle, isToggling, onDelete, isDeleting }: TaskListProps) {
    if (isLoading) {
        return (
            <div className="space-y-3">
                {[1, 2, 3].map((i) => (
                    <Skeleton key={i} className="h-16 w-full rounded-xl" />
                ))}
            </div>
        );
    }

    if (tasks.length === 0) {
        return (
            <div className="text-center py-12 border border-dashed rounded-xl p-8 space-y-3 bg-muted/20">
                <CheckCircle2 className="w-10 h-10 text-muted-foreground mx-auto" />
                <p className="text-base font-medium">No tasks found</p>
                <p className="text-xs text-muted-foreground">
                    You don't have any pending tasks.
                </p>
                <p className="text-xs text-muted-foreground">
                    Create one above to get started.
                </p>
            </div>
        );
    }

    return (
        <div className="space-y-3">
            {tasks.map((task) => (
                <TaskItemRow
                    key={task.id}
                    task={task}
                    onToggle={onToggle}
                    onDelete={onDelete}
                    isBusy={isLoading || isToggling || isDeleting}
                />
            ))}
        </div>
    );
}