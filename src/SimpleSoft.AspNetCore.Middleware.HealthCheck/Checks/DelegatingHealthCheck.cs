﻿#region License
// The MIT License (MIT)
// 
// Copyright (c) 2018 Simplesoft.pt
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

// ReSharper disable once CheckNamespace
namespace SimpleSoft.AspNetCore.Middleware.HealthCheck
{
    /// <summary>
    /// Health check thar invokes a funcion to calculate the current <see cref="HealthCheckStatus"/>.
    /// Exceptions thrown by the function will set the status to <see cref="HealthCheckStatus.Red"/>.
    /// </summary>
    public class DelegatingHealthCheck : HealthCheck
    {
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="properties">The health check properties</param>
        /// <param name="logger">An optional logger instance</param>
        /// <exception cref="ArgumentNullException"></exception>
        public DelegatingHealthCheck(DelegatingHealthCheckProperties properties, ILogger<DelegatingHealthCheck> logger = null) 
            : base(properties, logger)
        {
            Properties = properties ?? throw new ArgumentNullException(nameof(properties));
        }

        /// <summary>
        /// The delegate health check properties
        /// </summary>
        protected new DelegatingHealthCheckProperties Properties { get; }

        /// <inheritdoc />
        public override Task<HealthCheckStatus> OnUpdateStatusAsync(CancellationToken ct) => Properties.Action(ct);
    }
}