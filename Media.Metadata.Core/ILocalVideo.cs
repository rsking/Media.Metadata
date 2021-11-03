// -----------------------------------------------------------------------
// <copyright file="ILocalVideo.cs" company="RossKing">
// Copyright (c) RossKing. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Media.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

internal interface ILocalVideo
{
    System.IO.FileInfo FileInfo { get; }
}
