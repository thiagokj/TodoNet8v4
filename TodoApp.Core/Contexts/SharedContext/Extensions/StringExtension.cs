﻿using System.Text;

namespace TodoApp.Core.Contexts.SharedContext.Extensions;
public static class StringExtension
{
    public static string ToBase64(this string text)
        => Convert.ToBase64String(Encoding.ASCII.GetBytes(text));
}