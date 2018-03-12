using System;

namespace CoreTorrent.BEncode
{
	public abstract class BEncodedValue
	{
		#region Properties

		public abstract int LengthInBytes { get; }
		#endregion

		#region Constructors

		protected internal BEncodedValue() { }
		#endregion
	}
}