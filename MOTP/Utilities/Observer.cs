using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MOTP.Utilities
{
    public static class Observer
    {
        public static object GetBinding(DependencyObject obj)
            => obj.GetValue(BindingProperty);

        public static void SetBinding(DependencyObject obj, object value)
            => obj.SetValue(BindingProperty, value);

        public static readonly DependencyProperty BindingProperty =
            DependencyProperty.RegisterAttached("Binding", typeof(object),
                typeof(Observer), new PropertyMetadata(null, PropertyChangedCallback));

        public static void AddValueChangedHandler(DependencyObject d, RoutedEventHandler handler)
            => ((UIElement)d).AddHandler(ValueChangedEvent, handler);

        public static void RemoveValueChangedHandler(DependencyObject d, RoutedEventHandler handler)
            => ((UIElement)d).RemoveHandler(ValueChangedEvent, handler);

        public static readonly RoutedEvent ValueChangedEvent =
            EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble,
                typeof(RoutedEventHandler), typeof(Observer));

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((UIElement)d).RaiseEvent(new RoutedEventArgs(ValueChangedEvent, d));
    }
}
