namespace Glue.Controllers
{
    public class FileHelper
    {
        private static readonly string uploadFolder = "uploads";
        private static readonly int maxFileSizeInBytes = 10 * 1024 * 1024; // 10 MB大小限制

        private readonly IHttpContextAccessor _httpContextAccessor;
        public FileHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        private string AddHostToFileUrl(string filePath)
        {
            // Get the server's base URL
            string baseUrl = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host;

            // Generate the full URL for the uploaded file
            string fileUrl = baseUrl + filePath;
            return fileUrl;
        }
        public async Task<string> SaveFileAsync(IFormFile file, string uploadRoot = "wwwroot")
        {
            try
            {
                if(file.Length > 0)
                {
                    if (!file.ContentType.StartsWith("image/"))
                    {
                        throw new Exception("只允许上传图片文件");
                    }
                    if (file.Length > maxFileSizeInBytes)
                    {
                        throw new Exception("文件大小超过允许的限制");
                    }
                    DateTime currentDate = DateTime.Now;
                    // Create a folder path based on the current date
                    string dateFolder = Path.Combine(uploadRoot, uploadFolder, currentDate.ToString("yyyy/MM/dd"));
                    // Create the date folder if it doesn't exist
                    if (!Directory.Exists(dateFolder))
                    {
                        Directory.CreateDirectory(dateFolder);
                    }
                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    // Calculate the file path within the specified uploadFolder
                    string filePath = Path.Combine(dateFolder, uniqueFileName);

                    // Save the file to the server
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Return the file path (or URL) for the client
                    string relativePath = AddHostToFileUrl(filePath.Replace(uploadRoot, ""));

                    return relativePath;
                }
                else
                {
                    throw new Exception("No file uploaded");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<string>> SaveImagesAsync(List<IFormFile> files, string uploadRoot = "wwwroot")
        {
            Dictionary<int, Task<string>> saveTasks = new Dictionary<int, Task<string>>();
            int index = 0;

            foreach (var file in files)
            {
                Task<string> saveTask = SaveFileAsync(file);
                saveTasks.Add(index++, saveTask);
            }

            await Task.WhenAll(saveTasks.Values);

            List<string> filePaths = saveTasks.OrderBy(kv => kv.Key).Select(kv => kv.Value.Result).ToList();

            return filePaths;
        }
        
    }
}
