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

        public ProgressBar(int maxVal, int sizeMultiplier = 1) 
        {
            this.maxVal = maxVal * sizeMultiplier;
            progress = new Progress<int>(cnt =>
            {
                int currentProgress = int.Parse(cnt.ToString()) * sizeMultiplier;
                DisplayProgress(currentProgress);

                if (currentProgress >= maxVal)
                {
                    currentProgress = 0;
                }
            });

            currVal = 0;
            progress.Report(currVal);
        }

        private void DisplayProgress(int currentProgress)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write("Progress: [{0}{1}]", new string('o', currentProgress), new string(' ', maxVal - currentProgress));
        }

        public async Task UpdateProgress(int currVal)
        {
            progress.Report(currVal);
            await Task.Delay(20);
        }

        public async Task ResetProgress()
        {
            currVal = 0;
            await Task.Delay(20);
        } 
    }
}
