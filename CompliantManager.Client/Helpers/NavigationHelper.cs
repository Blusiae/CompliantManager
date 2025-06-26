using CompliantManager.Shared.Enums;

namespace CompliantManager.Client.Helpers
{
    public static class NavigationHelper
    {
        public static string GetListModeQueryString(string baseQuery, ListMode listMode) => $"/{baseQuery}/{listMode.ToString().ToLower()}";
    }
}
