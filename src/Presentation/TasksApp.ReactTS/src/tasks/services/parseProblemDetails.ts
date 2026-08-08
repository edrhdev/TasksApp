import type { CustomProblemDetails } from "../types/CustomProblemDetails";

/**
 * Safely parses RFC 7807 ProblemDetails from an HTTP error response.
 */
export async function parseProblemDetails(response: Response): Promise<CustomProblemDetails> {
    try {
        const data = await response.json();
        return {
            type: data.type || "Error",
            title: data.title || "Unexpected Error",
            status: data.status || response.status,
            detail: data.detail || "An unexpected error occurred on the server.",
            instance: data.instance,
        };
    } catch {
        return {
            title: "Response Parsing Error",
            status: response.status,
            detail: "Failed to parse the server error response.",
        };
    }
}
