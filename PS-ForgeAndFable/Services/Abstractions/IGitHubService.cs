using ForgeAndFable.Models;

namespace ForgeAndFable.Services.Abstractions;

/// <summary>
/// Abstraction over <see cref="GitHubService"/> consumed by Razor components.
///
/// Decouples component code from the concrete service implementation, making
/// it straightforward to substitute a test double in unit/component tests.
/// </summary>
public interface IGitHubService
{
    /// <summary>
    /// Fetches (and caches) public repository metadata from the GitHub REST API.
    /// Returns null if the URL is empty, unparseable, or the API call fails.
    /// </summary>
    Task<GitHubRepoStats?> FetchRepoStatsAsync(string? repoUrl);

    /// <summary>
    /// Fetches (and caches) a year of public contribution activity for the given
    /// login, scraped from GitHub's public contributions calendar HTML.
    /// Returns null if the login is empty or the fetch/parse fails.
    /// </summary>
    Task<GitHubContributions?> FetchContributionsAsync(string? username);
}
