﻿namespace application_recip.Helpers;

public static class CloneHelper<T>
{
    public static T? Clone(T? instance)
    {
        T? clone = default;

        if (instance != null)
        {
            var type = instance.GetType();

            clone = Activator.CreateInstance<T>();

            foreach (var property in type.GetProperties())
            {
                var value = property.GetValue(instance, null);
                var clonedProperty = type.GetProperty(property.Name);

                clonedProperty.SetValue(clone, value);
            }
        }

        return clone;
    }
}
