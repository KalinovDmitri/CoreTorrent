using System;
using System.Text;

using CoreTorrent.Common;

namespace CoreTorrent.BEncode
{
	public class BEncodedString : BEncodedValue
	{
		#region Fields and properties

		private byte[] _textBytes;
		private string _text;

		public override int LengthInBytes => throw new NotImplementedException();

		public byte[] TextBytes => _textBytes;

		public string Text
		{
			get => _text;
			set => SetText(value);
		}
		#endregion

		#region Constructors

		public BEncodedString() : this(new byte[0]) { }

		public BEncodedString(char[] value)
		{
			_text = new string(value);
			_textBytes = Encoding.UTF8.GetBytes(value);
		}

		public BEncodedString(string value)
		{
			_text = value;
			_textBytes = Encoding.UTF8.GetBytes(value);
		}

		public BEncodedString(byte[] value)
		{
			_text = Encoding.UTF8.GetString(value);
			_textBytes = value;
		}
		#endregion

		#region Class methods

		public override bool Equals(object obj)
		{
			BEncodedString other = obj as BEncodedString;
			if (other == null)
			{
				if (obj is string str)
					other = new BEncodedString(str);
			}

			return (other != null) && RawComparer.BytesEqual(_textBytes, other._textBytes);
		}

		public override int GetHashCode()
		{
			int hash = 0;

			foreach (var item in _textBytes) hash += item;

			return hash;
		}

		public override string ToString() => _text;

		private void SetText(string text)
		{
			_text = text;
			_textBytes = Encoding.UTF8.GetBytes(text);
		}
		#endregion

		#region Operators

		public static implicit operator BEncodedString(string value) => new BEncodedString(value);

		public static implicit operator BEncodedString(char[] value) => new BEncodedString(value);

		public static implicit operator BEncodedString(byte[] value) => new BEncodedString(value);
		#endregion
	}
}