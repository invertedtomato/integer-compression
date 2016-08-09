﻿using System;
using System.IO;

namespace InvertedTomato.VariableLengthIntegers {
    public static class VarInt {
        /* To be written
        /// <summary>
        /// Encode integer as signed VLQ.
        /// </summary>
        public static byte[] Encode(long value) {
            throw new NotImplementedException();
        }
        public static void Encode(long value, Stream output) {
            if (null == output) {
                throw new ArgumentNullException("output");
            }

            throw new NotImplementedException();
        }
        public static long Decode(Stream stream) {
            var qlv = new SignedVLQ();
            while (qlv.AppendByte(stream.ReadUInt8())) { }
            return qlv.ToValue();
        }

        /// <summary>
        /// Output parameters
        /// </summary>
        private long Value;
        private byte Position;
        private bool IsPositive;

        /// <summary>
        /// Is there more bytes remaining
        /// </summary>
        private bool IsMore = true;

        /// <summary>
        /// Append a byte to the VLQ. Returns true if all bytes are accounted for and the value is ready for reading.
        /// </summary>
        public bool AppendByte(byte value) {
            if (!IsMore) {
                throw new InvalidOperationException("Value already complete.");
            }

            // Handle sign
            byte startBit = 0;
            if (Position == 0) {
                IsPositive = !value.GetBit(0);
                startBit++;
            }

            // Add value
            for (var i = startBit; i < 7; i++) {
                if (value.GetBit(startBit)) {
                    checked { // Recieved more bits than can fit in an uint64 - throw an exception instead of wrapping
                        if (IsPositive) {
                            Value += 1 << Position;
                        } else {
                            Value -= 1 << Position;
                        }
                    }
                }

                Position++;
            }

            // Determine if complete
            IsMore = value.GetBit(7);

            return IsMore;
        }

        /// <summary>
        /// Convert value to a signed integer.
        /// </summary>
        /// <returns></returns>
        public long ToValue() {
            if (IsMore) {
                throw new InvalidOperationException("Value not complete.");
            }

            return IsPositive ? -1 * Value : Value;
        }*/
    }
}