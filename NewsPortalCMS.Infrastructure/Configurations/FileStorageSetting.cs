namespace NewsPortalCMS.Shared.Configuration
{
    public class FileStorageSettings
    {
        public string RootPath { get; set; } = "wwwroot";

        public long MaxImageSize { get; set; } =
            10 * 1024 * 1024;

        // Maximum video size = 1 GB
        public long MaxVideoSize { get; set; } =
            1L * 1024 * 1024 * 1024;

        public long MaxDocumentSize { get; set; } =
            20 * 1024 * 1024;
    }
}