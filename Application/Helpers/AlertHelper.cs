using Domain.Responses;
using System.Text.Json;

namespace Application.Helpers
{
    public static class AlertHelper
    {
        public static string ToTempData(this AlertResponse alertResponse)
            => JsonSerializer.Serialize(alertResponse);

        public static AlertResponse FromTempData(this Object tempData)
            => JsonSerializer.Deserialize<AlertResponse>(tempData?.ToString());
    }
}
