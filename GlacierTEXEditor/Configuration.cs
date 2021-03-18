using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.IO.Compression;

namespace GlacierTEXEditor
{
    class Configuration
    {
        public bool CheckIfGamePathEntryAdded()
        {
            try
            {
                List<string> lines = File.ReadAllLines("GlacierTEXEditor.ini").ToList();
                string line = lines.Where(l => l.StartsWith("GamePath")).FirstOrDefault();

                return line != null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        public string GetGamePath()
        {
            try
            {
                List<string> lines = File.ReadAllLines("GlacierTEXEditor.ini").ToList();
                string line = lines.Where(l => l.StartsWith("GamePath")).FirstOrDefault();

                if (line != null)
                {
                    string path = line.Substring(line.IndexOf('=') + 1);

                    if (Directory.Exists(path))
                    {
                        return path;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return null;
        }

        public void WriteGamePath(string path)
        {
            try
            {
                List<string> lines = File.ReadAllLines("GlacierTEXEditor.ini").ToList();
                string line = lines.Where(l => l.StartsWith("GamePath")).FirstOrDefault();

                if (line != null)
                {
                    string currentPath = line.Substring(line.IndexOf('=') + 1);
                    string newPath;

                    if (currentPath.Equals(""))
                    {
                        newPath = line + path;
                    }
                    else
                    {
                        newPath = line.Replace(line.Substring(line.IndexOf('=') + 1), path);
                    }

                    int index = lines.FindIndex(l => l.StartsWith("GamePath"));
                    lines[index] = newPath;
                }
                else
                {
                    lines.Add("GamePath=" + path);
                }

                File.WriteAllLines("GlacierTEXEditor.ini", lines);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public List<string> GetRecentFiles()
        {
            List<string> recentFiles = new List<string>();
            List<string> lines = File.ReadAllLines("GlacierTEXEditor.ini").ToList();
            int index = lines.FindIndex(l => l.StartsWith("RecentFiles"));

            if (index != -1)
            {
                for (int i = index + 1; i < lines.Count; i++)
                {
                    if (File.Exists(lines[i]))
                    {
                        recentFiles.Add(lines[i]);
                    }
                }

                List<string> distinctItems = recentFiles.Select(s => s).Distinct().ToList();

                for (int i = 0; i < recentFiles.Count; i++)
                {
                    if (!distinctItems.Contains(recentFiles[i]))
                    {
                        recentFiles[i] = Path.GetFileName(recentFiles[i]);
                    }
                }
            }

            return recentFiles;
        }

        public void ClearRecentFilesFromINI()
        {
            List<string> lines = File.ReadAllLines("GlacierTEXEditor.ini").ToList();
            int index = lines.FindIndex(l => l.StartsWith("RecentFiles"));

            int i = index + 1;
            int count = 0;

            while (i < lines.Count)
            {
                count++;
                i++;
            }

            lines.RemoveRange(index + 1, count);
            File.WriteAllLines("GlacierTEXEditor.ini", lines);
        }

        public void WriteRecentFilePath(string path)
        {
            List<string> lines = File.ReadAllLines("GlacierTEXEditor.ini").ToList();
            int index = lines.FindIndex(l => l.StartsWith("RecentFiles"));
            bool pathAlreadyAdded = lines.Where(l => l.Equals(path)).Count() > 0;

            using (StreamWriter streamWriter = new StreamWriter("GlacierTEXEditor.ini", true))
            {
                if (index == -1)
                {
                    streamWriter.WriteLine("RecentFiles");
                }

                if (!pathAlreadyAdded)
                {
                    streamWriter.WriteLine(path);
                }
            }
        }

        public int GetRecentFilesCount()
        {
            List<string> lines = File.ReadAllLines("GlacierTEXEditor.ini").ToList();
            int index = lines.FindIndex(l => l.StartsWith("RecentFiles"));
            int count = 0;

            if (index != -1)
            {
                for (int i = index + 1; i < lines.Count; i++)
                {
                    if (File.Exists(lines[i]))
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public void WriteCheckBoxesState(IEnumerable<CheckBox> checkedBoxes)
        {
            List<string> lines = File.ReadAllLines("GlacierTEXEditor.ini").ToList();
            int index = lines.FindIndex(l => l.StartsWith("CheckBoxes"));

            if (index == -1)
            {
                using (StreamWriter streamWriter = new StreamWriter("GlacierTEXEditor.ini", true))
                {
                    streamWriter.WriteLine("CheckBoxes");

                    foreach (CheckBox checkBox in checkedBoxes)
                    {
                        streamWriter.WriteLine(checkBox.Name + "=" + checkBox.Checked);
                    }
                }
            }
            else
            {
                int n = 0;
                int i = index + 1;

                while (!lines[i].StartsWith("CompressionLevel"))
                {
                    lines[i] = lines[i].Replace(lines[i].Substring(lines[i].IndexOf('=') + 1), checkedBoxes.ElementAt(n++).Checked.ToString());
                    i++;
                }

                File.WriteAllLines("GlacierTEXEditor.ini", lines);
            }
        }

        public CompressionLevel GetCompressionLevel()
        {
            CompressionLevel compressionLevel = CompressionLevel.Fastest;

            try
            {
                List<string> lines = File.ReadAllLines("GlacierTEXEditor.ini").ToList();
                string line = lines.Where(l => l.StartsWith("CompressionLevel")).FirstOrDefault();

                if (line != null)
                {
                    string level = line.Substring(line.IndexOf('=') + 1);

                    if (level.Equals("Optimal"))
                    {
                        compressionLevel = CompressionLevel.Optimal;
                    }
                    else if (level.Equals("Fastest"))
                    {
                        compressionLevel = CompressionLevel.Fastest;
                    }

                    return compressionLevel;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return compressionLevel;
        }

        public void WriteCompressionLevel(string compressionLevel)
        {
            try
            {
                List<string> lines = File.ReadAllLines("GlacierTEXEditor.ini").ToList();
                string line = lines.Where(l => l.StartsWith("CompressionLevel")).FirstOrDefault();

                if (line != null)
                {
                    string newCompressionLevel = line.Replace(line.Substring(line.IndexOf('=') + 1), compressionLevel);

                    int index = lines.FindIndex(l => l.StartsWith("CompressionLevel"));
                    lines[index] = newCompressionLevel;
                }
                else
                {
                    lines.Add("CompressionLevel=" + compressionLevel);
                }

                File.WriteAllLines("GlacierTEXEditor.ini", lines);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool CheckIfCompressionLvlExists()
        {
            try
            {
                List<string> lines = File.ReadAllLines("GlacierTEXEditor.ini").ToList();
                string line = lines.Where(l => l.StartsWith("CompressionLevel")).FirstOrDefault();

                return line != null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        public bool CheckIfAutoUpdateZIPExists()
        {
            try
            {
                List<string> lines = File.ReadAllLines("GlacierTEXEditor.ini").ToList();
                string line = lines.Where(l => l.StartsWith("AutoUpdateZIP")).FirstOrDefault();

                return line != null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        public bool GetAutoUpdateZIPState()
        {
            try
            {
                List<string> lines = File.ReadAllLines("GlacierTEXEditor.ini").ToList();
                string line = lines.Where(l => l.StartsWith("AutoUpdateZIP")).FirstOrDefault();

                if (line != null)
                {
                    string autoUpdateZIPState = line.Substring(line.IndexOf('=') + 1);

                    if (autoUpdateZIPState.Equals("True"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return true;
        }

        public void SetAutoUpdateZIP(bool autoUpdateZIP)
        {
            try
            {
                List<string> lines = File.ReadAllLines("GlacierTEXEditor.ini").ToList();
                string line = lines.Where(l => l.StartsWith("AutoUpdateZIP")).FirstOrDefault();

                if (line != null)
                {
                    string newLine = line.Replace(line.Substring(line.IndexOf('=') + 1), autoUpdateZIP.ToString());

                    int index = lines.FindIndex(l => l.StartsWith("AutoUpdateZIP"));
                    lines[index] = newLine;
                }
                else
                {
                    lines.Add("AutoUpdateZIP=" + autoUpdateZIP.ToString());
                }

                File.WriteAllLines("GlacierTEXEditor.ini", lines);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public GameVersion GetGameVersion()
        {
            GameVersion gameVersion = GameVersion.PC;

            try
            {
                List<string> lines = File.ReadAllLines("GlacierTEXEditor.ini").ToList();
                string line = lines.Where(l => l.StartsWith("GameVersion")).FirstOrDefault();

                if (line != null)
                {
                    string version = line.Substring(line.IndexOf('=') + 1);

                    if (version.Equals("PC"))
                    {
                        gameVersion = GameVersion.PC;
                    }
                    else if (version.Equals("PS2"))
                    {
                        gameVersion = GameVersion.PS2;
                    }
                    else if (version.Equals("PS3"))
                    {
                        gameVersion = GameVersion.PS3;
                    }
                    else if (version.Equals("PS4"))
                    {
                        gameVersion = GameVersion.PS4;
                    }
                    else if (version.Equals("XBOX"))
                    {
                        gameVersion = GameVersion.XBOX;
                    }

                    return gameVersion;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return gameVersion;
        }

        public void WriteGameVersion(string gameVersion)
        {
            try
            {
                List<string> lines = File.ReadAllLines("GlacierTEXEditor.ini").ToList();
                string line = lines.Where(l => l.StartsWith("GameVersion")).FirstOrDefault();

                if (line != null)
                {
                    string newGameVersion = line.Replace(line.Substring(line.IndexOf('=') + 1), gameVersion);

                    int index = lines.FindIndex(l => l.StartsWith("GameVersion"));
                    lines[index] = newGameVersion;
                }
                else
                {
                    lines.Add("GameVersion=" + gameVersion);
                }

                File.WriteAllLines("GlacierTEXEditor.ini", lines);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool CheckIfGameVersionExists()
        {
            try
            {
                List<string> lines = File.ReadAllLines("GlacierTEXEditor.ini").ToList();
                string line = lines.Where(l => l.StartsWith("GameVersion")).FirstOrDefault();

                return line != null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Glacier TEX Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }
    }
}
