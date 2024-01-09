public static class ExtensionMethods
{
    public static T NullCast<T>(this T obj) where T : UnityEngine.Object
        => (obj != null) ? obj : null;
}