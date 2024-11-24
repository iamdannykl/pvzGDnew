using System.Net.Http;
using Godot;

namespace GodotProjects.code;

public partial class httpTst : Button
{
	string esp32IpAddress = "localhost:5000";  // 使用Flask模拟的ESP32服务器地址
	bool isOn;
	void anxia()
	{
		Main();
	}
	async void Main()
	{
		// Flask服务器的IP地址和端口
		// 初始化HttpClient
		using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
		{
			// 打开LED
			if (!isOn)
			{
				isOn = true;
				string onUrl = $"http://{esp32IpAddress}/on";
				HttpResponseMessage onResponse = await client.GetAsync(onUrl);
				if (onResponse.IsSuccessStatusCode)
				{
					GD.Print("LED is turned ON.");
				}
				else
				{
					GD.Print("Failed to turn on LED.");
				}
			}
			else
			{
				isOn = false;
				// 关闭LED
				string offUrl = $"http://{esp32IpAddress}/off";
				HttpResponseMessage offResponse = await client.GetAsync(offUrl);
				if (offResponse.IsSuccessStatusCode)
				{
					GD.Print("LED is turned OFF.");
				}
				else
				{
					GD.Print("Failed to turn off LED.");
				}
			}
		}
	}
}