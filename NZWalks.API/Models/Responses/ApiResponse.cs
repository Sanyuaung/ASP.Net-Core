using System.Text.Json.Serialization;

namespace NZWalks.API.Models.Responses
{
    #region Pagination
    public class Pagination
    {
        [JsonPropertyName("current_page")] public int CurrentPage { get; set; }
        [JsonPropertyName("next_page")] public int? NextPage { get; set; }
        [JsonPropertyName("prev_page")] public int? PrevPage { get; set; }
        [JsonPropertyName("total_pages")] public int TotalPages { get; set; }
        [JsonPropertyName("total_record")] public int TotalRecord { get; set; }
    }
    #endregion

    #region ApiResponse
    public class ApiResponse
    {
        [JsonPropertyName("data")] public object? Data { get; set; }

        [JsonPropertyName("pagination")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Pagination? Pagination { get; set; }

        #region Lists
        // Return envelope with only data (no pagination)
        public static ApiResponse Lists(object value)
        {
            return new ApiResponse
            {
                Data = value
            };
        }
        #endregion
        #region WithPagination
        public static ApiResponse WithPagination(object items, int pageNumber, int pageSize, int totalCount)
        {
            var totalPages = pageSize <= 0 ? 0 : (int)Math.Ceiling((double)totalCount / pageSize);

            return new ApiResponse
            {
                Data = items,
                Pagination = new Pagination
                {
                    CurrentPage = pageNumber,
                    NextPage = pageNumber < totalPages ? pageNumber + 1 : null,
                    PrevPage = pageNumber > 1 ? pageNumber - 1 : null,
                    TotalPages = totalPages,
                    TotalRecord = totalCount
                }
            };
        }
        #endregion
    }
    #endregion
}
