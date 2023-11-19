using System;

public static class WithExtension
{
    public static T With<T>(this T obj, Action<T> set)
    {
        set.Invoke(obj);
        return obj;
    }
        
    public static T With<T>(this T obj, Func<bool> when, Action<T> set)
    {
        if (when.Invoke())
            set.Invoke(obj);
            
        return obj;
    }
    
    public static T With<T>(this T obj, bool when, Action<T> set)
    {
        if (when)
            set.Invoke(obj);
            
        return obj;
    }

    public static ConditionalAction<T> When<T>(this T obj, Func<bool> condition)
    {
        return new ConditionalAction<T>(obj, condition);
    }

    public class ConditionalAction<T>
    {
        private T _object;
        private Func<bool> _condition;

        public ConditionalAction(T obj, Func<bool> condition)
        {
            this._object = obj;
            this._condition = condition;
        }

        public T With(Action<T> action)
        {
            if (_condition.Invoke())
                action.Invoke(_object);

            return _object;
        }
    }
}