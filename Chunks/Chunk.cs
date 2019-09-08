﻿using System;

namespace VoxReader
{
    public class Chunk
    {
        /// <summary>
        /// The ID of the chunk
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Complete chunk data
        /// </summary>
        public byte[] Data { get; }

        /// <summary>
        /// Contains chunk specific information
        /// </summary>
        public byte[] Content { get; }

        /// <summary>
        /// Contains children chunks
        /// </summary>
        public byte[] Children { get; }

        /// <summary>
        /// Creates a new chunk using the given data
        /// </summary>
        /// <param name="data">Data starting at the first byte of the chunk</param>
        public Chunk(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data), "Data is null!");
            if (data.Length == 0)
                throw new InvalidDataException("Data is empty!");

            Data = data;

            Id = new string(Reader.GetCharArray(data, 0, 4));
            Content = data.GetRange(12, BitConverter.ToInt32(data, 4));
            Children = data.GetRange(12 + Content.Length, BitConverter.ToInt32(data, 8));
        }

        public override string ToString()
        {
            return $"{Id} N: {Content.Length} M: {Children.Length}";
        }
    }
}