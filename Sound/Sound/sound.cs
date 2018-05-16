using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sound
{
    class sound
    {
        static int num_songs,sizeFolder;
        static int _num_songs, choice;
        static string Source, Destination;
        static List<song> data;
        static int[,] dp;
        static Dictionary<string, List<song>> totalData;
        public sound(int _choice, string _Source, string _Destination,int _sizeFolder)
        {
            choice = _choice;
            Source = _Source;
            Destination = _Destination;
            sizeFolder = _sizeFolder;
            totalData = new Dictionary<string, List<song>>();
            data = new List<song>();
        }
        public void Run()
        {
            ReadFile();
            if (choice == 0)
            {
                WorstfitAlgorithmUsing__LinearSearch(sizeFolder);
            }
            else if (choice == 1)
            {
                WorstfitAlgorithmUsing__PriorityQueue(sizeFolder);
            }
            else if (choice == 2)
            {
                WorstFitDecreasingAlgorithmusing__LinearSearch(sizeFolder);
            }
            else if (choice == 3)
            {
                WorstFitDecreasingAlgorithmusing__PriorityQueue(sizeFolder);
            }
            else if (choice == 4)
            {
                FirstFitDecreasingAlgorithmUsing__LinearSearch(sizeFolder);
            }
            else if (choice == 5)
            {
                FolderFillingAlgorithm(sizeFolder);
            }
            MessageBox.Show("Finish");
        }
        void ReadFile()
        {
            string path = Source + "\\AudiosInfo.txt";
            string[] lines = System.IO.File.ReadLines(path).ToArray();
            int len = lines.Length;
            data = new List<song>();
            // get number of songs
            num_songs = int.Parse(lines[0]);
            for (int i = 1; i < len; i++)
            {
                string[] fileInfo = lines[i].Split(' ');
                data.Add(new song(fileInfo[0], lines[i], (int)TimeSpan.Parse(fileInfo[1]).TotalSeconds));
            }

        }
        /******************************************************/
        public void WorstfitAlgorithmUsing__LinearSearch(int sizeFolder)//O(N^2)
        {
            int[] createFolders = new int[num_songs + 2];
            int folder = 0;
            for (int i = 1; i <= num_songs; i++) // O(N)
            {
                createFolders[i] = sizeFolder;
            }
            for (int i = 0; i < num_songs; i++) //O(N^2)
            {
                int mostRem = 0;
                int index = -1;
                for (int j = 1; j <= num_songs; j++) //O(N)
                {
                    if (createFolders[j] != sizeFolder && data.ElementAt(i).duration <= createFolders[j])
                    {

                        if (createFolders[j] - data.ElementAt(i).duration >= mostRem) // to get the most remaining duration
                        {
                            mostRem = createFolders[j] - data.ElementAt(i).duration;
                            index = j;
                        }
                    }
                }
                if (index == -1) // ml2ash file mafto7
                {
                    folder++;
                    createFolders[folder] -= data.ElementAt(i).duration;
                    if (totalData.ContainsKey("F" + folder) == false)
                    {
                        totalData.Add("F" + folder, new List<song>());
                    }
                    totalData["F" + folder].Add(data.ElementAt(i));
                }
                else
                {
                    createFolders[index] -= data.ElementAt(i).duration;
                    if (totalData.ContainsKey("F" + index) == false)
                    {
                        totalData.Add("F" + index, new List<song>());
                    }
                    totalData["F" + index].Add(data.ElementAt(i));
                }
            }
            Copy_Songs("[1] WorstFit", folder);
        }
        public void WorstFitDecreasingAlgorithmusing__LinearSearch(int sizeFolder) //O(N^2)
        {
            data.Sort(delegate (song f1, song f2) { return f2.duration.CompareTo(f1.duration); });

            int[] createFolders = new int[num_songs + 2];
            int folder = 0;
            for (int i = 1; i <= num_songs; i++) // O(N)
            {
                createFolders[i] = sizeFolder;
            }
            for (int i = 0; i < num_songs; i++) //O(N^2)
            {
                int mostRem = 0;
                int index = -1;
                for (int j = 1; j <= num_songs; j++) //O(N)
                {
                    if (createFolders[j] != sizeFolder && data.ElementAt(i).duration <= createFolders[j])
                    {
                        if (createFolders[j] - data.ElementAt(i).duration >= mostRem) // to get the most remaining duration
                        {
                            mostRem = createFolders[j] - data.ElementAt(i).duration;
                            index = j;
                        }
                    }
                }
                if (index == -1) // ml2ash file mafto7
                {
                    folder++;
                    createFolders[folder] -= data.ElementAt(i).duration;
                    if (totalData.ContainsKey("F" + folder) == false)
                    {
                        totalData.Add("F" + folder, new List<song>());
                    }
                    totalData["F" + folder].Add(data.ElementAt(i));
                }
                else
                {
                    createFolders[index] -= data.ElementAt(i).duration;
                    if (totalData.ContainsKey("F" + index) == false)
                    {
                        totalData.Add("F" + index, new List<song>());
                    }
                    totalData["F" + index].Add(data.ElementAt(i));
                }
            }
            Copy_Songs("[2] WorstFit Decreasing", folder);
        }
        public void WorstfitAlgorithmUsing__PriorityQueue(int sizeFolder)// O(N log(N))
        {
            int folder = 0;
            int[] createFolders = new int[num_songs + 2];
            PriorityQueue<int, int> queue = new PriorityQueue<int, int>();
            for (int i = 1; i <= num_songs; i++) // O(N)
            {
                createFolders[i] = sizeFolder;
            }
            for (int i = 0; i < num_songs; i++) // O(N log(N))
            {
                if (queue.Count == 0)
                {
                    folder++;
                    createFolders[folder] -= data.ElementAt(i).duration;
                    /***********************************************************/
                    if (totalData.ContainsKey("F" + folder) == false)
                    {
                        totalData.Add("F" + folder, new List<song>());
                    }
                    totalData["F" + folder].Add(data.ElementAt(i));
                    /***********************************************************/
                    if (createFolders[folder] > 0)
                    {
                        queue.Add(new KeyValuePair<int, int>(-createFolders[folder], folder));
                    }
                }
                else
                {
                    KeyValuePair<int, int> mostRem = queue.Dequeue();
                    int mostRemSpace = -mostRem.Key;
                    if (mostRemSpace < data.ElementAt(i).duration)
                    {
                        queue.Add(mostRem);
                        folder++;
                        createFolders[folder] -= data.ElementAt(i).duration;
                        /************************************************************/
                        if (totalData.ContainsKey("F" + folder) == false)
                        {
                            totalData.Add(("F" + folder), new List<song>());
                        }
                        totalData["F" + folder].Add(data.ElementAt(i));
                        /************************************************************/
                        if (createFolders[folder] > 0)
                        {
                            queue.Add(new KeyValuePair<int, int>(-createFolders[folder], folder));
                        }
                    }
                    else
                    {
                        int mostRemFile = mostRem.Value;
                        createFolders[mostRemFile] -= data.ElementAt(i).duration;
                        /************************************************************/
                        if (totalData.ContainsKey("F" + mostRemFile) == false)
                        {
                            totalData.Add("F" + mostRemFile, new List<song>());
                        }
                        totalData["F" + mostRemFile].Add(data.ElementAt(i));
                        /************************************************************/
                        if (createFolders[mostRemFile] > 0)
                        {
                            queue.Add(new KeyValuePair<int, int>(-createFolders[mostRemFile], mostRemFile));
                        }
                    }
                }
            }
            Copy_Songs("[1] WorstFit", folder);

        }
        public void WorstFitDecreasingAlgorithmusing__PriorityQueue(int sizeFolder) // O(N log(N))
        {
            data.Sort(delegate (song f1, song f2) { return f2.duration.CompareTo(f1.duration); });

            int folder = 0;
            int[] createFolders = new int[num_songs + 2];
            PriorityQueue<int, int> queue = new PriorityQueue<int, int>();
            for (int i = 1; i <= num_songs; i++) // O(N)
            {
                createFolders[i] = sizeFolder;
            }
            for (int i = 0; i < num_songs; i++) // O(N log(N))
            {
                if (queue.Count == 0)
                {
                    folder++;
                    createFolders[folder] -= data.ElementAt(i).duration;
                    /***********************************************************/
                    if (totalData.ContainsKey("F" + folder) == false)
                    {
                        totalData.Add("F" + folder, new List<song>());
                    }
                    totalData["F" + folder].Add(data.ElementAt(i));
                    /***********************************************************/
                    if (createFolders[folder] > 0)
                    {
                        queue.Add(new KeyValuePair<int, int>(-createFolders[folder], folder));
                    }
                }
                else
                {
                    KeyValuePair<int, int> mostRem = queue.Dequeue();
                    int mostRemSpace = -mostRem.Key;
                    if (mostRemSpace < data.ElementAt(i).duration)
                    {
                        queue.Add(mostRem);
                        folder++;
                        createFolders[folder] -= data.ElementAt(i).duration;
                        /************************************************************/
                        if (totalData.ContainsKey("F" + folder) == false)
                        {
                            totalData.Add(("F" + folder), new List<song>());
                        }
                        totalData["F" + folder].Add(data.ElementAt(i));
                        /************************************************************/
                        if (createFolders[folder] > 0)
                        {
                            queue.Add(new KeyValuePair<int, int>(-createFolders[folder], folder));
                        }
                    }
                    else
                    {
                        int mostRemFile = mostRem.Value;
                        createFolders[mostRemFile] -= data.ElementAt(i).duration;
                        /************************************************************/
                        if (totalData.ContainsKey("F" + mostRemFile) == false)
                        {
                            totalData.Add("F" + mostRemFile, new List<song>());
                        }
                        totalData["F" + mostRemFile].Add(data.ElementAt(i));
                        /************************************************************/
                        if (createFolders[mostRemFile] > 0)
                        {
                            queue.Add(new KeyValuePair<int, int>(-createFolders[mostRemFile], mostRemFile));
                        }
                    }
                }
            }
            Copy_Songs("[2] WorstFit Decreasing", folder);
        }
        public void FirstFitDecreasingAlgorithmUsing__LinearSearch(int sizeFolder)
        {
            data.Sort(delegate (song f1, song f2) { return f2.duration.CompareTo(f1.duration); });

            int[] createFolders = new int[num_songs + 2];
            int folder = 0;
            for (int i = 0; i < num_songs; i++) // O(N)
            {
                createFolders[i] = sizeFolder;
            }
            for (int i = 0; i < num_songs; i++)
            {
                for (int j = 1; j <= num_songs; j++)
                {
                    if (data.ElementAt(i).duration <= createFolders[j])
                    {
                        if (createFolders[j] == sizeFolder) // first time to open this folder
                        {
                            folder++;
                        }
                        createFolders[j] -= data.ElementAt(i).duration;
                        /***************************************************************/
                        if (totalData.ContainsKey("F" + j) == false)
                        {
                            totalData.Add("F" + j, new List<song>());
                        }
                        totalData["F" + j].Add(data.ElementAt(i));
                        /***************************************************************/
                        break;
                    }
                }
            }
            Copy_Songs("[3] FirstFit Decreasing", folder);
        }
        /******************************************************/
        public void solve(int sizeFolder)
        {
            for (int i = 1; i <= num_songs; i++)
            {
                for (int j = 1; j <= sizeFolder; j++)
                {
                    dp[i, j] = Math.Max(dp[i, j], dp[i - 1, j]);
                    if (data.ElementAt(i-1).duration > 0 && data.ElementAt(i-1).duration <= j)
                    {
                        dp[i, j] = Math.Max(dp[i, j], dp[i - 1, j - data.ElementAt(i-1).duration] + data.ElementAt(i-1).duration);
                    }
                }
            }
        }
        public void build(int sizeFolder, int folder)
        {
            int i = num_songs, j = sizeFolder;
            while(i > 0 && j > 0)
            {
                if (data.ElementAt(i-1).duration > 0 && data.ElementAt(i-1).duration <= j)
                {
                    if (dp[i, j] == dp[i - 1, j - data.ElementAt(i-1).duration] + data.ElementAt(i-1).duration)
                    {
                        _num_songs--;
                        if (totalData.ContainsKey("F" + folder) == false)
                        {
                            totalData.Add("F" + folder, new List<song>());
                        }
                        totalData["F" + folder].Add(data.ElementAt(i-1));
                        j -= data.ElementAt(i-1).duration;
                        data.ElementAt(i-1).duration = 0;
                        i--;
                    }
                    else
                    {
                        i--;
                    }
                }
                else
                {
                    i--;
                }
            }
        }
        public void FolderFillingAlgorithm(int sizeFolder)
        {
            dp = new int[num_songs + 5, sizeFolder + 5];
            _num_songs = num_songs;
            int folder = 0;
            while (_num_songs > 0) // stop when all songs puts in folder
            {
                folder++;
                /******************************************************/
                for (int i = 0; i <= num_songs; i++)
                {
                    for (int j = 0; j <= sizeFolder; j++)
                    {
                        dp[i, j] = 0;
                    }
                }
                /******************************************************/
                solve(sizeFolder);
                build(sizeFolder, folder);
            }
            Copy_Songs("[4] FolderFilling", folder);
        }
        /******************************************************/
        static void Copy_Songs(String algo, int Number_Of_Folders)
        {
            String Destination_Path, Path;
            for (int Folder = 1; Folder <= Number_Of_Folders; Folder++)
            {
                Destination_Path = Destination + "\\F" + Folder;
                Path = Destination + "\\F" + Folder + "_METADATA.txt";
                Directory.CreateDirectory(Destination_Path);
                long sum = 0;
                using (StreamWriter sw = File.AppendText(Path))
                {
                    sw.WriteLine("F" + Folder);
                    foreach (song ssong in totalData["F" + Folder])
                    {
                        sum += ssong.duration;
                        sw.WriteLine(ssong.line);
                        String Source_Path = Source + "\\Audios\\" + ssong.name;
                        Copy_File(ssong.name, Source_Path, Destination_Path); // copy song to output path
                    }
                    TimeSpan Time = TimeSpan.FromSeconds(sum);
                    sw.WriteLine(Time.ToString(@"hh\:mm\:ss"));
                }
            }
        }
        static void Copy_File(String File_Name, String Source_Path, String Target_Path)
        {
            String Destination_File = Path.Combine(Target_Path, File_Name);
            if (!Directory.Exists(Target_Path))
            {
                Directory.CreateDirectory(Target_Path);
            }
            File.Copy(Source_Path, Destination_File, true);
            if (Directory.Exists(Source_Path))
            {
                String[] files = Directory.GetFiles(Source_Path);
                foreach (String Current_file in files)
                {
                    File_Name = Path.GetFileName(Current_file);
                    Destination_File = Path.Combine(Target_Path, File_Name);
                    File.Copy(Current_file, Destination_File, true);
                }
            }

        }
    }
}
