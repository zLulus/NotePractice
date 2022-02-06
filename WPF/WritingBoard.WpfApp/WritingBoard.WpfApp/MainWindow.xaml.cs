using System;
using System.Windows;
using System.Windows.Input;
using WritingBoard.WpfApp.Commands;

namespace WritingBoard.WpfApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel vm { get; set; }
        public ICommand ClearCommand { get; }
        public ICommand UndoCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand TextSelectCommand { get; }

        public event Action Closed;

        public MainWindow()
        {
            vm = new MainWindowViewModel();
            DataContext = vm;
            ClearCommand = new Command(Clear);
            UndoCommand = new Command(Undo);
            RemoveCommand = new Command(Remove);
            CloseCommand = new Command(Close);
            TextSelectCommand = new Command<string>(TextSelect);
            InitializeComponent();
        }

        private void TextSelect(string text)
        {
            vm.InputText += text;
            Clear();
        }

        private void Close()
        {
            Clear();
            Closed?.Invoke();
        }

        private void Clear()
        {
            inkCanvas.Strokes.Clear();
            vm.ClearAlternates();
        }

        private void Undo()
        {
            if (inkCanvas.Strokes.Count == 0)
                return;

            inkCanvas.Strokes.RemoveAt(inkCanvas.Strokes.Count - 1);
            vm.RecognizeCommand.Execute(inkCanvas.Strokes);
        }

        private void Remove()
        {
            if (string.IsNullOrWhiteSpace(vm.InputText))
                return;

            vm.InputText = vm.InputText.Substring(0, vm.InputText.Length - 1);
        }
    }
}
