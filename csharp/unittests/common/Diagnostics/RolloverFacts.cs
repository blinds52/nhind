﻿using System;
using System.IO;
using System.Threading;

using Health.Direct.Common.Diagnostics;

using Xunit;

namespace Health.Direct.Common.Tests.Diagnostics
{
    public class RolloverFacts
    {
        [Fact(Skip = "Long running test used to test the file rollover.")]
        public void Rollover()
        {
            var files = Directory.GetFiles(".", "common-tests*.log");
            foreach (var file in files)
            {
                File.Delete(file);
            }

            var logger = Log.For(this);
            Assert.NotNull(logger);

            var until = DateTime.Now.AddMinutes(1);
            while (DateTime.Now < until)
            {
                logger.Debug("The time is - {0:HH:mm:ss.fff}", DateTime.Now);
                Thread.Sleep(1000);
                Assert.True(File.Exists("common-tests.log"));
            }

            Assert.True(File.Exists("common-tests.000.log"));
        }
    }
}
