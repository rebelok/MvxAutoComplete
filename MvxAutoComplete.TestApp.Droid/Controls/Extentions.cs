using System;
using Android.Views;

namespace MvxAutoComplete.TestApp.Droid.Controls
{
    public static class Extentions
    {
        private static readonly Random RandomId = new Random();

        public static int GetGeneratedId(this ViewGroup viewGroup)
        {
            for (; ; )
            {
                var id = RandomId.Next(1, 0x00FFFFFF);
                if (viewGroup.FindViewById<View>(id) != null)
                {
                    continue;
                }
                return id;
            }
        }
    }
}