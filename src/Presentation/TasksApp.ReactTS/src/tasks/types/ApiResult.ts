import type { CustomProblemDetails } from "./CustomProblemDetails";

export interface ApiResult {
    isSuccess: boolean;
    error?: CustomProblemDetails;
}

export interface ApiResultData<T> extends ApiResult {
    data?: T;
}