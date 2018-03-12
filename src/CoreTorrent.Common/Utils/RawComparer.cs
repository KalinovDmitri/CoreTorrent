using System;

namespace CoreTorrent.Common
{
	public static class RawComparer
	{
		public static bool BytesEqual(byte[] first, byte[] second)
		{
			if (first == null)
			{
				throw new ArgumentNullException(nameof(first));
			}
			if (second == null)
			{
				throw new ArgumentNullException(nameof(second));
			}

			return (first.Length == second.Length) && BytesEqual(first, 0, second, 0, first.Length);
		}

		public static bool BytesEqual(byte[] first, int firstOffset, byte[] second, int secondOffset, int count)
		{
			if (first == null)
			{
				throw new ArgumentNullException(nameof(first));
			}
			if (second == null)
			{
				throw new ArgumentNullException(nameof(second));
			}

			if (first.Length - firstOffset < count || second.Length - secondOffset < count)
			{
				return false;
			}

			for (int index = 0; index < count; ++index)
			{
				if (first[firstOffset + index] != second[secondOffset + index])
					return false;
			}

			return true;
		}
	}
}