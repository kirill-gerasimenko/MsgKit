﻿using System;
using System.IO;
using OpenMcdf;

/*
   Copyright 2015 - 2016 Kees van Spelde

   Licensed under The Code Project Open License (CPOL) 1.02;
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

     http://www.codeproject.com/info/cpol10.aspx

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

namespace MsgWriter.Streams
{
    /// <summary>
    ///     The properties stream contained inside the top level of the .msg file, which represents the Message object itself.
    /// </summary>
    internal sealed class TopLevelPropertiesStream : Properties
    {
        #region Properties
        /// <summary>
        ///     The ID to use for naming the next Recipient object storage if one is created inside the .msg file
        /// </summary>
        internal int NextRecipientId { get; private set; }

        /// <summary>
        ///     The ID to use for naming the next Attachment object storage if one is created inside the .msg file
        /// </summary>
        internal int NextAttachmentId { get; private set; }

        /// <summary>
        ///     The number of Recipient objects
        /// </summary>
        internal int RecipientCount { get; private set; }

        /// <summary>
        ///     The number of Attachment objects
        /// </summary>
        internal int AttachmentCount { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        ///     Creates this object and reads all the properties from the toplevel stream
        /// </summary>
        /// <param name="stream">The <see cref="CFStream"/></param>
        internal TopLevelPropertiesStream(CFStream stream)
        {
            using (var memoryStream = new MemoryStream(stream.GetData()))
            using (var binaryReader = new BinaryReader(memoryStream))
            {
                binaryReader.ReadBytes(8); // Reserved
                NextRecipientId = Convert.ToInt32(binaryReader.ReadUInt32());
                NextAttachmentId = Convert.ToInt32(binaryReader.ReadUInt32());
                RecipientCount = Convert.ToInt32(binaryReader.ReadUInt32());
                AttachmentCount = Convert.ToInt32(binaryReader.ReadUInt32());
                binaryReader.ReadBytes(8); // Reserved
                ReadProperties(binaryReader);
            }
        }

        /// <summary>
        ///     Creates this object and sets all its properties
        /// </summary>
        /// <param name="nextRecipientId">
        ///     The ID to use for naming the next Recipient object storage if one is created inside the
        ///     .msg file. If no Recipient object storages are contained in the .msg file, this field MUST be set to 0
        /// </param>
        /// <param name="nextAttachmentId">
        ///     The ID to use for naming the next Attachment object storage if one is created inside the
        ///     .msg file. If no Attachment object storages are contained in the .msg file, this field MUST be set to 0
        /// </param>
        /// <param name="recipientCount">The number of Recipient objects</param>
        /// <param name="attachmentCount">The number of Attachment objects</param>
        internal TopLevelPropertiesStream(int nextRecipientId,
                                          int nextAttachmentId,
                                          int recipientCount,
                                          int attachmentCount)
        {
            NextRecipientId = nextRecipientId;
            NextAttachmentId = nextAttachmentId;
            RecipientCount = recipientCount;
            AttachmentCount = attachmentCount;
        }
        #endregion

        #region WriteProperties
        /// <summary>
        ///     Writes all the string and binary <see cref="Property">properties</see> as a <see cref="CFStream"/> to the 
        ///     given <paramref name="storage" />
        /// </summary>
        /// <param name="storage">The <see cref="CFStorage"/></param>
        internal void WriteProperties(CFStorage storage)
        {
            using (var memoryStream = new MemoryStream())
            using (var binaryWriter = new BinaryWriter(memoryStream))
            {
                binaryWriter.Write(new byte[8]); // Reserved
                binaryWriter.Write(Convert.ToUInt32(NextRecipientId));
                binaryWriter.Write(Convert.ToUInt32(NextAttachmentId));
                binaryWriter.Write(Convert.ToUInt32(RecipientCount));
                binaryWriter.Write(Convert.ToUInt32(AttachmentCount));
                binaryWriter.Write(new byte[8]); // Reserved
                // Indicates that all the properties that we are adding are stored in unicode format
                WriteProperties(storage, binaryWriter);
            }
        }
        #endregion
    }
}