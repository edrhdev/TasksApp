import { ENV } from "@/config/env";
import { parseProblemDetails } from "./parseProblemDetails";
import type { ApiResultData, ApiResult } from "../types/ApiResult";
import type { CreateTaskModel } from "../types/CreateTaskModel";
import type { TaskModel } from "../types/TaskModel";

const TASKS_ENDPOINT = `${ENV.API_BASE_URL}/api/tasks`;

export const taskApiClient = {
    async getAll(): Promise<ApiResultData<TaskModel[]>> {
        try {
            const response = await fetch(TASKS_ENDPOINT);
            if (!response.ok) {
                return {
                    isSuccess: false,
                    error: await parseProblemDetails(response)
                };
            }

            const data: TaskModel[] = await response.json();

            return {
                isSuccess: true,
                data
            };
        } catch {
            return {
                isSuccess: false,
                error: {
                    title: "Connection Error",
                    detail: "Unable to reach the server. Please check your network connection.",
                },
            };
        }
    },

    async create(task: CreateTaskModel): Promise<ApiResultData<TaskModel>> {
        try {
            const response = await fetch(TASKS_ENDPOINT, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(task),
            });

            if (!response.ok) {
                return {
                    isSuccess: false,
                    error: await parseProblemDetails(response)
                };
            }

            const data: TaskModel = await response.json();

            return {
                isSuccess: true,
                data
            };
        } catch {
            return {
                isSuccess: false,
                error: {
                    title: "Connection Error",
                    detail: "Failed to create the task due to a network error.",
                },
            };
        }
    },

    async toggleStatus(id: string): Promise<ApiResultData<TaskModel>> {
        try {
            const response = await fetch(`${TASKS_ENDPOINT}/${id}/toggle`, {
                method: "PATCH",
            });

            if (!response.ok) {
                return {
                    isSuccess: false,
                    error: await parseProblemDetails(response)
                };
            }

            const data: TaskModel = await response.json();

            return {
                isSuccess: true,
                data
            };
        } catch {
            return {
                isSuccess: false,
                error: {
                    title: "Connection Error",
                    detail: "Failed to update task status due to a network error.",
                },
            };
        }
    },

    async delete(id: string): Promise<ApiResult> {
        try {
            const response = await fetch(`${TASKS_ENDPOINT}/${id}`, {
                method: "DELETE",
            });

            if (!response.ok) {
                return {
                    isSuccess: false,
                    error: await parseProblemDetails(response)
                };
            }

            return { isSuccess: true };
        } catch {
            return {
                isSuccess: false,
                error: {
                    title: "Connection Error",
                    detail: "Failed to delete the task due to a network error.",
                },
            };
        }
    },
};