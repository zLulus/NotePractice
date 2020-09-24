using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfDemo.LogicalTreeAndVisualTree.Tools
{
    public static class VisualTreeTool
    {
        /// <summary>
        /// Find all controls in WPF Window by type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="StopAt"></param>
        /// <returns></returns>
        public static T FindParent<T>(DependencyObject item, Type StopAt) where T : class
        {
            if (item is T)
            {
                return item as T;
            }
            else
            {
                //Do note that using the VisualTreeHelper does only work on controls that derive from Visual or Visual3D. If you also need to inspect other elements (e.g. TextBlock, FlowDocument etc.), using VisualTreeHelper will throw an exception.
                //使用VisualTreeHelper仅对从Visual或Visual3D派生的控件起作用。 如果还需要检查其他元素（例如TextBlock，FlowDocument等），则使用VisualTreeHelper将引发异常。
                DependencyObject _parent = VisualTreeHelper.GetParent(item);
                if (_parent == null)
                {
                    return default(T);
                }
                else
                {
                    Type _type = _parent.GetType();
                    if (StopAt != null)
                    {
                        if ((_type.IsSubclassOf(StopAt) == true) || (_type == StopAt))
                        {
                            return null;
                        }
                    }

                    if ((_type.IsSubclassOf(typeof(T)) == true) || (_type == typeof(T)))
                    {
                        return _parent as T;
                    }
                    else
                    {
                        return FindParent<T>(_parent, StopAt);
                    }
                }
            }
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                int depObjCount = VisualTreeHelper.GetChildrenCount(depObj);
                for (int i = 0; i < depObjCount; i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    if (child is GroupBox)
                    {
                        GroupBox gb = child as GroupBox;
                        Object gpchild = gb.Content;
                        if (gpchild is T)
                        {
                            yield return (T)child;
                            child = gpchild as T;
                        }
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public static DependencyObject FindInVisualTreeDown(DependencyObject obj, Type type)
        {
            if (obj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(obj, i);

                    if (child.GetType() == type)
                    {
                        return child;
                    }

                    DependencyObject childReturn = FindInVisualTreeDown(child, type);
                    if (childReturn != null)
                    {
                        return childReturn;
                    }
                }
            }

            return null;
        }

        public static IEnumerable<T> FindVisualChildren2<T>(DependencyObject depObj) where T : DependencyObject
        {
            //调用
            //foreach (TextBlock tb in FindVisualChildren<TextBlock>(window))
            //{
            //    // do something with tb here
            //}

            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren2<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        /// <summary>
        /// To get a list of all childs of a specific type you can use:
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<DependencyObject> FindInVisualTreeDown2(DependencyObject obj, Type type)
        {
            if (obj != null)
            {
                if (obj.GetType() == type)
                {
                    yield return obj;
                }

                for (var i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                {
                    foreach (var child in FindInVisualTreeDown2(VisualTreeHelper.GetChild(obj, i), type))
                    {
                        if (child != null)
                        {
                            yield return child;
                        }
                    }
                }
            }

            yield break;
        }
    }
}
