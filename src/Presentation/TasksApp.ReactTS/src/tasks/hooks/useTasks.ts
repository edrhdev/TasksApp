import { taskApiClient } from "@/tasks/services/taskApiClient";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { toast } from "sonner";
import type { TaskModel } from "../types/TaskModel";
import type { ApiResult, ApiResultData } from "../types/ApiResult";

const TASKS_QUERY_KEY = ["tasks"];

export function useTasks() {
    const queryClient = useQueryClient();

    // Get all tasks
    const {
        data: tasks = [],
        isPending: isLoading,
        refetch,
        isError,
        error
    } = useQuery({
        queryKey: TASKS_QUERY_KEY,
        queryFn: async () => {
            const result: ApiResultData<TaskModel[]> = await taskApiClient.getAll();

            if (!result.isSuccess) {
                const errorMessage = result.error?.detail || "Failed to load tasks";

                toast.error(result.error?.title || "Error", {
                    description: errorMessage,
                });

                throw new Error(errorMessage);
            }

            return result.data ?? [];
        },
    });

    // Create a new task
    const createMutation = useMutation({
        mutationFn: async (title: string) => {
            const result: ApiResultData<TaskModel> = await taskApiClient.create({ title });

            if (!result.isSuccess) {
                throw new Error(result.error?.detail || "Could not create task");
            }

            return result.data!;
        },
        onSuccess: () => {
            toast.success("Task created successfully");
            queryClient.invalidateQueries({ queryKey: TASKS_QUERY_KEY });
        },
        onError: (error: Error) => {
            toast.error("Creation Failed", { description: error.message });
        },
    });

    // Toggle task status (completed/incomplete)
    const toggleMutation = useMutation({
        mutationFn: async (id: string) => {
            const result: ApiResultData<TaskModel> = await taskApiClient.toggleStatus(id);

            if (!result.isSuccess) {
                throw new Error(result.error?.detail || "Could not update task status");
            }

            return result.data!;
        },
        onMutate: async (id: string) => {
            await queryClient.cancelQueries({ queryKey: TASKS_QUERY_KEY });
            const previousTasks = queryClient.getQueryData<TaskModel[]>(TASKS_QUERY_KEY);

            queryClient.setQueryData<TaskModel[]>(TASKS_QUERY_KEY, (old = []) =>
                old.map((task) =>
                    task.id === id ? {
                        ...task,
                        isCompleted: !task.isCompleted
                    } : task
                )
            );

            return { previousTasks };
        },
        onError: (error: Error, _variables, context) => {
            if (context?.previousTasks) {
                queryClient.setQueryData(TASKS_QUERY_KEY, context.previousTasks);
            }

            toast.error("Update Failed", { description: error.message });
        },
        onSettled: () => {
            queryClient.invalidateQueries({ queryKey: TASKS_QUERY_KEY });
        },
    });

    // Delete a task
    const deleteMutation = useMutation({
        mutationFn: async (id: string) => {
            const result: ApiResult = await taskApiClient.delete(id);
            if (!result.isSuccess) {
                throw new Error(result.error?.detail || "Could not delete task");
            }
        },
        onSuccess: () => {
            toast.success("Task deleted successfully");
            queryClient.invalidateQueries({ queryKey: TASKS_QUERY_KEY });
        },
        onError: (error: Error) => {
            toast.error("Deletion Failed", { description: error.message });
        },
    });

    // wrappers, more readable and easier to use in components
    const toggleTaskStatus = async (id: string): Promise<void> => {
        await toggleMutation.mutateAsync(id);
    };

    const createTask = async (title: string): Promise<void> => {
        await createMutation.mutateAsync(title);
    };

    const deleteTask = async (id: string): Promise<void> => {
        await deleteMutation.mutateAsync(id);
    };

    const refreshTasks = async (): Promise<void> => {
        await refetch();
    };

    return {
        tasks,
        isLoading,
        isError,
        error,
        refreshTasks,
        createTask,
        toggleTaskStatus,
        isToggling: toggleMutation.isPending,
        deleteTask,
        isDeleting: deleteMutation.isPending,
    };
}