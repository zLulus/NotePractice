using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Ink;
using System.Windows.Input;

namespace HandwritingDemo.ViewModel
{
    /// <summary>
    /// 识别核心类
    /// </summary>
    public class HandwritingRecognizeViewModel : NotifyPropertyChangedBase
    {
        private readonly int MAX_Alternates_COUNT = 9;
        /// <summary>
        /// 中文语言Id
        /// </summary>
        private static readonly int ChsLanguageId = 0x0804;

        private ObservableCollection<string> _alternates;

        /// <summary>
        /// Get 候选词列表
        /// </summary>
        public ObservableCollection<string> Alternates
        {
            get { return _alternates; }

            private set
            {
                if (_alternates != value)
                {
                    _alternates = value;
                    RaisePropertyChanged(nameof(Alternates));
                }
            }
        }

        public ICommand RecognizeCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand UndoCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand TextSelectCommand { get; }

        private string _inputText;
        public string InputText
        {
            get { return _inputText; }

            set
            {
                if (_inputText != value)
                {
                    _inputText = value;
                    RaisePropertyChanged(nameof(InputText));
                }
            }
        }

        public StrokeCollection _strokes;
        public StrokeCollection Strokes
        {
            get { return _strokes; }

            set
            {
                if (_strokes != value)
                {
                    _strokes = value;
                    RaisePropertyChanged(nameof(Strokes));
                }
            }
        }

        public HandwritingRecognizeViewModel()
        {
            Alternates = new ObservableCollection<string>();
            RecognizeCommand = new Command<StrokeCollection>(Recognize);
            ClearCommand = new Command(Clear);
            UndoCommand = new Command(Undo);
            RemoveCommand = new Command(Remove);
            CloseCommand = new Command(Close);
            TextSelectCommand = new Command<string>(TextSelect);
        }

        /// <summary>
        /// 进行识别
        /// </summary>
        /// <param name="strokes">笔迹集合</param>
        private void Recognize(StrokeCollection strokes)
        {
            Alternates.Clear();

            if (strokes == null || strokes.Count == 0)
                return;

            Stroke stroke = GetCombinedStore(strokes);

            using (InkAnalyzer analyzer = new InkAnalyzer())
            {
                analyzer.AddStroke(stroke, ChsLanguageId);
                analyzer.SetStrokeType(stroke, StrokeType.Writing);

                AnalysisStatus status = analyzer.Analyze();

                if (status.Successful)
                {
                    foreach (string item in analyzer.GetAlternates().OfType<AnalysisAlternate>().Select(x => x.RecognizedString).ToArray())
                        if (Alternates.Count < MAX_Alternates_COUNT)
                            Alternates.Add(item);
                }
            }
        }

        /// <summary>
        /// 清空候选词
        /// </summary>
        public void ClearAlternates()
        {
            Alternates.Clear();
        }

        private static Stroke GetCombinedStore(StrokeCollection strokes)
        {
            StylusPointCollection points = new StylusPointCollection();

            foreach (Stroke stroke in strokes)
                points.Add(stroke.StylusPoints);

            return new Stroke(points);
        }

        private void TextSelect(string text)
        {
            InputText += text;
            Clear();
        }

        private void Close()
        {
            Clear();
        }

        private void Clear()
        {
            Strokes.Clear();
            ClearAlternates();
        }

        private void Undo()
        {
            if (Strokes.Count == 0)
                return;

            Strokes.RemoveAt(Strokes.Count - 1);
            RecognizeCommand.Execute(Strokes);
        }

        private void Remove()
        {
            if (string.IsNullOrEmpty(InputText))
                return;

            InputText = InputText.Substring(0, InputText.Length - 1);
        }
    }
}
