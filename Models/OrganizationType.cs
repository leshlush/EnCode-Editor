namespace SnapSaves.Models
{
    public enum OrganizationType
    {
        Default = 0,        // For the default seeded organization
        LtiIntegration = 1, // Organizations created via LTI
        SingleUser = 2,     // Organizations created for individual Google OAuth users
        AdminCreated = 3    // For future: Organizations created via admin panel
    }
}
