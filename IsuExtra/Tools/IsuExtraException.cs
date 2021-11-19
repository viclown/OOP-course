﻿using System;
using Isu.Tools;

namespace IsuExtra.Tools
{
    public class IsuExtraException : IsuException
    {
        public IsuExtraException()
        {
        }

        public IsuExtraException(string message)
            : base(message)
        {
        }

        public IsuExtraException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}