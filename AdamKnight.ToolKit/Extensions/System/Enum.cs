﻿using System.Runtime.CompilerServices;

namespace AdamKnight.ToolKit.Extensions;

partial class Extensions
{
	public static int ToInt32<TEnum>(this ref TEnum e)
		where TEnum : struct, Enum => Unsafe.As<TEnum, int>(ref e);
}
