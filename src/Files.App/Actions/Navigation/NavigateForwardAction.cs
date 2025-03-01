﻿// Copyright (c) 2023 Files Community
// Licensed under the MIT License. See the LICENSE.

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using Files.App.Commands;
using Files.App.Contexts;
using Files.App.Extensions;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Files.App.Actions
{
	internal class NavigateForwardAction : ObservableObject, IAction
	{
		private readonly IContentPageContext context = Ioc.Default.GetRequiredService<IContentPageContext>();

		public string Label { get; } = "Forward".GetLocalizedResource();

		public string Description { get; } = "NavigateForwardDescription".GetLocalizedResource();

		public HotKey HotKey { get; } = new(Keys.Right, KeyModifiers.Menu);
		public HotKey SecondHotKey { get; } = new(Keys.Mouse5);
		public HotKey MediaHotKey { get; } = new(Keys.GoForward, false);

		public RichGlyph Glyph { get; } = new("\uE72A");

		public bool IsExecutable => context.CanGoForward;

		public NavigateForwardAction()
		{
			context.PropertyChanged += Context_PropertyChanged;
		}

		public Task ExecuteAsync()
		{
			context.ShellPage!.Forward_Click();
			return Task.CompletedTask;
		}

		private void Context_PropertyChanged(object? sender, PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case nameof(IContentPageContext.CanGoForward):
					OnPropertyChanged(nameof(IsExecutable));
					break;
			}
		}
	}
}
