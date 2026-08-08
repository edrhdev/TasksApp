export interface TaskModel {
    id: string;
    title: string;
    isCompleted: boolean;
    createdAt: string;
    completedAt?: string | null;

    isUpdating?: boolean;
}
