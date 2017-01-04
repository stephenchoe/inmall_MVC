using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Core.Extensions
{
    public static class FileExtentions
    {
        public static double DirectorySizeInMegabytes(this DirectoryInfo dInfo, bool includeSubDir)
        {
            long size = DirectorySize(dInfo, includeSubDir);
            if (size == 0) return 0;

            return ConvertBytesToMegabytes(size);
        }
        public static long DirectorySize(this DirectoryInfo dInfo, bool includeSubDir)
        {
            // Enumerate all the files
            long totalSize = dInfo.EnumerateFiles()
                         .Sum(file => file.Length);

            // If Subdirectories are to be included
            if (includeSubDir)
            {
                // Enumerate all sub-directories
                totalSize += dInfo.EnumerateDirectories()
                         .Sum(dir => DirectorySize(dir, true));
            }

            return totalSize;
        }
        public static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }


        // Returns the human-readable file size for an arbitrary, 64-bit file size
        //  The default format is "0.### XB", e.g. "4.2 KB" or "1.434 GB"
        public static string GetSizeReadable(this FileInfo fileInfo)
        {
            long i = fileInfo.Length;

            string sign = (i < 0 ? "-" : "");
            double readable = (i < 0 ? -i : i);
            string suffix;
            if (i >= 0x1000000000000000) // Exabyte
            {
                suffix = "EB";
                readable = (double)(i >>
                    50);
            }
            else if (i >= 0x4000000000000) // Petabyte
            {
                suffix = "PB";
                readable = (double)(i >> 40);
            }
            else if (i >= 0x10000000000) // Terabyte
            {
                suffix = "TB";
                readable = (double)(i >> 30);
            }
            else if (i >= 0x40000000) // Gigabyte
            {
                suffix = "GB";
                readable = (double)(i >> 20);
            }
            else if (i >= 0x100000) // Megabyte
            {
                suffix = "MB";
                readable = (double)(i >> 10);
            }
            else if (i >= 0x400) // Kilobyte
            {
                suffix = "KB";
                readable = (double)i;
            }
            else
            {
                return i.ToString(sign + "0 B"); // Byte
            }
            readable = readable / 1024;

            return sign + readable.ToString("0.## ") + suffix;
        }



    }
    
}
