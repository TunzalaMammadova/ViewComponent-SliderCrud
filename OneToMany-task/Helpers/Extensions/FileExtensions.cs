using System;
namespace OneToMany_task.Helpers.Extensions
{
	public static class FileExtensions
	{
		public static bool CheckFileSize(this IFormFile file, int size)
		{
			return file.Length > size;
		}

		public static bool CheckFileType(this IFormFile file, string pattern)
		{
			return file.ContentType.Contains(pattern);
		}

        public async static Task SavFileToLocalAsync(this IFormFile file, string path)
		{
			using(FileStream stream = new FileStream(path, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}
		}

    }
}

