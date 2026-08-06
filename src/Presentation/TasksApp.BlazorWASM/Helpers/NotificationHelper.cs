using Radzen;
using TasksApp.BlazorWASM.Models;

namespace TasksApp.BlazorWASM.Helpers;

public static class NotificationHelper
{
    public static NotificationMessage BuildFromApiResult(ApiResult result, string successSummary, string successDetail)
    {
        if (result.IsSuccess)
        {
            return new NotificationMessage
            {
                Severity = NotificationSeverity.Success,
                Summary = successSummary,
                Detail = successDetail,
                Duration = 4000
            };
        }

        var error = result.Error;
        var summary = !string.IsNullOrWhiteSpace(error?.Title) ? error.Title : "Error";

        var detailParts = new List<string>();

        if (error?.Status.HasValue == true)
            detailParts.Add($"[Status {error.Status}]");

        if (!string.IsNullOrWhiteSpace(error?.Detail))
            detailParts.Add(error.Detail);
        else
            detailParts.Add("There was an error processing the request.");

        if (!string.IsNullOrWhiteSpace(error?.Instance))
            detailParts.Add($"Path: {error.Instance}");

        return new NotificationMessage
        {
            Severity = NotificationSeverity.Error,
            Summary = summary,
            Detail = string.Join(" - ", detailParts),
            Duration = 6000
        };
    }
}