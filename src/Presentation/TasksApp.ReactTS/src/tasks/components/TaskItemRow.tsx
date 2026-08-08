import { Trash2 } from "lucide-react";
import { Checkbox } from "@/components/ui/checkbox";
import { Button } from "@/components/ui/button";
import { Card, CardContent } from "@/components/ui/card";
import type { TaskModel } from "../types/TaskModel";
import { formatDate } from "../../lib/utils";
import { AlertDialog, AlertDialogAction, AlertDialogCancel, AlertDialogContent, AlertDialogDescription, AlertDialogFooter, AlertDialogHeader, AlertDialogTitle, AlertDialogTrigger } from "../../components/ui/alert-dialog";

interface TaskItemRowProps {
    task: TaskModel;
    onToggle: (id: string) => void;
    onDelete: (id: string) => void;
    isBusy: boolean;
}

export function TaskItemRow({ task, onToggle, onDelete, isBusy }: TaskItemRowProps) {
    return (
        <Card className={`p-0 transition-all ${task.isCompleted ? "opacity-60 bg-muted/50" : ""} ${isBusy ? "pointer-events-none opacity-50" : ""}`}>
            <CardContent className="p-4 flex items-center justify-between gap-3">
                <div className="flex items-center gap-3 min-w-0 flex-1">
                    <Checkbox
                        id={`task-${task.id}`}
                        checked={task.isCompleted}
                        onCheckedChange={() => onToggle(task.id)}
                        disabled={isBusy}
                        aria-label={`Mark "${task.title}" as completed`}
                    />
                    <div className="flex flex-col min-w-0">
                        <label
                            htmlFor={`task-${task.id}`}
                            className={`text-sm font-medium truncate cursor-pointer ${task.isCompleted ? "line-through text-muted-foreground" : ""}`}
                        >
                            {task.title}
                        </label>

                        <div className="flex flex-wrap items-center gap-x-2 text-[11px] text-muted-foreground mt-0.5">
                            <span>Created: {formatDate(task.createdAt)}</span>
                            {task.isCompleted && task.completedAt && (
                                <>
                                    <span>•</span>
                                    <span className="text-emerald-500/90 font-medium">
                                        Completed: {formatDate(task.completedAt)}
                                    </span>
                                </>
                            )}
                        </div>
                    </div>
                </div>

                <AlertDialog>
                    <AlertDialogTrigger asChild>
                        <Button
                            variant="ghost"
                            size="icon"
                            disabled={isBusy}
                            className="text-destructive hover:text-destructive hover:bg-destructive/10 shrink-0 self-center"
                            aria-label={`Delete task "${task.title}"`}
                        >
                            <Trash2 className="w-4 h-4" />
                        </Button>
                    </AlertDialogTrigger>
                    <AlertDialogContent>
                        <AlertDialogHeader>
                            <AlertDialogTitle>Are you sure?</AlertDialogTitle>
                            <AlertDialogDescription>
                                This action cannot be undone. This will permanently delete the
                                task <strong className="text-foreground">"{task.title}"</strong>.
                            </AlertDialogDescription>
                        </AlertDialogHeader>
                        <AlertDialogFooter>
                            <AlertDialogCancel>Cancel</AlertDialogCancel>
                            <AlertDialogAction
                                onClick={() => onDelete(task.id)}
                                className="bg-destructive hover:bg-destructive/90 text-destructive-foreground"
                            >
                                Delete
                            </AlertDialogAction>
                        </AlertDialogFooter>
                    </AlertDialogContent>
                </AlertDialog>
            </CardContent>
        </Card>
    );
}