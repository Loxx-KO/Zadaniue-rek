using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadanieRek.Other
{
    public class ProgressBar
    {
        private int maxVal;
        private int currVal;
        private IProgress<int> progress;
        int x;
        int y;

        public ProgressBar(int maxVal) 
        {
            this.maxVal = maxVal;

            x = Console.CursorLeft;
            y = Console.CursorTop;
            Console.CursorTop = Console.WindowTop + Console.WindowHeight - 1;
            Console.SetCursorPosition(x, y);

            progress = new Progress<int>(cnt =>
            {
                int currentProgress = int.Parse(cnt.ToString());
                DisplayProgress(currentProgress);

                if (currentProgress >= maxVal)
                {
                    currentProgress = 0;
                    Console.WriteLine();
                }
            });

            currVal = 0;
            progress.Report(currVal);
        }

        private void DisplayProgress(int currentProgress)
        {
            currVal = currentProgress;
            int progress = (currVal / maxVal) * 100;
            Console.SetCursorPosition(x, y);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(x, y);
            Console.Write("Progress: [{0}{1}] {2}%", new string('+', progress/10), new string('=', ((maxVal - currVal)/maxVal)*10), progress);
        }

        public async Task UpdateProgress(int currVal)
        {
            this.currVal = currVal;
            progress.Report(this.currVal);
            await Task.Delay(20);
        }

        public async Task ResetProgress()
        {
            currVal = 0;
            await Task.Delay(20);
        } 
    }
}
