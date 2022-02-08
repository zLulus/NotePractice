using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace HandwritingDemo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MainPageViewModel vm { get; set; }
        private readonly int MAX_ALTERNATES_COUNT = 21;
        
        private Task m_task;
        CancellationTokenSource s_cts;

        public MainPage()
        {
            this.InitializeComponent();

            vm = new MainPageViewModel();
            DataContext = vm;
            inkCanvas.InkPresenter.InputDeviceTypes = Windows.UI.Core.CoreInputDeviceTypes.Mouse | Windows.UI.Core.CoreInputDeviceTypes.Pen | Windows.UI.Core.CoreInputDeviceTypes.Touch;
            InkDrawingAttributes inkDrawingAttributes = inkCanvas.InkPresenter.CopyDefaultDrawingAttributes();
            inkDrawingAttributes.Size = new Size(8, 8);
            inkDrawingAttributes.Color = Windows.UI.Color.FromArgb(255, 112, 112, 112);
            inkDrawingAttributes.FitToCurve = true;
            inkDrawingAttributes.PenTip = PenTipShape.Rectangle;
            inkCanvas.InkPresenter.UpdateDefaultDrawingAttributes(inkDrawingAttributes);

            inkCanvas.InkPresenter.StrokesCollected += InkPresenter_StrokesCollected;
        }

        private void InkPresenter_StrokesCollected(InkPresenter sender, InkStrokesCollectedEventArgs args)
        {
            s_cts?.Cancel();
            m_task?.Dispose();
            s_cts = new CancellationTokenSource();
            m_task = new Task(async () =>
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.High, async () =>
                {
                    await Recognize(s_cts.Token);
                });
            }
            , s_cts.Token);
            m_task.Start();
        }

        private async Task Recognize(CancellationToken token)
        {
            if (inkCanvas.InkPresenter.StrokeContainer.GetStrokes().Count == 0)
                return;

            if (token.IsCancellationRequested)
                return;

            InkRecognizerContainer inkRecognizerContainer = new InkRecognizerContainer();
            IReadOnlyList<InkRecognizer> recognizers = inkRecognizerContainer.GetRecognizers();
            InkRecognizer recognizer = recognizers.SingleOrDefault(r => r.Name == "Microsoft 中文(简体)手写识别器");
            inkRecognizerContainer.SetDefaultRecognizer(recognizer);
            if (token.IsCancellationRequested)
                return;

            IReadOnlyList<InkRecognitionResult> results = await inkRecognizerContainer.RecognizeAsync(inkCanvas.InkPresenter.StrokeContainer, InkRecognitionTarget.All);

            if (token.IsCancellationRequested)
                return;
            
            vm.Alternates.Clear();

            foreach (InkRecognitionResult result in results)
            {
                foreach (string text in result.GetTextCandidates())
                {
                    if (vm.Alternates.Count < MAX_ALTERNATES_COUNT)
                        vm.Alternates.Add(text);
                    else
                        return;
                }
            }

        }


        private void Remove_Click(object sender, TappedRoutedEventArgs e)
        {
            inkCanvas.InkPresenter.StrokeContainer.Clear();
            vm.Alternates.Clear();

        }
    }
}
