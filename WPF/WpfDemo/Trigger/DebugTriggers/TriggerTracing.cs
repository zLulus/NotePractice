using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media.Animation;

//来自[In WPF, how to debug triggers?](https://stackoverflow.com/questions/32275445/in-wpf-how-to-debug-triggers)
// Code from http://www.wpfmentor.com/2009/01/how-to-debug-triggers-using-trigger.html
// No license specified - this code is trimmed out from Release build anyway so it should be ok using it this way

// HOWTO: add the following attached property to any trigger and you will see when it is activated/deactivated in the output window
// 如何使用：将以下附加属性添加到任何触发器，您将在输出窗口中看到它何时被激活/停用
//        TriggerTracing.TriggerName="your debug name"
//        TriggerTracing.TraceEnabled="True"

// Example:
// 例子：
// <Trigger my:TriggerTracing.TriggerName="BoldWhenMouseIsOver"  
//          my:TriggerTracing.TraceEnabled="True"  
//          Property="IsMouseOver"  
//          Value="True">  
//     <Setter Property = "FontWeight" Value="Bold"/>  
// </Trigger> 
//
// As this works on anything that inherits from TriggerBase, it will also work on <MultiTrigger>.
//由于此方法适用于从TriggerBase继承的任何内容，因此也适用于<MultiTrigger>

//补充：
//It works by:
//using attached properties to add dummy animation storyboards to the trigger
//activating WPF animation tracing and filtering the results to only the entries with the dummy storyboards
//工作原理：
//使用附加属性将虚拟动画storyboards添加到触发器
//激活WPF动画跟踪并将结果过滤到仅包含storyboards


namespace WpfDemo.Trigger.DebugTriggers
{
    /// <summary>
    /// Contains attached properties to activate Trigger Tracing on the specified Triggers.
    /// 包含附加属性以激活指定触发器上的触发器跟踪
    /// This file alone should be dropped into your app.
    /// 仅此文件应放入您的应用程序
    /// </summary>
    public static class TriggerTracing
    {
        static TriggerTracing()
        {
            // Initialise WPF Animation tracing and add a TriggerTraceListener
            //初始化WPF动画跟踪并添加TriggerTraceListener
            PresentationTraceSources.Refresh();
            PresentationTraceSources.AnimationSource.Listeners.Clear();
            PresentationTraceSources.AnimationSource.Listeners.Add(new TriggerTraceListener());
            PresentationTraceSources.AnimationSource.Switch.Level = SourceLevels.All;
        }

        #region TriggerName attached property

        /// <summary>
        /// Gets the trigger name for the specified trigger. This will be used
        /// to identify the trigger in the debug output.
        /// 获取指定触发器的触发器名称。 这将用于在调试输出中标识触发器。
        /// </summary>
        /// <param name="trigger">The trigger.</param>
        /// <returns></returns>
        public static string GetTriggerName(TriggerBase trigger)
        {
            return (string)trigger.GetValue(TriggerNameProperty);
        }

        /// <summary>
        /// Sets the trigger name for the specified trigger. This will be used
        /// to identify the trigger in the debug output.
        /// 设置指定触发器的触发器名称。 这将用于在调试输出中标识触发器。
        /// </summary>
        /// <param name="trigger">The trigger.</param>
        /// <returns></returns>
        public static void SetTriggerName(TriggerBase trigger, string value)
        {
            trigger.SetValue(TriggerNameProperty, value);
        }

        public static readonly DependencyProperty TriggerNameProperty =
            DependencyProperty.RegisterAttached(
            "TriggerName",
            typeof(string),
            typeof(TriggerTracing),
            new UIPropertyMetadata(string.Empty));

        #endregion

        #region TraceEnabled attached property

        /// <summary>
        /// Gets a value indication whether trace is enabled for the specified trigger.
        /// 获取一个值指示是否为指定的触发器启用跟踪
        /// </summary>
        /// <param name="trigger">The trigger.</param>
        /// <returns></returns>
        public static bool GetTraceEnabled(TriggerBase trigger)
        {
            return (bool)trigger.GetValue(TraceEnabledProperty);
        }

        /// <summary>
        /// Sets a value specifying whether trace is enabled for the specified trigger
        /// 设置一个值，该值指定是否为指定的触发器启用跟踪
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="value"></param>
        public static void SetTraceEnabled(TriggerBase trigger, bool value)
        {
            trigger.SetValue(TraceEnabledProperty, value);
        }

        public static readonly DependencyProperty TraceEnabledProperty =
            DependencyProperty.RegisterAttached(
            "TraceEnabled",
            typeof(bool),
            typeof(TriggerTracing),
            new UIPropertyMetadata(false, OnTraceEnabledChanged));

        private static void OnTraceEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var triggerBase = d as TriggerBase;

            if (triggerBase == null)
                return;

            if (!(e.NewValue is bool))
                return;

            if ((bool)e.NewValue)
            {
                // insert dummy story-boards which can later be traced using WPF animation tracing
                //添加虚拟storyboards，以后可以使用WPF动画跟踪对其进行跟踪
                var storyboard = new TriggerTraceStoryboard(triggerBase, TriggerTraceStoryboardType.Enter);
                triggerBase.EnterActions.Insert(0, new BeginStoryboard() { Storyboard = storyboard });

                storyboard = new TriggerTraceStoryboard(triggerBase, TriggerTraceStoryboardType.Exit);
                triggerBase.ExitActions.Insert(0, new BeginStoryboard() { Storyboard = storyboard });
            }
            else
            {
                // remove the dummy storyboards
                //移除虚拟storyboards
                foreach (TriggerActionCollection actionCollection in new[] { triggerBase.EnterActions, triggerBase.ExitActions })
                {
                    foreach (TriggerAction triggerAction in actionCollection)
                    {
                        BeginStoryboard bsb = triggerAction as BeginStoryboard;

                        if (bsb != null && bsb.Storyboard != null && bsb.Storyboard is TriggerTraceStoryboard)
                        {
                            actionCollection.Remove(bsb);
                            break;
                        }
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// 触发器跟踪Storyboard类型
        /// </summary>
        private enum TriggerTraceStoryboardType
        {
            //进入
            Enter, 
            //退出
            Exit
        }

        /// <summary>
        /// A dummy storyboard for tracing purposes
        /// 用于跟踪的虚拟storyboard
        /// </summary>
        private class TriggerTraceStoryboard : Storyboard
        {
            public TriggerTraceStoryboardType StoryboardType { get; private set; }
            public TriggerBase TriggerBase { get; private set; }

            public TriggerTraceStoryboard(TriggerBase triggerBase, TriggerTraceStoryboardType storyboardType)
            {
                TriggerBase = triggerBase;
                StoryboardType = storyboardType;
            }
        }

        /// <summary>
        /// A custom tracelistener.
        /// 自定义跟踪侦听器
        /// </summary>
        private class TriggerTraceListener : TraceListener
        {
            public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
            {
                base.TraceEvent(eventCache, source, eventType, id, format, args);

                if (format.StartsWith("Storyboard has begun;"))
                {
                    TriggerTraceStoryboard storyboard = args[1] as TriggerTraceStoryboard;
                    if (storyboard != null)
                    {
                        // add a breakpoint here to see when your trigger has been
                        // entered or exited
                        //在此处添加断点，以查看何时输入或退出触发器

                        // the element being acted upon
                        //被作用的元素
                        object targetElement = args[5];

                        // the namescope of the element being acted upon
                        //所作用元素的名称范围
                        INameScope namescope = (INameScope)args[7];

                        TriggerBase triggerBase = storyboard.TriggerBase;
                        //获得TriggerTracing.TriggerName
                        string triggerName = GetTriggerName(storyboard.TriggerBase);

                        Debug.WriteLine(string.Format("Element: {0}, {1}: {2}: {3}",
                            targetElement,
                            triggerBase.GetType().Name,
                            triggerName,
                            storyboard.StoryboardType));
                    }
                }
            }

            public override void Write(string message)
            {
            }

            public override void WriteLine(string message)
            {
            }
        }
    }
}
