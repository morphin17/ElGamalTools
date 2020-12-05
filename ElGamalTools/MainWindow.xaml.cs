using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Core.utils;
using Core.data;
using System.Numerics;
using System.ComponentModel;
using Microsoft.Win32;
using System.IO;

namespace ElGamalTools
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        About a;
        List<PairPG> pairPGs;
        PairPG pairPG;
        ElGamal elGamal;
        BackgroundWorker backgroundWorker = new BackgroundWorker();


        public MainWindow()
        {
            InitializeComponent();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 0:
                    {
                        BigInteger p = (BigInteger)e.UserState;
                        pCandidate.Text = Utils.BigIntegerToHexString(p);
                    }
                    break;
                case 1:
                    {
                        BigInteger n = (BigInteger)e.UserState;
                        gCandidate.Text = Utils.BigIntegerToHexString(n);
                    }
                    break;
                case 2:
                    {
                        pairPG = (PairPG) e.UserState;
                        SetPairPG();
                        StopGenerate();
                    }
                    break;
            }

        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            
            PairPG pairPG = new PairPG(length: Convert.ToInt16(e.Argument) / 8, worker: backgroundWorker);
            if (!backgroundWorker.CancellationPending)
            {
                backgroundWorker.ReportProgress(2, pairPG);
            }
            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            a = new About();
            a.ShowDialog();
        }

        private void keySize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (pairPGs != null)
            {
                int length = Convert.ToInt16(e.NewValue);
                pairPG = GetPairPG(length);
                SetPairPG();
            }
            
        }

        private void GeneratePairPG_Click(object sender, RoutedEventArgs e)
        {
            if (!backgroundWorker.IsBusy)
            {
                backgroundWorker.RunWorkerAsync(Convert.ToInt16(keySize.Text));
                StartGenerate();
            }
            else
            {
                backgroundWorker.CancelAsync();
                StopGenerate();
            }
            

        }
        
        private void StopGenerate()
        {
            GeneratePairPG.Content = "Сгенерировать пару";
            GeneratePairPG.ToolTip = "Пересчитать P и G. Это может занять некоторое время.";
            GenerateLoading.IsIndeterminate = false;
        }

        private void StartGenerate()
        {
            GeneratePairPG.Content = "Остановить генерацию";
            GeneratePairPG.ToolTip = null;
            GenerateLoading.IsIndeterminate = true;

        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            pairPGs = Utils.GetPairPGs();
            int length = Convert.ToInt16(keySize.Text);
            pairPG = GetPairPG(length);
            SetPairPG();
        }

        private void SetPairPG()
        {
            pValue.Text = Utils.BigIntegerToHexString(pairPG.prime);
            gValue.Text = Utils.BigIntegerToHexString(pairPG.generator);
        }

        private void SetKeys()
        {
            privateKeyValue.Text = Utils.BigIntegerToHexString(elGamal.privateK.x);
            publicKeyValue.Text = Utils.BigIntegerToHexString(elGamal.publicK.h);
        }

        private PairPG GetPairPG(int length)
        {
            return pairPGs[length / 8 - 8];
        }

        private void GenerateKeys_Click(object sender, RoutedEventArgs e)
        {
            elGamal = new ElGamal(pairPG);
            SetKeys();
        }

        private string showFileBrowser()
        {
            // Create OpenFileDialog 
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                return dialog.SelectedPath;
            }
           
        }

        private void SavePrivateKey_Click(object sender, RoutedEventArgs e)
        {
            if (elGamal == null || elGamal.privateK == null)
            {
                MessageBox.Show("Приватный ключ не задан.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Private key (*.pk)|*.pk";
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllBytes(saveFileDialog.FileName, Ser.GetBytes(elGamal.privateK));
        }

        private void OpenPrivateKey_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Private key (*.pk)|*.pk";
            if (openFileDialog.ShowDialog() == true)
            {
                elGamal = new ElGamal(privateKey: Des.toPrivateKey(File.ReadAllBytes(openFileDialog.FileName)), publicKey: elGamal.publicK);
                SetKeys();
            }
                
        }

        private void SavePublicKey_Click(object sender, RoutedEventArgs e)
        {
            if (elGamal == null  || elGamal.publicK == null)
            {
                MessageBox.Show("Публичный ключ не задан.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Public key (*.pubk)|*.pubk";
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllBytes(saveFileDialog.FileName, Ser.GetBytes(elGamal.publicK));
        }

        private void OpenPublicKey_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Public key (*.pubk)|*.pubk";
            if (openFileDialog.ShowDialog() == true)
            {
                elGamal = new ElGamal(publicKey: Des.toPublicKey(File.ReadAllBytes(openFileDialog.FileName)), privateKey: elGamal.privateK);
                SetKeys();
            }
                
        }
    }
}
