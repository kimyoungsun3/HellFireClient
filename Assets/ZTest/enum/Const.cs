using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
	public struct Const<T>
	{
		public T Value { get; private set; }

		public Const(T value)
			: this()
		{
			this.Value = value;
		}
	}
}