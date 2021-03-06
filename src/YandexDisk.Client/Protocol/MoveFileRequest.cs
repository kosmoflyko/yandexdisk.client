﻿using JetBrains.Annotations;

namespace YandexDisk.Client.Protocol
{
    /// <summary>
    /// Request of moving file on Disk
    /// </summary>
    [PublicAPI]
    public class MoveFileRequest
    {
        /// <summary>
        /// Путь к перемещаемому ресурсу. Например, %2Ffoo%2Fphoto.png.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Путь к новому положению ресурса. Например, %2Fbar%2Fphoto.png.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Признак перезаписи. Учитывается, если ресурс копируется в папку, в которой уже есть ресурс с таким именем.
        /// Допустимые значения:
        /// true — удалять файлы с совпадающими именами и записывать копируемые файлы.
        /// false — используется по умолчанию. Указывает не перезаписывать файлы и отменить копирование.
        /// </summary>
        public bool Overwrite { get; set; }
    }
}
