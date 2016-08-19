﻿using System.Collections.Generic;
using System.IO;

namespace InvertedTomato.IntegerCompression {
    /// <summary>
    /// Writer for VLQ signed numbers. Values are translated to unsigned values using ProtoBuffer's ZigZag algorithm.
    /// </summary>
    public class VLQSignedWriter : ISignedWriter {
        /// <summary>
        /// Write all given values.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static byte[] WriteAll(IEnumerable<long> values) { return WriteAll(0, values); }

        /// <summary>
        /// Write all given values with options.
        /// </summary>
        /// <param name="expectedMinValue">The expected minimum value to optimize encoded values for. To match standard use 0.</param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static byte[] WriteAll(ulong expectedMinValue, IEnumerable<long> values) {
            using (var stream = new MemoryStream()) {
                using (var writer = new VLQSignedWriter(stream, expectedMinValue)) {
                    foreach (var value in values) {
                        writer.Write(value);
                    }
                }
                return stream.ToArray();
            }
        }

        /// <summary>
        /// If disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Underlying unsigned writer.
        /// </summary>
        private readonly VLQUnsignedWriter Underlying;

        /// <summary>
        /// Standard instantiation.
        /// </summary>
        /// <param name="output"></param>
        public VLQSignedWriter(Stream output) {
            Underlying = new VLQUnsignedWriter(output);
        }

        /// <summary>
        /// Instantiate with options.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="expectedMinValue">The expected minimum value to optimize encoded values for. To match standard use 0.</param>
        public VLQSignedWriter(Stream input, ulong expectedMinValue) {
            Underlying = new VLQUnsignedWriter(input, expectedMinValue);
        }

        /// <summary>
        /// Append value to stream.
        /// </summary>
        /// <param name="value"></param>
        public void Write(long value) {
            var innerValue = ZigZag.Encode(value);
            Underlying.Write(innerValue);
        }

        /// <summary>
        /// Flush any unwritten bits and dispose.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing) {
            if (IsDisposed) {
                return;
            }
            IsDisposed = true;

            Underlying.Dispose();

            if (disposing) {
                // Dispose managed state (managed objects).
            }
        }

        /// <summary>
        /// Flush any unwritten bits and dispose.
        /// </summary>
        public void Dispose() {
            Dispose(true);
        }
    }
}
