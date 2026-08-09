namespace ForgeAndFable.Models;

/// <summary>
/// A year of public contribution activity for a GitHub account, scraped from the
/// public contributions calendar at <c>github.com/users/{login}/contributions</c>
/// (the same HTML fragment GitHub renders on the profile page). Cached in-memory
/// by <see cref="Services.GitHubService"/>.
///
/// This calendar is not exposed by the REST API — only via GraphQL (which needs a
/// token) or this public HTML endpoint. We use the latter to avoid holding a secret.
/// </summary>
public class GitHubContributions
{
    /// <summary>The account login the calendar belongs to.</summary>
    public string Login { get; set; } = string.Empty;

    /// <summary>Sum of contributions across every day in <see cref="Days"/>.</summary>
    public int TotalContributions { get; set; }

    /// <summary>
    /// Every day in the calendar, oldest first. GitHub returns a fixed ~53-week
    /// window (usually 365–371 days). Empty when the fetch or parse fails.
    /// </summary>
    public List<GitHubContributionDay> Days { get; set; } = new();
}

/// <summary>A single day cell in a <see cref="GitHubContributions"/> calendar.</summary>
public class GitHubContributionDay
{
    /// <summary>The calendar date of this cell.</summary>
    public DateOnly Date { get; set; }

    /// <summary>Contribution count for the day.</summary>
    public int Count { get; set; }

    /// <summary>GitHub's own intensity bucket, 0 (none) to 4 (most), driving cell colour.</summary>
    public int Level { get; set; }
}
