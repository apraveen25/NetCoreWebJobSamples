﻿//===============================================================================
// Microsoft FastTrack for Azure
// .Net Core 3 WebJob and Dependency Injection Samples
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

namespace WebJobDefaultDI
{
    /// <summary>
    /// Simple interface to demonstrate .Net Core default dependency injection capabilities
    /// </summary>
    public interface ISimpleService
    {
        public string SayHello();
    }
}
