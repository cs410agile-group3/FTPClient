using System;
using Xunit;
using ConsoleParser;
using System.Collections.Generic;

namespace ConsoleParser.UnitTests
{
    public class ConsoleParserTest
    {
        [Fact]
        public void canBeConstructedWithoutExploding()
        {
            Assert.NotNull(new ConsoleParser.Builder().build());
        }

        [Fact]
        public void throwsErrorOnInvalidCommand()
        {
            ConsoleParser parser = new ConsoleParser.Builder().build();
            Assert.Throws<InvalidOperationException>(() => parser.executeCommand("command"));
        }

        [Fact]
        public void doesNotThrowErrorOnValidCommand()
        {
            ConsoleParser parser = new ConsoleParser
                .Builder()
                .withCommand("command", "some command", (List<string> a) => {})
                .build();
            parser.executeCommand("command arg");
        }

        [Fact]
        public void executesValidCommand()
        {
            bool executed = false;
            string arg = null;
            ConsoleParser parser = new ConsoleParser
                .Builder()
                .withCommand("command", "some command", (List<string> a) => { executed = true; arg = a[0]; })
                .build();
            parser.executeCommand("command arg");
            Assert.True(executed);
            Assert.Equal("arg", arg);
        }

        [Fact]
        public void executesMultiArgCommandWithCorrectArgs()
        {
            List<string> foundArgs = null;
            List<string> expectedArgs = new List<string>();
            expectedArgs.Add("some");
            expectedArgs.Add("arg");
            ConsoleParser parser = new ConsoleParser
                .Builder()
                .withCommand("command", "some command", (List<string> a) => { foundArgs = a; })
                .build();
            parser.executeCommand("command some arg");
            Assert.Equal(foundArgs, expectedArgs);
        }

        [Fact]
        public void executesCommandWithNoArgs()
        {
            int argsLength = -1;
            ConsoleParser parser = new ConsoleParser
                .Builder()
                .withCommand("command", "some command", (List<string> a) => { argsLength = a.Count; })
                .build();
            parser.executeCommand("command");
            Assert.Equal(0, argsLength);
        }

        [Fact]
        public void canHoldManyCommands()
        {
            int calls = 0;
            ConsoleParser parser = new ConsoleParser
                .Builder()
                .withCommand("command", "some command", (List<string> a) => { calls += 1; })
                .withCommand("ls", "some command", (List<string> a) => { calls += 1; })
                .withCommand("mv", "some command", (List<string> a) => { calls += 1; })
                .build();
            parser.executeCommand("command some arg");
            parser.executeCommand("ls some arg");
            parser.executeCommand("mv some arg");
            Assert.Equal(3, calls);
        }
    }
}