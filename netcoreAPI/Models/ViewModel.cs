namespace netcoreAPI.Models
{
    public record AuthRequest
    {
        public required string? Username { get; set; }

        public required string? Password { get; set; }
    }

    public record AuthRespModel(int UserId, string Token);

    public record ApiInfoModel(string Author, string Version);
}
