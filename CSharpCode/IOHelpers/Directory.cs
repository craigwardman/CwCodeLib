namespace CwCodeLib.IOHelpers
{
    internal class Directory
    {
        /// <summary>
        /// Ensures that all directories leading up to a file path exist. Call this method with or without a filename (method uses every directory up to the last "\")
        /// </summary>
        /// <param name="targetpath">Folder or file path (for folder, include a trailing "\")</param>
        public static void EnsureDirectories(string targetpath)
        {
            // swap the UNC backslashes
            targetpath = targetpath.Replace("/", "\\");
            if (targetpath.StartsWith("\\\\"))
            {
                targetpath = ("//" + targetpath.Substring(2, targetpath.Length - 2));
            }

            // ensure there is no filename in the path
            string[] pathParts = targetpath.Split('\\');
            if (!string.IsNullOrEmpty(System.IO.Path.GetFileName(targetpath)))
            {
                pathParts[pathParts.Length - 1] = "";
            }

            // make sure that every directory in the path exists
            string currentPath = "";
            for (int i = 0; i <= pathParts.Length - 1; i++)
            {
                if (pathParts[i] != "")
                {
                    pathParts[i] = pathParts[i].Replace("/", "\\");
                    currentPath = System.IO.Path.Combine(currentPath, pathParts[i]);
                    if (!pathParts[i].StartsWith("\\\\") && !System.IO.Directory.Exists(currentPath))
                    {
                        System.IO.Directory.CreateDirectory(currentPath);
                    }
                }
            }
        }

        public static string GetAvailableFilename(string rootFolder, string targetFilename)
        {
            string finalFilename = targetFilename;
            int ctr = 0;
            while (System.IO.File.Exists(System.IO.Path.Combine(rootFolder, finalFilename)))
            {
                ctr++;
                finalFilename = System.IO.Path.GetFileNameWithoutExtension(targetFilename) + "_"
                            + ctr + System.IO.Path.GetExtension(targetFilename);
            }

            return finalFilename;
        }
    }
}