using System.Collections.Generic;
using UnityEngine;

namespace GameCore.CustomExtensions.DebugSystemExtensions
{
	public static class DebugExtensions
	{
		public static void DebugMessage(string message, MessageType messageType = MessageType.Log)
		{
			string debugMessage = "<color=" + MessageColour[messageType] + ">" + message + "</color>";
			Debug.Log(debugMessage);
		}

		private static readonly Dictionary<MessageType, string> MessageColour = new Dictionary<MessageType, string>()
		{
			{MessageType.Log, "white"},
			{MessageType.Warning, "yellow"},
			{MessageType.Error, "red"}
		};

		public enum MessageType
		{
			Log,
			Warning,
			Error
		}
	}
}