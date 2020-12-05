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
        BigInteger MCreateSign;
        BigInteger MCheckSign;
        Signature signCreated;
        Signature signOpened;
        bool signCreatedFlag = false;
        bool privateKeySave = false;
        bool publicKeySave = false;

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
            if (elGamal.privateK != null)
            {
                privateKeyValue.Text = Utils.BigIntegerToHexString(elGamal.privateK.x);
            }
            if (elGamal.publicK != null)
            {
                publicKeyValue.Text = Utils.BigIntegerToHexString(elGamal.publicK.h);
            }
        }

        private PairPG GetPairPG(int length)
        {
            return pairPGs[length / 8 - 8];
        }

        private bool saveKeysDialog()
        {
            if (signCreatedFlag )
            {
                MessageBoxResult dialogResult = MessageBox.Show("С помощью текущих ключей была создана подпись. Стоит сохранять ключи. Все ровно выполнить действие?", "Ключи", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.No)
                {
                    return true;
                }
                
            }

            signCreatedFlag = false;
            signCreated = null;
            rValue.Text = "";
            sValue.Text = "";
            return false;
        }

        private void GenerateKeys_Click(object sender, RoutedEventArgs e)
        {
            if (saveKeysDialog()) return;

            elGamal = new ElGamal(pairPG);
            SetKeys();
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
            if (saveFileDialog.ShowDialog() == true && saveFileDialog.FileName.Length != 0)
            {
                File.WriteAllBytes(saveFileDialog.FileName, Ser.GetBytes(elGamal.privateK));
                privateKeySave = true;
            }
                
        }

        private void OpenPrivateKey_Click(object sender, RoutedEventArgs e)
        {
            if (saveKeysDialog()) return;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Private key (*.pk)|*.pk";
            if (openFileDialog.ShowDialog() == true && openFileDialog.FileName.Length != 0)
            {
                if (elGamal == null)
                {
                    elGamal = new ElGamal(privateKey: Des.toPrivateKey(File.ReadAllBytes(openFileDialog.FileName)));
                }
                else
                {
                    elGamal = new ElGamal(privateKey: Des.toPrivateKey(File.ReadAllBytes(openFileDialog.FileName)), publicKey: elGamal.publicK);
                }
                
                SetKeys();
                privateKeySave = true;
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
            if (saveFileDialog.ShowDialog() == true && saveFileDialog.FileName.Length != 0)
            {
                File.WriteAllBytes(saveFileDialog.FileName, Ser.GetBytes(elGamal.publicK));
                publicKeySave = true;
            }
                
        }

        private void OpenPublicKey_Click(object sender, RoutedEventArgs e)
        {
            if (saveKeysDialog()) return;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Public key (*.pubk)|*.pubk";
            if (openFileDialog.ShowDialog() == true && openFileDialog.FileName.Length != 0)
            {
                if (elGamal == null)
                {
                    elGamal = new ElGamal(publicKey: Des.toPublicKey(File.ReadAllBytes(openFileDialog.FileName)));
                }
                else
                {
                    elGamal = new ElGamal(publicKey: Des.toPublicKey(File.ReadAllBytes(openFileDialog.FileName)), privateKey: elGamal.privateK);
                }
                
                SetKeys();
                publicKeySave = true;
            }
                
        }

        private void OpenFileBrowser_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true && openFileDialog.FileName.Length != 0)
            {
                MCreateSign = Core.utils.Encoder.EncodeByteArray(File.ReadAllBytes(openFileDialog.FileName));
                filePath.Text = openFileDialog.FileName;
            }
        }

        private void CreateSign_Click(object sender, RoutedEventArgs e)
        {
            if (elGamal == null || elGamal.privateK == null || elGamal.publicK == null)
            {
                MessageBox.Show("Сгенерируйте или откройте публичный и приватный ключ.");
                return;
            }

            if (MCreateSign == 0)
            {
                MessageBox.Show("Файл не открыт.");
                return;
            }

            signCreated = elGamal.Generate(MCreateSign);
            rValue.Text = Utils.BigIntegerToHexString(signCreated.r);
            sValue.Text = Utils.BigIntegerToHexString(signCreated.s);
           
        }


        private void SaveSign_Click(object sender, RoutedEventArgs e)
        {
            if (elGamal == null || elGamal.publicK == null)
            {
                MessageBox.Show("Публичный ключ не задан.");
                return;
            }

            if (signCreated == null)
            {
                MessageBox.Show("Электронная подпись не вычислена.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Signature (*.sign)|*.sign";
            if (saveFileDialog.ShowDialog() == true && saveFileDialog.FileName.Length != 0)
            {
                File.WriteAllBytes(saveFileDialog.FileName, Ser.GetBytes(signCreated));
                signCreatedFlag = true;
            }
                
        }

        private void OpenFileBrowserCheckSign_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true && openFileDialog.FileName.Length != 0)
            {
                MCheckSign = Core.utils.Encoder.EncodeByteArray(File.ReadAllBytes(openFileDialog.FileName));
                filePathCheckSign.Text = openFileDialog.FileName;
            }
        }

        private void OpenSignFileBrowserCheckSign_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Signature (*.sign)|*.sign";
            if (openFileDialog.ShowDialog() == true && openFileDialog.FileName.Length != 0)
            {
                signOpened = Des.toSignature(File.ReadAllBytes(openFileDialog.FileName));
                fileSignPathCheckSign.Text = openFileDialog.FileName;
            }
        }

        private void CheckSign_Click(object sender, RoutedEventArgs e)
        {
            if (elGamal == null || elGamal.publicK == null)
            {
                MessageBox.Show("Публичный ключ не задан.");
                return;
            }

            if (signOpened == null)
            {
                MessageBox.Show("Электронная подпись не открыта.");
                return;
            }

            if (MCheckSign == 0)
            {
                MessageBox.Show("Файл не открыт.");
                return;
            }

            if (elGamal.Verify(MCheckSign, signOpened))
            {
                MessageBox.Show("Подпись действительна");
            }
            else
            {
                MessageBox.Show("Подпись не действительна");
            }
        }
    }
}
