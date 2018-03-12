using System;

namespace CoreTorrent.BEncode
{
	public class BEncodedNumber : BEncodedValue, IComparable<BEncodedNumber>
	{
		#region Fields and properties

		private long _number;

		public long Number
		{
			get => _number;
			set => _number = value;
		}

		public override int LengthInBytes
		{
			get
			{
				long number = _number;
				int length = 2;

				if (number == 0L) return length + 1;
				if (number < 0L)
				{
					number = -number; length++;
				}
				for (long i = number; i != 0L; i /= 10) length++;

				return length;
			}
		}
		#endregion

		#region Constructors

		public BEncodedNumber() : this(0L) { }

		public BEncodedNumber(long value) => _number = value;
		#endregion

		#region Class methods

		public int CompareTo(BEncodedNumber other)
		{
			if (other == null)
			{
				throw new ArgumentNullException(nameof(other));
			}

			return _number.CompareTo(other._number);
		}

		public override bool Equals(object obj)
		{
			var other = obj as BEncodedNumber;

			return (other != null) && _number == other._number;
		}

		public override int GetHashCode() => _number.GetHashCode();

		public override string ToString() => _number.ToString();
		#endregion

		#region Operators

		public static implicit operator BEncodedNumber(long value) => new BEncodedNumber(value);
		#endregion
	}
}