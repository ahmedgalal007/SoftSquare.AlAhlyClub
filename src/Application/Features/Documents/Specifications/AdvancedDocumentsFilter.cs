using SoftSquare.AlAhlyClub.Application.Common.Security;

namespace SoftSquare.AlAhlyClub.Application.Features.Documents.Specifications;

public enum DocumentListView
{
    [Description("All")] All,
    [Description("My Document")] My,
    [Description("Created Toady")] CreatedToday,

    [Description("Created within the last 30 days")]
    Created30Days
}

public class AdvancedDocumentsFilter : PaginationFilter
{
    public DocumentListView ListView { get; set; } = DocumentListView.All;
    public UserProfile? CurrentUser { get; set; }
}