namespace ForgeAndFable.Models;

/// <summary>
/// A reference to a GitHub account the site surfaces — pairing the account login
/// with a short human label (e.g. "Personal", "Work"). Consumed by the profile
/// stats panel to know which accounts to fetch and how to caption each card.
/// </summary>
/// <param name="Label">Short caption shown on the card, e.g. "Personal".</param>
/// <param name="Username">The GitHub account login, e.g. "Broomfields".</param>
public record GitHubAccountRef(string Label, string Username);
