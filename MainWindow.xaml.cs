using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FrHello.NetLib.Core.Security;
using Microsoft.Win32;
using MvvmCross.Commands;

namespace HashTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private CancellationTokenSource _lastCancellationTokenSource;

        private string _filePath;
        private string _inputString;

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath
        {
            get => _filePath;
            set
            {
                if (_filePath != value)
                {
                    _filePath = value;
                    OnPropertyChanged();
                    OnFilePathChanged();
                }
            }
        }

        /// <summary>
        /// 输入文本
        /// </summary>
        public string InputString
        {
            get => _inputString;
            set
            {
                if (_inputString != value)
                {
                    _inputString = value;
                    OnPropertyChanged();
                    OnInputStringChanged();
                }
            }
        }

        /// <summary>
        /// Md5
        /// </summary>
        public string Md5 { get; private set; }

        /// <summary>
        /// Sha1
        /// </summary>
        public string Sha1 { get; private set; }

        /// <summary>
        /// Sha256
        /// </summary>
        public string Sha256 { get; private set; }

        /// <summary>
        /// Sha384
        /// </summary>
        public string Sha384 { get; private set; }

        /// <summary>
        /// Sha512
        /// </summary>
        public string Sha512 { get; private set; }

        public MvxCommand FileSelectedCommand { get; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            FileSelectedCommand = new MvxCommand(FileSelectedCommandHandler);
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void FileSelectedCommandHandler()
        {
            var dialog = new OpenFileDialog
            {
                Multiselect = false
            };

            var result = dialog.ShowDialog(this);
            if (result == true)
            {
                if (dialog.FileNames.Any())
                {
                    FilePath = dialog.FileNames.First();
                }
            }
        }

        private void OnInputStringChanged()
        {
            _filePath = null;
            OnPropertyChanged(nameof(FilePath));
            Calculate();
        }

        private void OnFilePathChanged()
        {
            if (File.Exists(FilePath))
            {
                _inputString = null;
                OnPropertyChanged(nameof(InputString));
                Calculate();
            }
            else
            {
                _filePath = null;
                OnPropertyChanged(nameof(FilePath));
            }
        }

        private void Calculate()
        {
            _lastCancellationTokenSource?.Cancel();
            _lastCancellationTokenSource = new CancellationTokenSource();

            if (FilePath != null)
            {
                Calculate(new FileInfo(FilePath), _lastCancellationTokenSource.Token);
            }
            else
            {
                Calculate(InputString, _lastCancellationTokenSource.Token);
            }
        }

        private void Calculate(FileInfo fileInfo, CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    var md5 = SecurityHelper.Hash.Md5.ComputeHash(fileInfo);
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        Md5 = md5;
                    }
                }

                if (!cancellationToken.IsCancellationRequested)
                {
                    var sha1 = SecurityHelper.Hash.Sha1.ComputeHash(fileInfo);
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        Sha1 = sha1;
                    }
                }

                if (!cancellationToken.IsCancellationRequested)
                {
                    var sha256 = SecurityHelper.Hash.Sha256.ComputeHash(fileInfo);
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        Sha256 = sha256;
                    }
                }

                if (!cancellationToken.IsCancellationRequested)
                {
                    var sha384 = SecurityHelper.Hash.Sha384.ComputeHash(fileInfo);
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        Sha384 = sha384;
                    }
                }

                if (!cancellationToken.IsCancellationRequested)
                {
                    var sha512 = SecurityHelper.Hash.Sha512.ComputeHash(fileInfo);
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        Sha512 = sha512;
                    }
                }

                UpdateCalculateValues();
            }, cancellationToken);
        }

        private void Calculate(string str, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(str))
            {
                Md5 = null;
                Sha1 = null;
                Sha256 = null;
                Sha384 = null;
                Sha512 = null;
                
                UpdateCalculateValues();
            }
            else
            {
                Task.Run(() =>
                {
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        var md5 = SecurityHelper.Hash.Md5.ComputeHash(str);
                        if (!cancellationToken.IsCancellationRequested)
                        {
                            Md5 = md5;
                        }
                    }

                    if (!cancellationToken.IsCancellationRequested)
                    {
                        var sha1 = SecurityHelper.Hash.Sha1.ComputeHash(str);
                        if (!cancellationToken.IsCancellationRequested)
                        {
                            Sha1 = sha1;
                        }
                    }

                    if (!cancellationToken.IsCancellationRequested)
                    {
                        var sha256 = SecurityHelper.Hash.Sha256.ComputeHash(str);
                        if (!cancellationToken.IsCancellationRequested)
                        {
                            Sha256 = sha256;
                        }
                    }

                    if (!cancellationToken.IsCancellationRequested)
                    {
                        var sha384 = SecurityHelper.Hash.Sha384.ComputeHash(str);
                        if (!cancellationToken.IsCancellationRequested)
                        {
                            Sha384 = sha384;
                        }
                    }

                    if (!cancellationToken.IsCancellationRequested)
                    {
                        var sha512 = SecurityHelper.Hash.Sha512.ComputeHash(str);
                        if (!cancellationToken.IsCancellationRequested)
                        {
                            Sha512 = sha512;
                        }
                    }

                    UpdateCalculateValues();
                }, cancellationToken);
            }
        }

        private void UpdateCalculateValues()
        {
            OnPropertyChanged(nameof(Md5));
            OnPropertyChanged(nameof(Sha1));
            OnPropertyChanged(nameof(Sha256));
            OnPropertyChanged(nameof(Sha384));
            OnPropertyChanged(nameof(Sha512));
        }

        private void MainWindow_OnDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                FilePath = ((System.Array) e.Data.GetData(DataFormats.FileDrop))?.GetValue(0).ToString();
            }

            DropMask.Visibility = Visibility.Collapsed;
        }

        private void MainWindow_OnDragEnter(object sender, DragEventArgs e)
        {
            DropMask.Visibility = Visibility.Visible;
        }

        private void MainWindow_OnDragLeave(object sender, DragEventArgs e)
        {
            DropMask.Visibility = Visibility.Collapsed;
        }
    }
}
