﻿using jumpfs.CommandLineParsing;

namespace jumpfs.Commands
{
    public class CmdList
    {
        public static readonly CommandDescriptor Descriptor
            = new CommandDescriptor(Run, "list")
                .WithArguments(
                    ArgumentDescriptor.Create<string>(Names.Name)
                        .WithHelpText("restrict the output to items where the name or path contains the supplied name")
                        .AllowEmpty()
                )
                .WithHelpText("lists all or a subset of stored bookmarks");

        private static void Run(ParseResults results, ApplicationContext context)
        {
            var name = results.ValueOf<string>(Names.Name);
            var marks = context.Repo.List(name);
            foreach (var mark in marks)
            {
                context.WriteLine($"{mark.Name} --> {context.ToNative(mark.Path)}");
            }
        }
    }
}