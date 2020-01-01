using System;

public interface IMyExtension
{
	string GetName();
	string GetDateTime();
	object Anything { get; set; }
}
