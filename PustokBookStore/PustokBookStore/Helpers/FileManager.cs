using PustokBookStore.Entities;

namespace PustokBookStore.Helpers
{
    public static class FileManager
    {
        public static string  Save(IFormFile file,string rootPath, string folder)
        {
            string newFileName = Guid.NewGuid().ToString() + (file.FileName.Length <= 64 ? file.FileName : file.FileName.Substring(file.FileName.Length - 64));
            var path = Path.Combine(rootPath, folder, newFileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return newFileName;
        }

        public static void Delete(string folder, string rootPath, string fileName)
        {
            var path = Path.Combine(folder, rootPath, fileName);

            if(File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
