using System;
using System.IO;

namespace CoreTorrent.BEncode.IO
{
	public class BEncodedStream : Stream
	{
		#region Fields and properties

		private Stream _baseStream;

		private bool _hasPeek;
		private bool _strictDecoding;
		private byte[] _peeked;

		public override bool CanRead => _baseStream.CanRead;

		public override bool CanSeek => _baseStream.CanSeek;

		public override bool CanWrite => false;

		public override long Length => _baseStream.Length;

		public override long Position
		{
			get => (_hasPeek) ? _baseStream.Position - 1 : _baseStream.Position;
			set
			{
				if (value != Position)
				{
					_hasPeek = false;
					_baseStream.Position = value;
				}
			}
		}

		public bool StrictDecoding => _strictDecoding;
		#endregion

		#region Constructors

		public BEncodedStream(Stream baseStream) : this(baseStream, true) { }

		public BEncodedStream(Stream baseStream, bool strictDecoding)
		{
			if (baseStream == null)
			{
				throw new ArgumentNullException(nameof(baseStream));
			}

			_baseStream = baseStream;
			_peeked = new byte[1];
			_strictDecoding = strictDecoding;
		}
		#endregion

		#region Class methods

		public override void Flush()
		{
			throw new NotSupportedException();
		}

		public int PeekByte()
		{
			if (!_hasPeek)
			{
				_hasPeek = Read(_peeked, 0, 1) == 1;
			}
			return _hasPeek ? _peeked[0] : (-1);
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			int read = 0;

			if (_hasPeek && count > 0)
			{
				_hasPeek = false;
				buffer[offset] = _peeked[0];
				offset++;
				count--;
				read++;
			}

			return read + _baseStream.Read(buffer, offset, count);
		}

		public override int ReadByte()
		{
			if (_hasPeek)
			{
				_hasPeek = false;
				return _peeked[0];
			}
			return base.ReadByte();
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			long value = (!_hasPeek || origin != SeekOrigin.Current) ? _baseStream.Seek(offset, origin) : _baseStream.Seek(offset - 1, origin);
			_hasPeek = false;
			return value;
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}
		#endregion
	}
}